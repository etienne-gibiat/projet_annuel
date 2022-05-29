using PGSauce.Core.Strings;
using UnityEngine;

namespace PGSauce.Core.PGEvents
{
	[CreateAssetMenu(fileName = "Game Event String", menuName = MenuPaths.MenuBase + "Game Events/1 args/String")]
	public class PGEventString : PGEvent1Args<string>
	{
	}
}
