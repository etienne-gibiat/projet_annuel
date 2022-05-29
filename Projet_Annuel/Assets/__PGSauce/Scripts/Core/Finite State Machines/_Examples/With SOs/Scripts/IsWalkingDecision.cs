using PGSauce.Core.Strings;
using UnityEngine;

namespace PGSauce.Core.FSM.WithSo.Example
{
    [CreateAssetMenu(menuName = MenuPaths.FsmExample + "Decisions/Is Walking")]
    public class IsWalkingDecision : SoDecision<Player>
    {
        public override bool Decide(Player controller)
        {
            return controller.IsWalking;
        }
    }
}