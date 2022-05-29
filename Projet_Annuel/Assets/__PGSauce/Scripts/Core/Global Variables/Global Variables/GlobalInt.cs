using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PGSauce.Core;
using PGSauce.Core.GlobalVariables;
using PGSauce.Core.Strings;

namespace PGSauce.Core.GlobalVariables
{
    [CreateAssetMenu(fileName = "new Global Int", menuName = MenuPaths.MenuBase + "Global Variables/Global Int")]
    public class GlobalInt : IGlobalValue<int>
    {
    }
}