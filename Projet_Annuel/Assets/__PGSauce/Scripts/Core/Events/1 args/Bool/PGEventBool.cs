using PGSauce.Core.Strings;
using UnityEngine;

namespace PGSauce.Core.PGEvents
{
	[CreateAssetMenu(fileName = "Game Event Bool", menuName = MenuPaths.MenuBase + "Game Events/1 args/Bool")]
	public class PGEventBool : PGEvent1Args<bool>
	{
	}
}
