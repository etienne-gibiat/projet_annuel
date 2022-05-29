using PGSauce.Core.Strings;
using UnityEngine;

namespace PGSauce.Core.PGEvents
{
	[CreateAssetMenu(fileName = "Game Event Gameobject", menuName = MenuPaths.MenuBase + "Game Events/1 args/Gameobject")]
	public class PGEventGameobject : PGEvent1Args<GameObject>
	{
	}
}
