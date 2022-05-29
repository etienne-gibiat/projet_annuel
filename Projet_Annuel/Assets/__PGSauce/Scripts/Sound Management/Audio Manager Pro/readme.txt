Audio Manager Pro V1.1.7

Thank you for purchasing this asset, I hope it finds you well. If you have any questions, suggestions or problems please contact me at:
Email: support@qfsw.co.uk
Discord: https://discord.gg/g8SJ7X6
Issue Tracker: https://bitbucket.org/QFSW/audio-manager-pro/issues
Twitter: https://twitter.com/QFSW1024https://twitter.com/QFSW1024


1. General Notes
In general, all of AMPs functionality should be accessed via the managers; e.g. you should always use MusicManager.Main.Play(Track) as opposed to Track.Play() to ensure that AMP keeps track of everything for you.
If you wish to bypass the managers and use the functionality of the individual scripts themselves (e.g. Edition, SFXLayer) then full management and protection is not guaranteed, and functionality is undefined.
To create any objects used by AMP such as SFXObjects, Tracks etc. it is recommended to use the editor instead of doing it via C#; C# initialisation is possible, however, for cases where runtime creation is necessary.


2. Setting Up

2.1.a. Music Manager
In order to use the Music Manager and Track functionality of AMP, you must first add a MusicManager component to a GameObject in your scene. This can be done by clicking Add Component then searching for MusicManager.
You should only ever have one MusicManager present, as all of its functionality can be statically accessed via MusicManager.Main

2.1.b. Track
AMP uses a Track system to handle all of its music functionality. To create a new Track, go to the assets view and click Create -> Audio Manager Pro -> Track.
From here, you can setup your Track with its intro sound, volumes, different editions etc.

2.2.a. Playlist Manager
AMP provides you with a system to have automated playlist handling, enabling you to create collections of Tracks that can be managed for you.
To use this, you must add a PlaylistManager to the scene, and all of its functionality will be accessible from PlaylistManager.Main.
Note: A MusicManager must also be present in the scene for the PlaylistManager to function.

2.2.b. Playlists
In order to use the Playlist manager you must create a Playlist object. You can do this by clicking Create -> Audio Manager Pro -> Playlist.
Here you will be able to choose all of the Tracks you would like to use in the Playlist, as well as various playback modes and settings for the playlist.

2.3.a. SFXManager
In order to use the Sound Effect Manager and SFX functionality of AMP, you must first add a SFXManager component to a GameObject in your scene. This can be done by clicking Add Component then searching for SFXManager.
You should only ever have one SFXManager present, as all of its functionality can be statically accessed via SFXManager.Main

2.3.b. SFXObject
AMP uses SFXObjects to store all the information about sound effects. Each SFXObject contains global sound settings like volume and pitch multipliers, in addition to individual layers (SFXLayers) enabling you to create multilayer sound effects.
Each SFXLayer will contain its own set of settings giving you the full power over your sound effects.
To create a new SFXObject, go to the assets view and click Create -> Audio Manager Pro -> SFXObject.

2.3.c. SFXGroup
AMP also allows you to group multiple different SFXObjects into an SFXGroup. By doing this, you can have a collection of similar SFXObjects in one SFXGroup (e.g. explosions), enabling you to have AMP play a random SFXObject from the SFXGroup for you.
This makes it easier to create sound vareity in your game.
To create a new SFXGroup, go to the assets view and click Create -> Audio Manager Pro -> SFXGroup. Here you will be able to choose which SFXObjects go into the group, as well as various playback modes and settings.


3. MusicManager Usage

3.1. Playing a Track
In order to play a Track in AMP, you must use one of the following overloads:
MusicManager.Main.Play(Track DesiredTrack);
MusicManager.Main.Play(Track DesiredTrack, float Delay, bool IsOneShot = false);
MusicManager.Main.Play(Track DesiredTrack, float Delay, float FadeDuration, bool IsOneShot = false);
By default, the Track will be played instantly with a crossfade of the length of the fade in duration specified in the main edition of the Track.
Playing Tracks will automatically fade out / stop any current playing Track, and will loop the new one forever until stopped or a different track is started.
To play a Track without looping it, use IsOneShot = true;

3.2. Switching Edition
AMP provides a feature called Editions; these allow you to have many different variants of your song or sections of it (e.g. indoors and outdoors version) so that you can easily switch edition of the song at runtime.
To switch Editions of the currently playing Track, you must use one of the following overloads:
MusicManager.Main.SwitchEdition(int EditionIndex, float Delay = 0);
MusicManager.Main.SwitchEdition(int EditionIndex, float Delay, float FadeOverride);
MusicManager.Main.SwitchEdition(string EditionName, float Delay = 0);
MusicManager.Main.SwitchEdition(string EditionName, float Delay, float FadeOverride);
MusicManager.Main.SwitchEdition(Edition DesiredEdition);
MusicManager.Main.SwitchEdition(Edition DesiredEdition, float Delay);
MusicManager.Main.SwitchEdition(Edition DesiredEdition, float Delay, float FadeOverride);
By default, the Edition switch will start instantly, and will use a crossfade of length "fade in duration" as defined for the target Edition

