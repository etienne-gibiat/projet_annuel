using _Elementis.Scripts.WFC;
using PGSauce.Core.Strings;
using UnityEngine;

namespace _Elementis.Scripts
{
    [CreateAssetMenu(menuName = MenuPaths.GamePath + "WFC Quest")]
    public class WfcQuest : NpcQuestTextProvider
    {
        public override string GetCurrentText()
        {
            if (WfcCastle.Instance.LackATileOrMore)
            {
                return $"Il manque {WfcCastle.Instance.MissingPieces} pièces avant de pouvoir construire un chateau...";
            }
            else
            {
                return
                    "Merci aventurière, nous pouvons construire votre chateau. Approchez de moi à chaque fois que vous voulez le changer.";
            }
        }

        public override void PlayerEnteredInRange()
        {
            WfcCastle.Instance.Generate();
        }
    }
}