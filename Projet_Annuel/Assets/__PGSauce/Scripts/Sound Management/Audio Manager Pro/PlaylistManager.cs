using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGSauce.AudioManagement.External;
using PGSauce.AudioManagement.External.Coroutines.Music;
using PGSauce.AudioManagement.External.EventBinding;
using System.Linq;

namespace PGSauce.AudioManagement.External
{


    /// <summary>The PlaylistManager handles all of the Playlists in the scene. The component should only be used once in the scene, and requires a MusicManager to be present in the scene. All of its functionality can be accessed via PlaylistManager.Main</summary>
    public class PlaylistManager : MonoBehaviour
    {
#if UNITY_EDITOR
        public bool CollapseLibrary;
#endif

        /// <summary>Marks the GameObject as DontDestroyOnLoad to provide the manager with scene persistence (Note: manager must be present on a root GameObject for this functionality to work).</summary>
        [Tooltip(
            "Marks the GameObject as DontDestroyOnLoad to provide the manager with scene persistence (Note: manager must be present on a root GameObject for this functionality to work).")]
        [SerializeField]
        private bool ScenePersistent;

        /// <summary>Library of Playlists stored on the PlaylistManager so that Playlist can be started by name without needing a reference to the Playlist..</summary>
        [Tooltip(
            "Library of Playlists stored on the PlaylistManager so that Playlist can be started by name without needing a reference to the Playlist.")]
        public Playlist[] PlaylistLibrary = new Playlist[0];

        /// <summary>The Playlist currently being played by the PlaylistManager.</summary>
        private Playlist TargetPlaylist;

        /// <summary>Static access point to the PlaylistManager object active in the scene.</summary>
        public static PlaylistManager Main;

        /// <summary>The current coroutine for scheduling Tracks.</summary>
        private Coroutine TrackScheduler;

        /// <summary>The current coroutine for delaying the playing of a Playlist.</summary>
        private Coroutine DelayJob;

        //Initializes
        private void Awake()
        {
            if (Main == null)
            {
                Main = this;
            }
            else
            {
                Destroy(gameObject);
            }

            if (ScenePersistent)
            {
                DontDestroyOnLoad(this);
            }

            MusicManager.OnTrackChangeCallback += ValidateIsPlaying;
            MusicManager.OnTrackStopCallback += ValidateOneShotEnd;
        }

        /// <summary>Validates if this playlist is still being played and stops it if it is not.</summary>
        /// <param name="CurrentManagerTrack">The current Track on the MusicManager.</param>
        private void ValidateIsPlaying(Track CurrentManagerTrack)
        {
            if (TargetPlaylist == null)
            {
                return;
            }

            if (CurrentManagerTrack != TargetPlaylist.CurrentTrack)
            {
                StopAllJobs();
            }
        }

        /// <summary>Validates if this playlist is still being played and stops it if it is not.</summary>
        /// <param name="CurrentManagerTrack">The current Track on the MusicManager.</param>
        private void ValidateOneShotEnd(Track CurrentManagerTrack, bool IsOneShot)
        {
            if (TargetPlaylist == null)
            {
                return;
            }

            if ((CurrentManagerTrack != TargetPlaylist.CurrentTrack &&
                 CurrentManagerTrack != TargetPlaylist.LastTrack) || !IsOneShot)
            {
                StopAllJobs();
            }
        }

        /// <summary>Stops any scheduled jobs.</summary>
        private void StopAllJobs()
        {
            StopDelayJob();
            StopTrackScheduler();
        }

        /// <summary>Stops any current delay jobs.</summary>
        private void StopDelayJob()
        {
            if (DelayJob != null)
            {
                StopCoroutine(DelayJob);
                DelayJob = null;
            }
        }

        /// <summary>Stops any current Track scheduling</summary>
        private void StopTrackScheduler()
        {
            if (TrackScheduler != null)
            {
                StopCoroutine(TrackScheduler);
                TrackScheduler = null;
            }
        }

