using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PGSauce.Core.Strings;
using UnityEditor;

namespace PGSauce.Core.PGEvents
{
    [CreateAssetMenu(fileName = "Game Event Void", menuName = MenuPaths.MenuBase + "Game Events/Void", order = 1)]
    public class PGEvent : PGEventNoArg
    {
    }
}
