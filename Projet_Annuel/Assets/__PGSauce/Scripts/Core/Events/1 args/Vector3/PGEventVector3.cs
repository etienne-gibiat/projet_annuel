using PGSauce.Core.Strings;
using UnityEngine;

namespace PGSauce.Core.PGEvents
{
	[CreateAssetMenu(fileName = "Game Event Vector3", menuName = MenuPaths.MenuBase + "Game Events/1 args/Vector3")]
	public class PGEventVector3 : PGEvent1Args<Vector3>
	{
	}
}
