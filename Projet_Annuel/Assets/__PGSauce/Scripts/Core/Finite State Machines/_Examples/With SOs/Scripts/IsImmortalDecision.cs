using UnityEngine;
using PGSauce.Core.Strings;

namespace PGSauce.Core.FSM.WithSo.Example
{
    [CreateAssetMenu(menuName = MenuPaths.FsmExample + "Decisions/Is Immortal")]
    public class IsImmortalDecision : SoDecision<Player>
    {
        public override bool Decide(Player controller)
        {
            return controller.IsImmortal;
        }
    }
}
