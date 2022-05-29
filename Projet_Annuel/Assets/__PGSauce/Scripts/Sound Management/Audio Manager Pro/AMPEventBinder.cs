using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using PGSauce.AudioManagement.External.EventBinding;

/// <summary>Provides full internal AMP functionality for binding events.</summary>
namespace PGSauce.AudioManagement.External.EventBinding
{
	[Serializable]
	public class UnityObjectEvent : UnityEvent<object> { }

	/// <summary>Marks a method as a bindable to an AMP event.</summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class BindableMethod : Attribute { }

	/// <summary>A trigger and method binded together into an event to be used by the AMPEventBinder.</summary>
	[Serializable]
	public class BindedEvent
	{
		/// <summary>The name of this BindedEvent.</summary>
		public string EventName;
		/// <summary>The trigger that causes this event to execute.</summary>
		public EventTriggerType EventTrigger;
		/// <summary>The method to execute on event trigger.</summary>
		public UnityObjectEvent EventCallback;
		/// <summary>The type of audio manager this event is binded to.</summary>
		public Type ManagerType;

		/// <summary>Constructs and binds a new event.</summary>
		/// <param name="Name">The name of this BindedEvent.</param>
		/// <param name="Trigger">The trigger that causes this event to execute.</param>
		/// <param name="Callback">The method to execute on event trigger.</param>
		/// <param name="ManagerType">The type of audio manager this event is binded to.</param>
		public BindedEvent(string Name, EventTriggerType Trigger, UnityObjectEvent Callback, Type ManagerType)
		{
			EventName = Name;
			EventTrigger = Trigger;
			EventCallback = Callback;
			this.ManagerType = ManagerType;
		}

		/// <summary>Executes the event.</summary>
		public void Execute()
		{
			if (EventCallback != null)
			{
				//Retreives the necessary object
				object ManagerInstance;
				if (ManagerType == typeof(MusicManager)) { ManagerInstance = MusicManager.Main; }
				else if (ManagerType == typeof(PlaylistManager)) { ManagerInstance = PlaylistManager.Main; }
				else if (ManagerType == typeof(SFXManager)) { ManagerInstance = SFXManager.Main; }
				else { return; }

				//Error handling
				if (ManagerInstance == null)
				{
					Debug.LogError("Event '" + EventName + "' could not be executed as no instance of " + ManagerType.Name + " was present in the scene.");
					return;
				}

				//Invokes method
				EventCallback.Invoke(ManagerInstance);
			}
		}
	}

	/// <summary>Different trigger types for binding events.</summary>
	public enum EventTriggerType
	{
		/// <summary>None.</summary>
		None = 0,
		/// <summary>Occurs when the Monobehaviour starts.</summary>
		Start = 1,
		/// <summary>Occurs when the GameObject is activated.</summary>
		OnEnable = 2,
		/// <summary>Occurs when the GameObject is deactivated.</summary>
		OnDisable = 3,
		/// <summary>Occurs when the RigidBody experiences a collision.</summary>
		OnCollisionEnter = 4,
		/// <summary>Occurs when the RigidBody2D experiences a collision.</summary>
		OnCollisionEnter2D = 5,
		/// <summary>Occurs when the RigidBody experiences a trigger event.</summary>
		OnTriggerEnter = 6,
		/// <summary>Occurs when the RigidBody2D experiences a trigger event.</summary>
		OnTriggerEnter2D = 7,
		OnCollisionExit = 8,
		OnCollisionExit2D = 9,
		OnTriggerExit = 10,
		OnTriggerExit2D = 11,
		OnMouseDown = 12,
		OnMouseEnter = 13,
		OnMouseExit = 14,
		OnMouseUp = 15
	}
}

/// <summary>Handles executing BindedEvents as desired.</summary>
public class AMPEventBinder : MonoBehaviour
{
	/// <summary>Collection of all BindedEvents on this Monobehaviour.</summary>
	public List<BindedEvent> AllEvents = new List<BindedEvent>();

	/// <summary>Executes all BindedEvents with the specified trigger type.</summary>
	/// <param name="DesiredEventTrigger">The desired event trigger type.</param>
	private void ExecuteEvents(EventTriggerType DesiredEventTrigger)
    {
        for (int i = 0; i < AllEvents.Count; i++)
        {
            if (AllEvents[i].EventTrigger == DesiredEventTrigger) { AllEvents[i].Execute(); }
        }
    }

	//Executes events as needed
	private void Start() { ExecuteEvents(EventTriggerType.Start); }
	private void OnEnable() { ExecuteEvents(EventTriggerType.OnEnable); }
	private void OnDisable() { ExecuteEvents(EventTriggerType.OnDisable); }
	private void OnCollisionEnter(Collision Col) { ExecuteEvents(EventTriggerType.OnCollisionEnter); }
	private void OnCollisionEnter2D(Collision2D Col) { ExecuteEvents(EventTriggerType.OnCollisionEnter2D); }
	private void OnTriggerEnter(Collider Col) { ExecuteEvents(EventTriggerType.OnTriggerEnter); }
	private void OnTriggerEnter2D (Collider2D Col) { ExecuteEvents(EventTriggerType.OnTriggerEnter2D); }
	private void OnCollisionExit(Collision Col) { ExecuteEvents(EventTriggerType.OnCollisionExit); }
	private void OnCollisionExit2D(Collision2D Col) { ExecuteEvents(EventTriggerType.OnCollisionExit2D); }
	private void OnTriggerExit(Collider Col) { ExecuteEvents(EventTriggerType.OnTriggerExit); }
	private void OnTriggerExit2D(Collider2D Col) { ExecuteEvents(EventTriggerType.OnTriggerExit2D); }
	//ADD MOUSE EVENT CALLBACK
}
