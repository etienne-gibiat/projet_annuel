using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using PGSauce.AudioManagement.External;
using PGSauce.AudioManagement.External.Coroutines.SFX;
using PGSauce.AudioManagement.External.EventBinding;

/// <summary>The SFXManager handles all the sound effects in the scene. The component should only be used once in the scene, and all of its functionality can be accessed via SFXManager.Main</summary>
public class SFXManager : SoundManager
{
    #region Initialization
#if UNITY_EDITOR
    public bool CollapseObjectLibrary;
    public bool CollapseGroupLibrary;
#endif

    /// <summary>The type of audio this sound manager handles.</summary>
    public override AudioObject.SoundType ManagerType { get { return AudioObject.SoundType.SFX; } }

    /// <summary>Library of SFXObjects stored on the SFXManager so that SFXObjects can be started by name without needing a reference to the SFXObject.</summary>
    [Tooltip("Library of SFXObjects stored on the SFXManager so that SFXObjects can be started by name without needing a reference to the SFXObject.")]
    public SFXObject[] SFXObjectLibrary = new SFXObject[0];

    /// <summary>Library of SFXGroups stored on the SFXManager so that SFXGroups can be started by name without needing a reference to the SFXGroup.</summary>
    [Tooltip("Library of SFXGroups stored on the SFXManager so that SFXGroups can be started by name without needing a reference to the SFXGroup.")]
    public SFXGroup[] SFXGroupLibrary = new SFXGroup[0];

    /// <summary>Static access point to the SFXManager object active in the scene.</summary>
    [HideInInspector]
    public static SFXManager Main;

    /// <summary>Public callback hook for when Stop All is invoked.</summary>
    public System.Action OnStopAll = () => { };

    /// <summary>All current Coroutines for delaying the start of an SFXObject/Layers.</summary>
    private List<Coroutine> SequencedStartJobs = new List<Coroutine>();
    /// <summary>All current Coroutines for delaying the end of an SFXLayer.</summary>
    private List<Coroutine> SequencedEndJobs = new List<Coroutine>();

