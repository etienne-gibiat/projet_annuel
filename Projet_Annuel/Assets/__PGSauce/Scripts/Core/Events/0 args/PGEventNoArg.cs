using UnityEngine;
using System;

namespace PGSauce.Core.PGEvents
{
    public abstract class PGEventNoArg : IPGEvent
    {
		private event Action action;

		public void Raise()
		{
			action?.Invoke();
		}

		public void Register(Action callback, GameObject gameObject)
		{
			action += callback;
			RegisterListener(gameObject);
		}

		public void Unregister(Action callback, GameObject gameObject)
		{
			action -= callback;
			UnregisterListener(gameObject);
		}
	}
}
