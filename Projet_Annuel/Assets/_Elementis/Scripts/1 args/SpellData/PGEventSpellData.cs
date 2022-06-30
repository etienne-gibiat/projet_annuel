using UnityEngine;
using System.Collections.Generic;
using PGSauce.Core.PGEvents;
using PGSauce.Core.Strings;

namespace _Elementis.Scripts.Spells
{
	[CreateAssetMenu(fileName = "Game Event SpellData", menuName = MenuPaths.GamePath + "Game Events/1 args/SpellData")]
	public class PGEventSpellData : PGEvent1Args<SpellData>
	{
	}
}