3.3. Stopping Playback
AMP provides you with several different ways to stop playback. To end the current playing Track, use one of the following overloads:
MusicManager.Main.Stop();
MusicManager.Main.Stop(Track AudioTrack);
MusicManager.Main.Stop(float FadeDuration);
MusicManager.Main.Stop(Track AudioTrack, float FadeDuration);
By default, it will stop the currently playing Track instantly. If a duration is specified, it will fade out. If a Track is specified, it will only stop if the specified Track is being played.
Additionally, AMP also allows you restart the current Track in a similar fashion:
MusicManager.Main.Restart();
MusicManager.Main.Restart(Track AudioTrack);
MusicManager.Main.Restart(float FadeDuration);
MusicManager.Main.Restart(Track AudioTrack, float FadeDuration);

3.4. Pausing Playback
To Pause and Unpause playback in AMP, simply use MusicManager.Main.Pause(); and MusicManager.Main.Unpause(); all crossfades, sounds etc will be paused for you.
The current pause status can be determined by the property MusicManager.Main.Paused

3.5. Changing the Volume of the Playback
AMP allows you to change the global volume of the MusicManager easily with an optional fade effect:
MusicManager.Main.SetVolume(float Volume);
MusicManager.Main.SetVolume(float Volume, float FadeDuration);
The current volume of the MusicManager can be determined by MusicManager.Main.MasterVolume

3.6. Extra Utilities
AMP provides extra utilities to the user for the MusicManager; none of these are required for most users, but are available for those that need them.
Internally, AMP uses a pooling system to minimise the number of GameObjects needed to handle all the current sounds.
If for any reason you wish to remove the extra inactive objects that are not currently being used but are held on to for pooling, use MusicManager.Main.RemoveInactive() to destroy them.
If you need to inject your own custom code into the MusicManager and do not wish to modify the source code, then there are two static callbacks available:
MusicManager.OnTrackChangeCallback (Action<Track>) - The Track will be the new Track that has started playing.
MusicManager.OnTrackStopCallback (Action<Track, bool>) - The Track will be the Track that has just stopped playing, and the bool will be true if the stop was to end a OneShot Track playback.


4. SFXManager Usage

4.1. Playing an SFXObject
AMP supports both 2D and 3D playback of SFXObjects. In 3D mode, a parent transform may be specified.
For 2D playback, use one of the following overloads:
SFXManager.Main.Play(SFXObject SFX);
SFXManager.Main.Play(SFXObject SFX, float DelayDuration);
SFXManager.Main.Play(SFXObject SFX, float DelayDuration, float Volume);
SFXManager.Main.Play(SFXObject SFX, float DelayDuration, float Volume, float Pitch);
All of these functions return a Coroutine for the current delay; if you wish to stop a single scheduled sound then you should cache this coroutine.
These functions are suited for 2D audio playback, as they will leave the AudioObject under the root SFXManager.
By default, volume pitch and delay will be as defined on the SFXObject
For 3D playback, use one of the following overloads:
SFXManager.Main.Play(SFXObject SFX, Vector3 Position, Transform Parent = null, bool IsGlobalPosition = true);
SFXManager.Main.Play(SFXObject SFX, Vector3 Position, float DelayDuration, Transform Parent = null, bool IsGlobalPosition = true);
SFXManager.Main.Play(SFXObject SFX, Vector3 Position, float DelayDuration, float Volume, Transform Parent = null, bool IsGlobalPosition = true);
SFXManager.Main.Play(SFXObject SFX, Vector3 Position, float DelayDuration, float Volume, float Pitch, Transform Parent = null, bool IsGlobalPosition = true);

4.2. Playing an SFXGroup
In addition to SFXObjects, AMP also provides you with SFXGroups. These allow you to play a collection of similar SFXObjects, and have AMP chose one at random for you.
These are used in a very similar fashion to SFXObjects:
SFXManager.Main.Play(SFXGroup SFX);
SFXManager.Main.Play(SFXGroup SFX, float DelayDuration);
SFXManager.Main.Play(SFXGroup SFX, float DelayDuration, float Volume);
SFXManager.Main.Play(SFXGroup SFX, float DelayDuration, float Volume, float Pitch);
SFXManager.Main.Play(SFXGroup SFX, Vector3 Position, Transform Parent = null, bool IsGlobalPosition = true);
SFXManager.Main.Play(SFXGroup SFX, Vector3 Position, float DelayDuration, Transform Parent = null, bool IsGlobalPosition = true);
SFXManager.Main.Play(SFXGroup SFX, Vector3 Position, float DelayDuration, float Volume, Transform Parent = null, bool IsGlobalPosition = true);
SFXManager.Main.Play(SFXGroup SFX, Vector3 Position, float DelayDuration, float Volume, float Pitch, Transform Parent = null, bool IsGlobalPosition = true);
Similarly, playing an SFXGroup will also return a Coroutine