    //Initializes
    protected override void Awake()
    {
        base.Awake();
        Parent.name = "SFX Objects";

        if (Main == null)
        {
            Main = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion

    #region LibraryPlayback
    /// <summary>Finds the desired SFXObject in the SFXManager's Library.</summary>
    /// <param name="SFXName">The name of the SFXObject in the SFXObjectLibrary.</param>
    /// <returns>The retrieved SFXObject.</returns>
    [BindableMethod]
    private SFXObject FindSFXObjectInLibrary(string SFXName)
    {
        SFXObject DesiredSFXObject = SFXObjectLibrary.FirstOrDefault((SFXObject x) => x.SFXName == SFXName);
        if (DesiredSFXObject == null) { Debug.LogError("No SFXObject named '" + SFXName + "' was found in the SFXObjectLibrary of the SFXManager."); }
        return DesiredSFXObject;
    }

    /// <summary>Finds the desired SFXGroup in the SFXManager's Library.</summary>
    /// <param name="SFXName">The name of the SFXGroup in the SFXGroupLibrary.</param>
    /// <returns>The retrieved SFXGroup.</returns>
    [BindableMethod]
    private SFXGroup FindSFXGroupInLibrary(string SFXName)
    {
        SFXGroup DesiredSFXGroup = SFXGroupLibrary.FirstOrDefault((SFXGroup x) => x.GroupName == SFXName);
        if (DesiredSFXGroup == null) { Debug.LogError("No SFXGroup named '" + SFXName + "' was found in the SFXGroupLibrary of the SFXManager."); }
        return DesiredSFXGroup;
    }

    /// <summary>Plays an SFXObject from the SFXObjectLibrary on the SFXManager with the default delay settings.</summary>
    /// <param name="SFXName">The name of the SFXObject in the SFXObjectLibrary.</param>
    /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
    [BindableMethod]
    public Coroutine PlayFromSFXObjectLibrary(string SFXName)
    {
        SFXObject DesiredSFXObject = FindSFXObjectInLibrary(SFXName);
        if (DesiredSFXObject != null) { return Play(DesiredSFXObject); }
        else { return null; }
    }

    [BindableMethod]
    /// <summary>Plays an SFXObject from the SFXObjectLibrary on the SFXManager.</summary>
    /// <param name="SFXName">The name of the SFXObject in the SFXObjectLibrary.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXObject.</param>
    /// <returns>The Coroutine for delaying this SFXObject.</returns
    public Coroutine PlayFromSFXObjectLibrary(string SFXName, float DelayDuration)
    {
        SFXObject DesiredSFXObject = FindSFXObjectInLibrary(SFXName);
        if (DesiredSFXObject != null) { return Play(DesiredSFXObject, DelayDuration); }
        else { return null; }
    }

    [BindableMethod]
    /// <summary>Plays an SFXObject from the SFXObjectLibrary on the SFXManager.</summary>
    /// <param name="SFXName">The name of the SFXObject in the SFXObjectLibrary.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXObject.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <returns>The Coroutine for delaying this SFXObject.</returns
    public Coroutine PlayFromSFXObjectLibrary(string SFXName, float DelayDuration, float Volume)
    {
        SFXObject DesiredSFXObject = FindSFXObjectInLibrary(SFXName);
        if (DesiredSFXObject != null) { return Play(DesiredSFXObject, DelayDuration, Volume); }
        else { return null; }
    }

    [BindableMethod]
    /// <summary>Plays an SFXObject from the SFXObjectLibrary on the SFXManager.</summary>
    /// <param name="SFXName">The name of the SFXObject in the SFXObjectLibrary.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXObject.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Pitch">The pitch multiplier of the SFXObject to be used.</param>
    /// <returns>The Coroutine for delaying this SFXObject.</returns
    public Coroutine PlayFromSFXObjectLibrary(string SFXName, float DelayDuration, float Volume, float Pitch)
    {
        SFXObject DesiredSFXObject = FindSFXObjectInLibrary(SFXName);
        if (DesiredSFXObject != null) { return Play(DesiredSFXObject, DelayDuration, Volume, Pitch); }
        else { return null; }
    }

    [BindableMethod]
    /// <summary>Plays an SFXObject from the SFXObjectLibrary on the SFXManager with the default delay settings in 3D space.</summary>
    /// <param name="SFXName">The name of the SFXObject in the SFXObjectLibrary.</param>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
    public Coroutine PlayFromSFXObjectLibrary(string SFXName, Vector3 Position, Transform Parent = null, bool IsGlobalPosition = true)
    {
        SFXObject DesiredSFXObject = FindSFXObjectInLibrary(SFXName);
        if (DesiredSFXObject != null) { return Play(DesiredSFXObject, Position, Parent, IsGlobalPosition); }
        else { return null; }
    }

    [BindableMethod]
    /// <summary>Plays an SFXObject from the SFXObjectLibrary on the SFXManager in 3D space.</summary>
    /// <param name="SFXName">The name of the SFXObject in the SFXObjectLibrary.</param>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXObject.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying this SFXObject.</returns
    public Coroutine PlayFromSFXObjectLibrary(string SFXName, Vector3 Position, float DelayDuration, Transform Parent = null, bool IsGlobalPosition = true)
    {
        SFXObject DesiredSFXObject = FindSFXObjectInLibrary(SFXName);
        if (DesiredSFXObject != null) { return Play(DesiredSFXObject, Position, DelayDuration, Parent, IsGlobalPosition); }
        else { return null; }
    }

    [BindableMethod]
    /// <summary>Plays an SFXObject from the SFXObjectLibrary on the SFXManager in 3D space.</summary>
    /// <param name="SFXName">The name of the SFXObject in the SFXObjectLibrary.</param>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXObject.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying this SFXObject.</returns
    public Coroutine PlayFromSFXObjectLibrary(string SFXName, Vector3 Position, float DelayDuration, float Volume, Transform Parent = null, bool IsGlobalPosition = true)
    {
        SFXObject DesiredSFXObject = FindSFXObjectInLibrary(SFXName);
        if (DesiredSFXObject != null) { return Play(DesiredSFXObject, Position, DelayDuration, Volume, Parent, IsGlobalPosition); }
        else { return null; }
    }

    [BindableMethod]
    /// <summary>Plays an SFXObject from the SFXObjectLibrary on the SFXManager in 3D space.</summary>
    /// <param name="SFXName">The name of the SFXObject in the SFXObjectLibrary.</param>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXObject.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Pitch">The pitch multiplier of the SFXObject to be used.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying this SFXObject.</returns
    public Coroutine PlayFromSFXObjectLibrary(string SFXName, Vector3 Position, float DelayDuration, float Volume, float Pitch, Transform Parent = null, bool IsGlobalPosition = true)
    {
        SFXObject DesiredSFXObject = FindSFXObjectInLibrary(SFXName);
        if (DesiredSFXObject != null) { return Play(DesiredSFXObject, Position, DelayDuration, Volume, Pitch, Parent, IsGlobalPosition); }
        else { return null; }
    }

    [BindableMethod]
    /// <summary>Plays an SFXGroup from the SFXGroupLibrary on the SFXManager with the default delay settings.</summary>
    /// <param name="SFXName">The name of the SFXGroup in the SFXGroupLibrary.</param>
    /// <returns>The Coroutine for delaying this SFXGroup (null if no delay is applied).</returns>
    public Coroutine PlayFromSFXGroupLibrary(string SFXName)
    {
        SFXGroup DesiredSFXGroup = FindSFXGroupInLibrary(SFXName);
        if (DesiredSFXGroup != null) { return Play(DesiredSFXGroup); }
        else { return null; }
    }

    [BindableMethod]
    /// <summary>Plays an SFXGroup from the SFXGroupLibrary on the SFXManager with the default delay settings.</summary>
    /// <param name="SFXName">The name of the SFXGroup in the SFXGroupLibrary.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXGroup.</param>
    /// <returns>The Coroutine for delaying this SFXGroup (null if no delay is applied).</returns>
    public Coroutine PlayFromSFXGroupLibrary(string SFXName, float DelayDuration)
    {
        SFXGroup DesiredSFXGroup = FindSFXGroupInLibrary(SFXName);
        if (DesiredSFXGroup != null) { return Play(DesiredSFXGroup, DelayDuration); }
        else { return null; }
    }

    [BindableMethod]
    /// <summary>Plays an SFXGroup from the SFXGroupLibrary on the SFXManager with the default delay settings.</summary>
    /// <param name="SFXName">The name of the SFXGroup in the SFXGroupLibrary.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXGroup.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <returns>The Coroutine for delaying this SFXGroup (null if no delay is applied).</returns>
    public Coroutine PlayFromSFXGroupLibrary(string SFXName, float DelayDuration, float Volume)
    {
        SFXGroup DesiredSFXGroup = FindSFXGroupInLibrary(SFXName);
        if (DesiredSFXGroup != null) { return Play(DesiredSFXGroup, DelayDuration, Volume); }
        else { return null; }
    }

    [BindableMethod]
    /// <summary>Plays an SFXGroup from the SFXGroupLibrary on the SFXManager with the default delay settings.</summary>
    /// <param name="SFXName">The name of the SFXGroup in the SFXGroupLibrary.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXGroup.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Pitch">The pitch multiplier of the SFXObject to be used.</param>
    /// <returns>The Coroutine for delaying this SFXGroup (null if no delay is applied).</returns>
    public Coroutine PlayFromSFXGroupLibrary(string SFXName, float DelayDuration, float Volume, float Pitch)
    {
        SFXGroup DesiredSFXGroup = FindSFXGroupInLibrary(SFXName);
        if (DesiredSFXGroup != null) { return Play(DesiredSFXGroup, DelayDuration, Volume, Pitch); }
        else { return null; }
    }

    [BindableMethod]
    /// <summary>Plays an SFXGroup from the SFXGroupLibrary on the SFXManager with the default delay settings in 3D space.</summary>
    /// <param name="SFXName">The name of the SFXGroup in the SFXGroupLibrary.</param>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying this SFXGroup (null if no delay is applied).</returns>
    public Coroutine PlayFromSFXGroupLibrary(string SFXName, Vector3 Position, Transform Parent = null, bool IsGlobalPosition = true)
    {
        SFXGroup DesiredSFXGroup = FindSFXGroupInLibrary(SFXName);
        if (DesiredSFXGroup != null) { return Play(DesiredSFXGroup, Position, Parent, IsGlobalPosition); }
        else { return null; }
    }

    [BindableMethod]
    /// <summary>Plays an SFXGroup from the SFXGroupLibrary on the SFXManager with the default delay settings in 3D space.</summary>
    /// <param name="SFXName">The name of the SFXGroup in the SFXGroupLibrary.</param>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXGroup.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying this SFXGroup (null if no delay is applied).</returns>
    public Coroutine PlayFromSFXGroupLibrary(string SFXName, Vector3 Position, float DelayDuration, Transform Parent = null, bool IsGlobalPosition = true)
    {
        SFXGroup DesiredSFXGroup = FindSFXGroupInLibrary(SFXName);
        if (DesiredSFXGroup != null) { return Play(DesiredSFXGroup, Position, DelayDuration, Parent, IsGlobalPosition); }
        else { return null; }
    }

    [BindableMethod]
    /// <summary>Plays an SFXGroup from the SFXGroupLibrary on the SFXManager with the default delay settings in 3D space.</summary>
    /// <param name="SFXName">The name of the SFXGroup in the SFXGroupLibrary.</param>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXGroup.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying this SFXGroup (null if no delay is applied).</returns>
    public Coroutine PlayFromSFXGroupLibrary(string SFXName, Vector3 Position, float DelayDuration, float Volume, Transform Parent = null, bool IsGlobalPosition = true)
    {
        SFXGroup DesiredSFXGroup = FindSFXGroupInLibrary(SFXName);
        if (DesiredSFXGroup != null) { return Play(DesiredSFXGroup, Position, DelayDuration, Volume, Parent, IsGlobalPosition); }
        else { return null; }
    }

    [BindableMethod]
    /// <summary>Plays an SFXGroup from the SFXGroupLibrary on the SFXManager with the default delay settings in 3D space.</summary>
    /// <param name="SFXName">The name of the SFXGroup in the SFXGroupLibrary.</param>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXGroup.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Pitch">The pitch multiplier of the SFXObject to be used.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying this SFXGroup (null if no delay is applied).</returns>
    public Coroutine PlayFromSFXGroupLibrary(string SFXName, Vector3 Position, float DelayDuration, float Volume, float Pitch, Transform Parent = null, bool IsGlobalPosition = true)
    {
        SFXGroup DesiredSFXGroup = FindSFXGroupInLibrary(SFXName);
        if (DesiredSFXGroup != null) { return Play(DesiredSFXGroup, Position, DelayDuration, Volume, Pitch, Parent, IsGlobalPosition); }
        else { return null; }
    }
    #endregion

    #region Playback
    [BindableMethod]
    /// <summary>Plays an SFXObject with the default delay settings.</summary>
    /// <param name="SFX">The SFXObject to play.</param>
    /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
    public Coroutine Play(SFXObject SFX) { return Play(SFX, SFX.GetDelayDuration()); }

    [BindableMethod]
    /// <summary>Plays an SFXObject.</summary>
    /// <param name="SFX">The SFXObject to play.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXObject.</param>
    /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
    public Coroutine Play(SFXObject SFX, float DelayDuration) { return SFX.Play(DelayDuration); }

    [BindableMethod]
    /// <summary>Plays an SFXObject.</summary>
    /// <param name="SFX">The SFXObject to play.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXObject.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
    public Coroutine Play(SFXObject SFX, float DelayDuration, float Volume) { return SFX.Play(DelayDuration, Volume); }

    [BindableMethod]
    /// <summary>Plays an SFXObject.</summary>
    /// <param name="SFX">The SFXObject to play.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXObject.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Pitch">The pitch multiplier of the SFXObject to be used.</param>
    /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
    public Coroutine Play(SFXObject SFX, float DelayDuration, float Volume, float Pitch) { return SFX.Play(DelayDuration, Volume, Pitch); }

    [BindableMethod]
    /// <summary>Plays an SFXObject with the default delay settings.</summary>
    /// <param name="SFX">The SFXObject to play.</param>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
    public Coroutine Play(SFXObject SFX, Vector3 Position, Transform Parent = null, bool IsGlobalPosition = true) { return Play(SFX, Position, SFX.GetDelayDuration(), Parent, IsGlobalPosition); }

    [BindableMethod]
    /// <summary>Plays an SFXObject.</summary>
    /// <param name="SFX">The SFXObject to play.</param>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXObject.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
    public Coroutine Play(SFXObject SFX, Vector3 Position, float DelayDuration, Transform Parent = null, bool IsGlobalPosition = true) { return SFX.Play(Position, DelayDuration, Parent, IsGlobalPosition); }

    [BindableMethod]
    /// <summary>Plays an SFXObject.</summary>
    /// <param name="SFX">The SFXObject to play.</param>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXObject.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
    public Coroutine Play(SFXObject SFX, Vector3 Position, float DelayDuration, float Volume, Transform Parent = null, bool IsGlobalPosition = true) { return SFX.Play(Position, DelayDuration, Volume, Parent, IsGlobalPosition); }
    
    /// <summary>Plays an SFXObject.</summary>
    /// <param name="SFX">The SFXObject to play.</param>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXObject.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Pitch">The pitch multiplier of the SFXObject to be used.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying this SFXObject (null if no delay is applied).</returns>
    [BindableMethod]
    public Coroutine Play(SFXObject SFX, Vector3 Position, float DelayDuration, float Volume, float Pitch, Transform Parent = null, bool IsGlobalPosition = true) { return SFX.Play(Position, DelayDuration, Volume, Pitch, Parent, IsGlobalPosition); }

    /// <summary>Delays the playing of an SFXObject.</summary>
    /// <param name="SFX">The SFXObject to play.</param>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXObject.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Pitch">The pitch multiplier of the SFXObject to be used.</param>
    /// <returns>The Coroutine for delaying this SFXObject.</returns>
    public Coroutine PlaySuspended(SFXObject SFX, float DelayDuration, float Volume, float Pitch)
    {
        SequencedStartJobs.RemoveAll((Coroutine x) => x == null);
        Coroutine StartJob = StartCoroutine(DelayedPlay(SFX, DelayDuration, Volume, Pitch));
        SequencedStartJobs.Add(StartJob);
        return StartJob;
    }

    /// <summary>Delays the playing of an SFXObject.</summary>
    /// <param name="SFX">The SFXObject to play.</param>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXObject.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Pitch">The pitch multiplier of the SFXObject to be used.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying this SFXObject.</returns>
    public Coroutine PlaySuspended(SFXObject SFX, Vector3 Position, float DelayDuration, float Volume, float Pitch, Transform Parent = null, bool IsGlobalPosition = true)
    {
        SequencedStartJobs.RemoveAll((Coroutine x) => x == null);
        Coroutine StartJob = StartCoroutine(DelayedPlay(SFX, Position, DelayDuration, Volume, Pitch, Parent, IsGlobalPosition));
        SequencedStartJobs.Add(StartJob);
        return StartJob;
    }
    
    /// <summary>Delays the playing of an SFXObject.</summary>
    /// <param name="SFX">The SFXObject to play.</param>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXObject.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Pitch">The pitch multiplier of the SFXObject to be used.</param>
    /// <returns>The Coroutine for delaying this SFXObject.</returns>
    public Coroutine PlaySuspended(SFXGroup SFX, float DelayDuration, float Volume, float Pitch)
    {
        SequencedStartJobs.RemoveAll((Coroutine x) => x == null);
        Coroutine StartJob = StartCoroutine(DelayedPlay(SFX, DelayDuration, Volume, Pitch));
        SequencedStartJobs.Add(StartJob);
        return StartJob;
    }
    
    /// <summary>Delays the playing of an SFXObject.</summary>
    /// <param name="SFX">The SFXObject to play.</param>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXObject.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Pitch">The pitch multiplier of the SFXObject to be used.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying this SFXObject.</returns>
    public Coroutine PlaySuspended(SFXGroup SFX, Vector3 Position, float DelayDuration, float Volume, float Pitch, Transform Parent = null, bool IsGlobalPosition = true)
    {
        SequencedStartJobs.RemoveAll((Coroutine x) => x == null);
        Coroutine StartJob = StartCoroutine(DelayedPlay(SFX, Position, DelayDuration, Volume, Pitch, Parent, IsGlobalPosition));
        SequencedStartJobs.Add(StartJob);
        return StartJob;
    }

    /// <summary>Delays the playing of an SFXLayer.</summary>
    /// <param name="SFX">The SFXLayer to play.</param>
    /// <param name="DelayDuration">Duration of the delay for the SFXLayer.</param>
    /// <param name="SFXVolume">The volume multiplier applied.</param>
    /// <param name="SFXPitch">The pitch multiplier applied.</param>
    /// <returns>The Coroutine for delaying this SFXLayer.</returns>
    public Coroutine PlaySuspended(SFXLayer SFX, float DelayDuration, float SFXVolume, float SFXPitch)
    {
        SequencedStartJobs.RemoveAll((Coroutine x) => x == null);
        Coroutine StartJob = StartCoroutine(DelayedPlay(SFX, DelayDuration, SFXVolume, SFXPitch));
        SequencedStartJobs.Add(StartJob);
        return StartJob;
    }

    /// <summary>Delays the playing of an SFXLayer in 3D space.</summary>
    /// <param name="SFX">The SFXLayer to play.</param>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">Duration of the delay for the SFXLayer.</param>
    /// <param name="SFXVolume">The volume multiplier applied.</param>
    /// <param name="SFXPitch">The pitch multiplier applied.</param>
    /// <param name="Parent">Parent transform to assign to the SFXLayer (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXLayer at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying this SFXLayer.</returns>
    public Coroutine PlaySuspended(SFXLayer SFX, Vector3 Position, float DelayDuration, float SFXVolume, float SFXPitch, Transform Parent = null, bool IsGlobalPosition = true)
    {
        SequencedStartJobs.RemoveAll((Coroutine x) => x == null);
        Coroutine StartJob = StartCoroutine(DelayedPlay(SFX, Position, DelayDuration, SFXVolume, SFXPitch, Parent, IsGlobalPosition));
        SequencedStartJobs.Add(StartJob);
        return StartJob;
    }

    /// <summary>Plays the SFXObject after the specified delay.</summary>
    /// <param name="SFX">The SFXObject to play.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXObject.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Pitch">The pitch multiplier of the SFXObject to be used.</param>
    private IEnumerator DelayedPlay(SFXObject SFX, float DelayDuration, float Volume, float Pitch)
    {
        yield return new WaitForUnpausedSeconds(DelayDuration);
        Play(SFX, 0, Volume, Pitch);
    }
    
    /// <summary>Plays the SFXObject after the specified delay.</summary>
    /// <param name="SFX">The SFXObject to play.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXObject.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Pitch">The pitch multiplier of the SFXObject to be used.</param>
    private IEnumerator DelayedPlay(SFXGroup SFX, float DelayDuration, float Volume, float Pitch)
    {
        yield return new WaitForUnpausedSeconds(DelayDuration);
        Play(SFX, 0, Volume, Pitch);
    }

    /// <summary>Plays the SFXObject after the specified delay.</summary>
    /// <param name="SFX">The SFXObject to play.</param>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXObject.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Pitch">The pitch multiplier of the SFXObject to be used.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    private IEnumerator DelayedPlay(SFXObject SFX, Vector3 Position, float DelayDuration, float Volume, float Pitch, Transform Parent = null, bool IsGlobalPosition = true)
    {
        yield return new WaitForUnpausedSeconds(DelayDuration);
        Play(SFX, Position, 0, Volume, Pitch, Parent, IsGlobalPosition);
    }
    
    /// <summary>Plays the SFXObject after the specified delay.</summary>
    /// <param name="SFX">The SFXObject to play.</param>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXObject.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Pitch">The pitch multiplier of the SFXObject to be used.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    private IEnumerator DelayedPlay(SFXGroup SFX, Vector3 Position, float DelayDuration, float Volume, float Pitch, Transform Parent = null, bool IsGlobalPosition = true)
    {
        yield return new WaitForUnpausedSeconds(DelayDuration);
        Play(SFX, Position, 0, Volume, Pitch, Parent, IsGlobalPosition);
    }

    /// <summary>Plays the SFXLayer after the specified delay.</summary>
    /// <param name="SFX">The SFXLayer to play.</param>
    /// <param name="Position">The position in 3D space to play the SFXLayer at.</param>
    /// <param name="DelayDuration">Duration of the delay for the SFXObject.</param>
    /// <param name="SFXVolume">The volume multiplier applied.</param>
    /// <param name="SFXPitch">The pitch multiplier applied.</param>
    /// <param name="Parent">Parent transform to assign to the SFXLayer (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXLayer at is global or local to the parent.</param>
    private IEnumerator DelayedPlay(SFXLayer SFX, Vector3 Position, float DelayDuration, float SFXVolume, float SFXPitch, Transform Parent = null, bool IsGlobalPosition = true)
    {
        yield return new WaitForUnpausedSeconds(DelayDuration);
        SFX.Play(Position, 0, SFXVolume, SFXPitch, Parent, IsGlobalPosition);
    }

    /// <summary>Plays the SFXLayer after the specified delay in 3D space.</summary>
    /// <param name="SFX">The SFXLayer to play.</param>
    /// <param name="DelayDuration">Duration of the delay for the SFXObject.</param>
    /// <param name="SFXVolume">The volume multiplier applied.</param>
    /// <param name="SFXPitch">The pitch multiplier applied.</param>
    private IEnumerator DelayedPlay(SFXLayer SFX, float DelayDuration, float SFXVolume, float SFXPitch)
    {
        yield return new WaitForUnpausedSeconds(DelayDuration);
        SFX.Play(0, SFXVolume, SFXPitch);
    }

    /// <summary>Plays the SFXGroup with the default delay settings.</summary>
    /// <param name="SFX">The SFXGroup that will be played.</param>
    /// <returns>The Coroutine for delaying the SFXObject played (null if no delay is applied).</returns>
    [BindableMethod]
    public Coroutine Play(SFXGroup SFX) { return SFX.Play(); }

    /// <summary>Plays the SFXGroup.</summary>
    /// <param name="SFX">The SFXGroup that will be played.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXGroup.</param>
    /// <returns>The Coroutine for delaying the SFXObject played (null if no delay is applied).</returns>
    [BindableMethod]
    public Coroutine Play(SFXGroup SFX, float DelayDuration) { return SFX.Play(DelayDuration); }

    /// <summary>Plays the SFXGroup.</summary>
    /// <param name="SFX">The SFXGroup that will be played.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXGroup.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <returns>The Coroutine for delaying the SFXObject played (null if no delay is applied).</returns>
    [BindableMethod]
    public Coroutine Play(SFXGroup SFX, float DelayDuration, float Volume) { return SFX.Play(DelayDuration, Volume); }

    [BindableMethod]
    /// <summary>Plays the SFXGroup.</summary>
    /// <param name="SFX">The SFXGroup that will be played.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXGroup.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Pitch">The pitch multiplier of the SFXObject to be used.</param>
    /// <returns>The Coroutine for delaying the SFXObject played (null if no delay is applied).</returns>
    public Coroutine Play(SFXGroup SFX, float DelayDuration, float Volume, float Pitch) { return SFX.Play(DelayDuration, Volume, Pitch); }

    [BindableMethod]
    /// <summary>Plays the SFXGroup with the default delay settings in 3D space.</summary>
    /// <param name="SFX">The SFXGroup that will be played.</param>
    /// <param name="Position">The position in 3D space to play the SFXGroup at.</param>
    /// <param name="Parent">Parent transform to assign to the SFXGroup (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying the SFXObject played (null if no delay is applied).</returns>
    public Coroutine Play(SFXGroup SFX, Vector3 Position, Transform Parent = null, bool IsGlobalPosition = true) { return SFX.Play(Position, Parent, IsGlobalPosition); }

    [BindableMethod]
    /// <summary>Plays the SFXGroup in 3D space.</summary>
    /// <param name="SFX">The SFXGroup that will be played.</param>
    /// <param name="Position">The position in 3D space to play the SFXGroup at.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXGroup.</param>
    /// <param name="Parent">Parent transform to assign to the SFXGroup (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying the SFXObject played (null if no delay is applied).</returns>
    public Coroutine Play(SFXGroup SFX, Vector3 Position, float DelayDuration, Transform Parent = null, bool IsGlobalPosition = true) { return SFX.Play(Position, DelayDuration, Parent, IsGlobalPosition); }

    [BindableMethod]
    /// <summary>Plays the SFXGroup in 3D space.</summary>
    /// <param name="SFX">The SFXGroup that will be played.</param>
    /// <param name="Position">The position in 3D space to play the SFXGroup at.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXGroup.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Parent">Parent transform to assign to the SFXGroup (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying the SFXObject played (null if no delay is applied).</returns>
    public Coroutine Play(SFXGroup SFX, Vector3 Position, float DelayDuration, float Volume, Transform Parent = null, bool IsGlobalPosition = true) { return SFX.Play(Position, DelayDuration, Volume, Parent, IsGlobalPosition); }

    [BindableMethod]
    /// <summary>Plays the SFXGroup in 3D space.</summary>
    /// <param name="SFX">The SFXGroup that will be played.</param>
    /// <param name="Position">The position in 3D space to play the SFXGroup at.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXGroup.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Pitch">The pitch multiplier of the SFXObject to be used.</param>
    /// <param name="Parent">Parent transform to assign to the SFXGroup (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying the SFXObject played (null if no delay is applied).</returns>
    public Coroutine Play(SFXGroup SFX, Vector3 Position, float DelayDuration, float Volume, float Pitch, Transform Parent = null, bool IsGlobalPosition = true) { return SFX.Play(Position, DelayDuration, Volume, Pitch, Parent, IsGlobalPosition); }
    #endregion

    #region JobManagement
    /// <summary>Ends all scheduled start jobs.</summary>
    private void EndAllStartJobs()
    {
        for (int i = 0; i < SequencedStartJobs.Count; i++) { if (SequencedStartJobs[i] != null) { StopCoroutine(SequencedStartJobs[i]); } }
        SequencedStartJobs.Clear();
    }

    /// <summary>Ends all scheduled end jobs.</summary>
    private void EndAllEndJobs()
    {
        for (int i = 0; i < SequencedEndJobs.Count; i++) { if (SequencedEndJobs[i] != null) { StopCoroutine(SequencedEndJobs[i]); } }
        SequencedEndJobs.Clear();
    }

    /// <summary>Stops all sound currently being played.</summary>
    public void StopAll()
    {
        //Ends all jobs
        EndAllStartJobs();
        EndAllEndJobs();

        //Stops all current sound
        for (int i = ActiveAudio.Count - 1; i >= 0; i--) { StopAudio(ActiveAudio[i]); }
        OnStopAll();
    }

    /// <summary>Sequences the end of a one shot AudioObject.</summary>
    /// <param name="Audio">The AudioObject attempting to initiate the end.</param>
    /// <param name="RemainingTime">How much time remains before the AudioObject ends.</param>
    public void SequenceEnd(AudioObject Audio, float RemainingTime)
    {
        SequencedEndJobs.RemoveAll((Coroutine x) => x == null);
        SequencedEndJobs.Add(StartCoroutine(DelayedEndAudio(Audio, RemainingTime)));
    }

    /// <summary>Sequences the end of a one shot AudioObject.</summary>
    /// <param name="Audio">The AudioObject attempting to initiate the end.</param>
    public void SequenceEnd(AudioObject Audio)
    {
        SequencedEndJobs.RemoveAll((Coroutine x) => x == null);
        SequencedEndJobs.Add(StartCoroutine(DelayedEndAudio(Audio)));
    }

    /// <summary>Delays the end of a one shot AudioObject.</summary>
    /// <param name="Audio">The AudioObject attempting to initiate the end.</param>
    /// <param name="RemainingTime">How much time remains before the AudioObject ends.</param>
    private IEnumerator DelayedEndAudio(AudioObject Audio, float Duration)
    {
        //Waits and ends the AudioObject
        yield return new WaitForUnpausedSeconds(Duration, Paused);
        StopAudio(Audio);
    }

    /// <summary>Delays the end of a one shot AudioObject.</summary>
    /// <param name="Audio">The AudioObject attempting to initiate the end.</param>
    private IEnumerator DelayedEndAudio(AudioObject Audio)
    {
        //Waits and ends the AudioObject
        WaitUntil Wait = new WaitUntil(() => (!Paused && !Audio.Source.isPlaying) || Audio.Source.time >= Audio.Source.clip.length);
        yield return Wait;
        StopAudio(Audio);
    }
    #endregion

    #region AudioFading
    /// <summary>Sets the global volume of the SoundManager with a fade effect.</summary>
    /// <param name="Volume">The desired volume.</param>
    /// <param name="FadeDuration">The duration of the fade effect.</param>
    protected override IEnumerator VolumeFade(float Volume, float FadeDuration)
    {
        //Calculates rate of change
        float Rate = Mathf.Abs(Volume - this.Volume) / FadeDuration;

        //Continues until fade is complete
        while (this.Volume != Volume)
        {
            //Changes volume gradually
            MasterVolume = Mathf.MoveTowards(this.Volume, Volume, Rate * Time.unscaledDeltaTime);
            yield return new WaitForNextUnpausedFrame();
        }
        yield break;
    }
    #endregion
}

/// <summary>Custom coroutine instructions for the SFXManager.</summary>
namespace PGSauce.AudioManagement.External.Coroutines.SFX
{
    /// <summary>Waits for a set duration of time, independant of the current timescale. Time when the SFXManager is paused will not be counted.</summary>
    public sealed class WaitForUnpausedSeconds : CustomYieldInstruction
    {
        private float TimeRemaining;
        private bool BypassPause;

        /// <summary>Waits for a set duration of time, independant of the current timescale. Time when the SFXManager is paused will not be counted.</summary>
        /// <param name="Duration">The duration of time to wait for in seconds.</param>
        /// <param name="BypassPause">If the wait should bypass the pause effect.</param>
        public WaitForUnpausedSeconds(float Duration, bool BypassPause = false) { TimeRemaining = Duration; this.BypassPause = BypassPause; }

        public override bool keepWaiting
        {
            get
            {
                if (!BypassPause && SFXManager.Main.Paused) { return true; }
                else
                {
                    TimeRemaining -= Time.unscaledDeltaTime;
                    return TimeRemaining > 0;
                }
            }
        }
    }

    /// <summary>Waits for the next frame in which the SFXManager is not paused.</summary>
    public sealed class WaitForNextUnpausedFrame : CustomYieldInstruction
    {
        public override bool keepWaiting { get { return SFXManager.Main.Paused; } }
    }
}
