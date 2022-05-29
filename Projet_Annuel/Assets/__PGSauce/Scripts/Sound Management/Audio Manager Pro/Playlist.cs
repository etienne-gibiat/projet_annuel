using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGSauce.AudioManagement.External.Utilities;
using PGSauce.Core.Strings;

namespace PGSauce.AudioManagement.External
{
    /// <summary>A Playlist that will play through a list of different Tracks with various different playing modes.</summary>
    [CreateAssetMenu(fileName = "Playlist", menuName = MenuPaths.Audio + "PG Playlist", order = 2)]
    public class Playlist : ScriptableObject
    {
        /// <summary>The different randomization technique used for shuffling Playlists.</summary>
        public enum ShuffleMode
        {
            /// <summary>No randomization.</summary>
            None = 0,

            /// <summary>Chooses each Track at random, ensuring consecutive songs are different.</summary>
            Random = 1,

            /// <summary>Randomizes the Playlist before iterating through, ensuring each Track is played once before randomising again so that the shuffling maximises the time between the same Track being replayed.</summary>
            RandomUnique = 2
        }

        /// <summary>The different modes for how to crossfade Tracks in a Playlist.</summary>
        public enum CrossfadeMode
        {
            /// <summary>Crossfades every Track with a Playlist determined duration.</summary>
            ForcedOn = 0,

            /// <summary>Plays each Track sequentially with no crossfade.</summary>
            ForcedOff = 1,

            /// <summary>Crossfade settings are inherited from the Tracks, and each Track will (not) crossfade with the duration defined in the main Edition.</summary>
            TrackDefined = 2
        }

        /// <summary>(Optional) The name of the Playlist.</summary>
        [Tooltip("(Optional) The name of the Playlist.")]
        public string PlaylistName;

        /// <summary>The shuffling mode used for this Playlist.</summary>
        [Tooltip("The shuffling mode used for this Playlist.")]
        public ShuffleMode Shuffle;

        /// <summary>The crossfading mode for this Playlist.</summary>
        [Tooltip("The crossfading mode for this Playlist.")]
        public CrossfadeMode Crossfade;

        /// <summary>The duration of the crossfade between different Tracks (if Crossfade = ForcedOn).</summary>
        [Tooltip("The duration of the crossfade between different Tracks (if Crossfade = ForcedOn).")]
        public float CrossfadeDuration;

        /// <summary>The duration of the gap between subsequent tracks.</summary>
        [Tooltip("The duration of the gap between subsequent tracks.")]
        public float TrackGapDuration;

        /// <summary>The Tracks to play in this Playlist.</summary>
        [Tooltip("The Tracks to play in this Playlist.")]
        public Track[] Tracks = new Track[0];

        /// <summary>If the Playlist is currently being played (Read Only).</summary>
        public bool IsPlaying { get; private set; }

        /// <summary>The Track currently being played on this Playlist (Read Only).</summary>
        public Track CurrentTrack { get; private set; }

        /// <summary>The Track previously being played on this Playlist (Read Only).</summary>		
        public Track LastTrack { get; private set; }

        /// <summary>The index of the currently playing Track in the list.</summary>
        private int TrackID;

        /// <summary>The index of the next Track to be played.</summary>
        private int NextTrackID;

        /// <summary>A list of all the Tracks that will be played.</summary>
        private List<Track> TracksToPlay;

        public override string ToString()
        {
            string NameString = PlaylistName;
            if (System.String.IsNullOrEmpty(PlaylistName))
            {
                NameString = name;
            }

            return NameString;
        }

        private void OnEnable()
        {
            Reset();
        }

        private void OnDisable()
        {
            Reset();
        }

        /// <summary>Resets the Playlist to its untampered state.</summary>
        private void Reset()
        {
            CurrentTrack = null;
            LastTrack = null;
            IsPlaying = false;
            TrackID = 0;
            NextTrackID = 0;
        }

        /// <summary>Initialises and starts the Playlist.</summary>
        public void Start()
        {
            //Initialises
            IsPlaying = true;
            TracksToPlay = new List<Track>(Tracks);

            //Selects the Track to play first
            switch (Shuffle)
            {
                case ShuffleMode.Random:
                    TrackID = TracksToPlay.RandomIndex();
                    break;
                case ShuffleMode.RandomUnique:
                    TracksToPlay.Randomize();
                    goto case ShuffleMode.None;
                case ShuffleMode.None:
                    TrackID = 0;
                    break;
            }

            NextTrackID = TrackID;
            LastTrack = CurrentTrack;
            CurrentTrack = TracksToPlay[TrackID];

            //Starts playing the Playlist
            MusicManager.Main.Play(CurrentTrack, 0, true);
            SelectNext();
        }

        /// <summary>Stops the current Playlist from playing.</summary>
        public void Stop()
        {
            Reset();
        }

        /// <summary>Calculates the duration of time to wait before playing the next queued Track.</summary>
        /// <returns>The duration of time to wait.</returns>
        public float CalculateWaitTime()
        {
            //Waits for duation of Track
            Edition CurrentEdition = CurrentTrack.Editions[0];
            float WaitTime = CurrentEdition.Soundtrack.length;
            if (CurrentTrack.Intro != null)
            {
                //Adds on time for intro
                WaitTime += CurrentTrack.Intro.length;
                WaitTime -= CurrentTrack.OverlapDuration;
            }

            switch (Crossfade)
            {
                //Subtracts any time used for crossfading
                case CrossfadeMode.ForcedOn:
                    WaitTime -= CrossfadeDuration;
                    break;
                case CrossfadeMode.TrackDefined:
                    WaitTime -= TracksToPlay[NextTrackID].Editions[0].FadeLength;
                    break;
                case CrossfadeMode.ForcedOff:
                    WaitTime += TrackGapDuration;
                    break;
            }

            return WaitTime;
        }

        /// <summary>Plays the next queued Track in the Playlist.</summary>
        public void PlayNext()
        {
            //Plays the Track
            TrackID = NextTrackID;
            LastTrack = CurrentTrack;
            CurrentTrack = TracksToPlay[TrackID];
            switch (Crossfade)
            {
                case CrossfadeMode.ForcedOn:
                    MusicManager.Main.Play(CurrentTrack, 0, CrossfadeDuration, true);
                    break;
                case CrossfadeMode.ForcedOff:
                    MusicManager.Main.Play(CurrentTrack, 0, 0, true);
                    break;
                case CrossfadeMode.TrackDefined:
                    MusicManager.Main.Play(CurrentTrack, 0, true);
                    break;
            }

            //Selects the next Track to be played
            SelectNext();
        }

        /// <summary>Selects which Track will be played next.</summary>
        private void SelectNext()
        {
            //Choses randomly until a different Track is selected
            if (Tracks.Length == 1)
            {
                NextTrackID = 0;
            }
            else
            {
                if (Shuffle == ShuffleMode.Random)
                {
                    while (TrackID == NextTrackID)
                    {
                        NextTrackID = TracksToPlay.RandomIndex();
                    }
                }
                else
                {
                    NextTrackID++;
                    if (NextTrackID >= TracksToPlay.Count)
                    {
                        //Loops round when Playlist is complete
                        NextTrackID -= TracksToPlay.Count;
                        //Randomizes the order until a different Track is selected
                        if (Shuffle == ShuffleMode.RandomUnique)
                        {
                            do
                            {
                                TracksToPlay.Randomize();
                            } while (TracksToPlay[NextTrackID] == CurrentTrack);
                        }
                    }
                }
            }
        }
    }
}
