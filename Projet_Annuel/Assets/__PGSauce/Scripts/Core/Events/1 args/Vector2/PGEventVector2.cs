using UnityEngine;
using PGSauce.Core.Strings;

namespace PGSauce.Core.PGEvents
{
	[CreateAssetMenu(fileName = "Game Event Vector2", menuName = MenuPaths.MenuBase + "Game Events/1 args/Vector2")]
	public class PGEventVector2 : PGEvent1Args<Vector2>
	{
	}
}
