using PGSauce.Core.Strings;
using UnityEngine;

namespace PGSauce.Core.FSM.WithSo.Example
{
    [CreateAssetMenu(menuName = MenuPaths.FsmExample + "Decisions/Is Dead")]
    public class IsDeadDecision : SoDecision<Player>
    {
        public override bool Decide(Player controller)
        {
            return controller.IsDead;
        }
    }
}