4.3. Stopping Playback
To stop all currently playing sounds in AMP, you must use SFXManager.Main.StopAll();

4.4. Pausing Playback
To Pause and Unpause playback in AMP, simply use SFXManager.Main.Pause(); and SFXManager.Main.Unpause(); all crossfades, sounds etc will be paused for you.
The current pause status can be determined by the property SFXManager.Main.Paused

4.5. Changing the Volume of the Playback
AMP allows you to change the global volume of the SFXManager easily with an optional fade effect:
SFXManager.Main.SetVolume(float Volume);
SFXManager.Main.SetVolume(float Volume, float FadeDuration);
The current volume of the SFXManager can be determined by SFXManager.Main.MasterVolume


5. PlaylistManager Usage

5.1. Playing a Playlist
To use the PlaylistManager, a MusicManager must also be present.
To start a Playlist, simply use PlaylistManager.Main.Play(Playlist DesiredPlaylist, float Delay = 0);
This will start the Track scheduler, which will automatically play and stop Tracks as needed.

5.2. Stopping a Playlist
If the PlaylistManager detects that another source (e.g. your code) has stopped or started a Track on the MusicManager, then it will end the Track schedule and resume control to the MusicManager.
If you would like to force the PlaylistManager to stop, then simply use PlaylistManager.Main.Stop();


6. Libray Playback

6.1. Initialisation
AMP provides a feature set known as Library Playback; this allows you to play a Track/Playlist/SFXObject/SFXGroup from anywhere without having an actual object reference.
To setup the libraries, simply populate the arrays in the corresponding Manager's inspector with the desired Tracks/Playlists/SFXObjects/SFXGroups; please ensure that the name field is completed, as this will be used to find the Track/Playlist/SFXObject/SFXGroup.

6.2. Playback

6.2.a. MusicManager
To play a Track using the library feature, you must use on of the following overloads:
MusicManager.Main.PlayFromLibrary(string TrackName);
MusicManager.Main.PlayFromLibrary(string TrackName, float Delay, bool IsOneShot = false);
MusicManager.Main.PlayFromLibrary(string TrackName, float Delay, float FadeDuration, bool IsOneShot = false);
All of these will return the Track that has been selected from the Library.

6.2.b. SFXManager
To play an SFXObject using the library feature, you must use on of the following overloads:
SFXManager.Main.PlayFromSFXObjectLibrary(string SFXName);
SFXManager.Main.PlayFromSFXObjectLibrary(string SFXName, float DelayDuration);
SFXManager.Main.PlayFromSFXObjectLibrary(string SFXName, float DelayDuration, float Volume);
SFXManager.Main.PlayFromSFXObjectLibrary(string SFXName, float DelayDuration, float Volume, float Pitch);
SFXManager.Main.PlayFromSFXObjectLibrary(string SFXName, Vector3 Position, Transform Parent = null, bool IsGlobalPosition = true);
SFXManager.Main.PlayFromSFXObjectLibrary(string SFXName, Vector3 Position, float DelayDuration, Transform Parent = null, bool IsGlobalPosition = true);
SFXManager.Main.PlayFromSFXObjectLibrary(string SFXName, Vector3 Position, float DelayDuration, float Volume, Transform Parent = null, bool IsGlobalPosition = true);
SFXManager.Main.PlayFromSFXObjectLibrary(string SFXName, Vector3 Position, float DelayDuration, float Volume, float Pitch, Transform Parent = null, bool IsGlobalPosition = true);
All of these will return the Coroutine that the standard Play functions would return.
To play an SFXGroup using the library feature, you must use on of the following overloads:
SFXManager.Main.PlayFromSFXGroupLibrary(string SFXName);
SFXManager.Main.PlayFromSFXGroupLibrary(string SFXName, float DelayDuration);
SFXManager.Main.PlayFromSFXGroupLibrary(string SFXName, float DelayDuration, float Volume);
SFXManager.Main.PlayFromSFXGroupLibrary(string SFXName, float DelayDuration, float Volume, float Pitch);
SFXManager.Main.PlayFromSFXGroupLibrary(string SFXName, Vector3 Position, Transform Parent = null, bool IsGlobalPosition = true);
SFXManager.Main.PlayFromSFXGroupLibrary(string SFXName, Vector3 Position, float DelayDuration, Transform Parent = null, bool IsGlobalPosition = true);
SFXManager.Main.PlayFromSFXGroupLibrary(string SFXName, Vector3 Position, float DelayDuration, float Volume, Transform Parent = null, bool IsGlobalPosition = true);
SFXManager.Main.PlayFromSFXGroupLibrary(string SFXName, Vector3 Position, float DelayDuration, float Volume, float Pitch, Transform Parent = null, bool IsGlobalPosition = true);
All of these will return the Coroutine that the standard Play functions would return.

6.2.c. PlaylistManager
To play a Playlist using the library feature, you must use PlaylistManager.Main.PlayFromLibrary(string PlaylistName, float Delay = 0);
