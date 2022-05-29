using System;
using UnityEngine;

namespace PGSauce.Core.PGEvents
{
	public class PGEvent1Args<T0> : IPGEvent
	{
		private event Action<T0> action;

		public void Raise(T0 value0)
		{
			action?.Invoke(value0);
		}

		public void Register(Action<T0> callback, GameObject gameObject)
		{
			action += callback;
			RegisterListener(gameObject);
		}

		public void Unregister(Action<T0> callback, GameObject gameObject)
		{
			action -= callback;
			UnregisterListener(gameObject);
		}
	}
}
