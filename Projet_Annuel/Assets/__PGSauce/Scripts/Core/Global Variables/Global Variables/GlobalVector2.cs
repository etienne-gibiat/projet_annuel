using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGSauce.Core;
using PGSauce.Core.GlobalVariables;
using PGSauce.Core.Strings;

namespace PGSauce.Core.GlobalVariables
{
    [CreateAssetMenu(fileName = "new Global Vector2", menuName = MenuPaths.MenuBase + "Global Variables/Global Vector2")]
    public class GlobalVector2 : IGlobalValue<Vector2>
    {
    }
}