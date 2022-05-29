using PGSauce.Core.PGDebugging;
using PGSauce.Core.Strings;
using UnityEngine;

namespace PGSauce.Core.FSM.WithSo.Example
{
    [CreateAssetMenu(menuName = MenuPaths.FsmExample + "Actions/Print")]
    public class PrintMessageAction : SoAction<Player>
    {
        public string message = "ACTION";
        public override void Act(Player controller)
        {
            PGDebug.Message(message).Log();
        }
    }
}