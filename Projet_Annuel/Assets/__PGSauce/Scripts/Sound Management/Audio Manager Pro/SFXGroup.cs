using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGSauce.AudioManagement.External.Utilities;
using PGSauce.AudioManagement;
using PGSauce.Core.Strings;

namespace PGSauce.AudioManagement.External
{
    /// <summary>A group of various different complete SFXObjects. Playing this SFXGroup will result in one of its constituent SFXObjects being played at random.</summary>
[CreateAssetMenu(fileName = "SFXGroup", menuName = "AMP/SFX Group (Random)", order = 4)]
public abstract class SFXGroup : PgSfxBase
{
    /// <summary>The degree of random behaviour to use when selecting a random SFXObject from the SFXGroup.</summary>
    public enum RandomMode
    {
        /// <summary>Purely random, allowing the same SFXObject to be selected multiple times consecutively.</summary>
        AllowRepeats = 0,
        /// <summary>Purely random, with the exception of disallowing the same SFXObject to be selected multiple times consecutively.</summary>
        DisallowRepeats = 1,
        /// <summary>Pre-randomizes a list of the SFXObjects, iterating through the list as they are selected, before re-randomizing the list on exhaustion.</summary>
        FullyUnique = 2
    }

    /// <summary>The name of this SFXGroup.</summary>
    [Tooltip("(Optional) The name of this SFXGroup.")]
    public string GroupName;

    /// <summary>The various different SFXObjects that belong to this SFXGroup.</summary>
    [Tooltip("The various different SFXObjects that belong to this SFXGroup.")]
    public SFXObject[] SFXObjects = new SFXObject[0];

    /// <summary>Randomization method to use for this SFXGroup.</summary>
    [Tooltip("Randomization method to use for this SFXGroup.")]
    public RandomMode Randomization;

    /// <summary>The index of the last SFXObject that was played.</summary>
    private int LastID;

    /// <summary>A list of all the SFXObjects that will be played.</summary>
    private List<SFXObject> SFXObjectsToPlay;

	public override string ToString ()
	{
		string NameString = GroupName;
		if (System.String.IsNullOrEmpty(GroupName)) { NameString = name; }
		return NameString;
	}

    private void OnEnable() { Reset(); }
    private void OnDisable() { Reset(); }
    /// <summary>Resets the SFXGroup to its untampered state.</summary>
    private void Reset()
    {
        LastID = 0;
        SFXObjectsToPlay = new List<SFXObject>(SFXObjects);
        SFXObjectsToPlay.Randomize();
    }

    /// <summary>Plays a randomly selected SFXObject.</summary>
    /// <returns>The Coroutine for delaying the SFXObject played (null if no delay is applied).</returns>
    public Coroutine Play()
    {
        SelectNext();
        return SFXManager.Main.Play(SFXObjectsToPlay[LastID]);
    }

    /// <summary>Plays a randomly selected SFXObject.</summary>
    /// <param name="DelayDuration">Duration of the global delay for the SFXGroup.</param>
    /// <returns>The Coroutine for delaying the SFXObject played (null if no delay is applied).</returns>
    public Coroutine Play(float DelayDuration)
    {
        SelectNext();
        return SFXManager.Main.Play(SFXObjectsToPlay[LastID], DelayDuration);
    }

    /// <summary>Plays a randomly selected SFXObject.</summary>
    /// <param name="DelayDuration">Duration of the global delay for the SFXGroup.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <returns>The Coroutine for delaying the SFXObject played (null if no delay is applied).</returns>
    public Coroutine Play(float DelayDuration, float Volume)
    {
        SelectNext();
        return SFXManager.Main.Play(SFXObjectsToPlay[LastID], DelayDuration, Volume);
    }

    /// <summary>Plays a randomly selected SFXObject.</summary>
    /// <param name="DelayDuration">Duration of the global delay for the SFXGroup.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Pitch">The pitch multiplier of the SFXObject to be used.</param>
    /// <returns>The Coroutine for delaying the SFXObject played (null if no delay is applied).</returns>
    public Coroutine Play(float DelayDuration, float Volume, float Pitch)
    {
        SelectNext();
        return SFXManager.Main.Play(SFXObjectsToPlay[LastID], DelayDuration, Volume, Pitch);
    }

    /// <summary>Plays a randomly selected SFXObject in 3D space.</summary>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying the SFXObject played (null if no delay is applied).</returns>
    public Coroutine Play(Vector3 Position, Transform Parent = null, bool IsGlobalPosition = true)
    {
        SelectNext();
        return SFXManager.Main.Play(SFXObjectsToPlay[LastID], Position, Parent, IsGlobalPosition);
    }

    /// <summary>Plays a randomly selected SFXObject in 3D space.</summary>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXGroup.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying the SFXObject played (null if no delay is applied).</returns>
    public Coroutine Play(Vector3 Position, float DelayDuration, Transform Parent = null, bool IsGlobalPosition = true)
    {
        SelectNext();
        return SFXManager.Main.Play(SFXObjectsToPlay[LastID], Position, DelayDuration, Parent, IsGlobalPosition);
    }

    /// <summary>Plays a randomly selected SFXObject in 3D space.</summary>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXGroup.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying the SFXObject played (null if no delay is applied).</returns>
    public Coroutine Play(Vector3 Position, float DelayDuration, float Volume, Transform Parent = null, bool IsGlobalPosition = true)
    {
        SelectNext();
        return SFXManager.Main.Play(SFXObjectsToPlay[LastID], Position, DelayDuration, Volume, Parent, IsGlobalPosition);
    }

    /// <summary>Plays a randomly selected SFXObject in 3D space.</summary>
    /// <param name="Position">The position in 3D space to play the SFXObject at.</param>
    /// <param name="DelayDuration">Duration of the global delay for the SFXGroup.</param>
    /// <param name="Volume">The volume multiplier of the SFXObject to be used.</param>
    /// <param name="Pitch">The pitch multiplier of the SFXObject to be used.</param>
    /// <param name="Parent">Parent transform to assign to the SFXObject (Optional).</param>
    /// <param name="IsGlobalPosition">If the position to play the SFXObject at is global or local to the parent.</param>
    /// <returns>The Coroutine for delaying the SFXObject played (null if no delay is applied).</returns>
    public Coroutine Play(Vector3 Position, float DelayDuration, float Volume, float Pitch, Transform Parent = null, bool IsGlobalPosition = true)
    {
        SelectNext();
        return SFXManager.Main.Play(SFXObjectsToPlay[LastID], Position, DelayDuration, Volume, Pitch, Parent, IsGlobalPosition);
    }

    /// <summary>Determines which SFXObject will be played next.</summary>
    private void SelectNext()
    {
        int NewID = 0;
        switch (Randomization)
        {
            //Selects random
            case RandomMode.AllowRepeats: NewID = SFXObjectsToPlay.RandomIndex(); break;
            //Selects random until a new SFXObject is selected
            case RandomMode.DisallowRepeats: do { NewID = SFXObjectsToPlay.RandomIndex(); } while (NewID == LastID); break;
            case RandomMode.FullyUnique:
                {
                    //Moves to the next SFXObject
                    NewID = LastID + 1;
                    if (NewID >= SFXObjectsToPlay.Count)
                    {
                        //Loops back and randomizes list
                        SFXObject LastSFX = SFXObjectsToPlay[LastID];
                        NewID -= SFXObjectsToPlay.Count;
                        do { SFXObjectsToPlay.Randomize(); } while (SFXObjectsToPlay[0] != LastSFX);
                    }
                    break;
                }
        }

        //Updates ID
        LastID = NewID;
    }
}

}