        /// <summary>Begins playing a Playlist.</summary>
        /// <param name="DesiredPlaylist">The Playlist that will be played.</param>
        /// <param name="Delay">How long to delay the playing of the playlist by.</param>
        [BindableMethod]
        public void Play(Playlist DesiredPlaylist, float Delay = 0)
        {
            if (Delay > 0)
            {
                DelayJob = StartCoroutine(DelayedPlay(DesiredPlaylist, Delay));
            }
            else
            {
                //Resets
                StopAllJobs();

                //Starts Playlist
                TargetPlaylist = DesiredPlaylist;
                TargetPlaylist.Start();

                //Starts Track scheduler
                TrackScheduler = StartCoroutine(PlayPlaylist());
            }
        }

        /// <summary>Delays the start of a playlist.</summary>
        /// <param name="DesiredPlaylist">The Playlist that will be played.</param>
        /// <param name="DelayDuration">The duration of the delay.</param>
        private IEnumerator DelayedPlay(Playlist DesiredPlaylist, float DelayDuration)
        {
            yield return new WaitForUnpausedSeconds(DelayDuration);
            Play(DesiredPlaylist);
        }

        [BindableMethod]
        /// <summary>Stops the current Playlist.</summary>
        public void Stop()
        {
            if (TargetPlaylist != null)
            {
                //Ends current Track if it belongs to this Playlist
                MusicManager.Main.Stop(TargetPlaylist.CurrentTrack);

                //Stops Playlist and Track scheduler
                TargetPlaylist.Stop();
                StopAllJobs();
            }
        }

        /// <summary>Schedules the Tracks in the Playlist to play in succession.</summary>
        private IEnumerator PlayPlaylist()
        {
            //Ends if Playlist is no longer being played
            while (TargetPlaylist != null && TargetPlaylist.IsPlaying)
            {
                //Waits for current Track to end
                yield return new WaitForUnpausedSeconds(TargetPlaylist.CalculateWaitTime());

                //Starts next Track if Playlist is still being played, ends Playlist and scheduler otherwise
                if (TargetPlaylist != null && TargetPlaylist.IsPlaying &&
                    (MusicManager.Main.IsPlaying(TargetPlaylist.CurrentTrack) || !MusicManager.Main.IsPlaying()))
                {
                    TargetPlaylist.PlayNext();
                }
                else
                {
                    Stop();
                }
            }
        }

        /// <summary>Finds the desired Playlist in the PlaylistManager's Library.</summary>
        /// <param name="PlaylistName">The name of the Playlist in the PlaylistLibrary.</param>
        /// <returns>The retrieved Playlist.</returns>
        private Playlist FindPlaylistInLibrary(string PlaylistName)
        {
            Playlist DesiredPlaylist = PlaylistLibrary.FirstOrDefault((Playlist x) => x.PlaylistName == PlaylistName);
            if (DesiredPlaylist == null)
            {
                Debug.LogError("No Playlist named '" + PlaylistName +
                               "' was found in the PlaylistLibrary of the PlaylistManager.");
            }

            return DesiredPlaylist;
        }

        [BindableMethod]
        /// <summary>Begins playing a Playlist from the PlaylistLibrary on the PlaylistManager.</summary>
        /// <param name="PlaylistName">The name of the Playlist in the PlaylistLibrary.</param>
        /// <param name="Delay">How long to delay the playing of the playlist by.</param>
        /// <returns>The Playlist from the PlaylistLibrary that will be played.</returns>
        public Playlist PlayFromLibrary(string PlaylistName, float Delay = 0)
        {
            Playlist DesiredPlaylist = FindPlaylistInLibrary(PlaylistName);
            if (DesiredPlaylist != null)
            {
                Play(DesiredPlaylist, Delay);
            }

            return DesiredPlaylist;
        }
    }
}