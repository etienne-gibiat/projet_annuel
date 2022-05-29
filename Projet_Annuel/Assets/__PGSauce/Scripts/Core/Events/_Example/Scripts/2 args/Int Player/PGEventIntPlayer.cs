using UnityEngine;
using PGSauce.Core.PGEvents;
using PGSauce.Core.Strings;

namespace PGSauce.PGEvents.Example
{
	[CreateAssetMenu(fileName = "Game Event Int Player", menuName = MenuPaths.GamePath + "Game Events/2 args/IntPlayer")]
	public class PGEventIntPlayer : PGEvent2Args<int,Player>
	{
	}
}
