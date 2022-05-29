using System;
using UnityEngine;
using PGSauce.Core.PGEvents;

namespace PGSauce.Core.PGEvents
{
	public class PGEvent2Args<T0, T1> : IPGEvent
	{
		private event Action<T0, T1> action;

		public void Raise(T0 value0, T1 value1)
		{
			action?.Invoke(value0, value1);
		}

		public void Register(Action<T0, T1> callback, GameObject gameObject)
		{
			action += callback;
			RegisterListener(gameObject);
		}

		public void Unregister(Action<T0, T1> callback, GameObject gameObject)
		{
			action -= callback;
			UnregisterListener(gameObject);
		}
	}
}
