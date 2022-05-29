using System;
using UnityEngine;
using UnityEngine.Events;
using PGSauce.Core.PGEvents;

namespace PGSauce.PGEvents.Example
{
	public class PGEventListenerIntPlayer : PGEventListener2Args<int,Player, PGEventIntPlayer, UnityEventIntPlayer>
	{
	}
}
