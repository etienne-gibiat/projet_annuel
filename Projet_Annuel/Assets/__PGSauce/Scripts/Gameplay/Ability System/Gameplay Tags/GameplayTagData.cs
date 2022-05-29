using PGSauce.Core.Strings;
using UnityEngine;

namespace PGSauce.Gameplay.AbilitySystem.Gameplay_Tags
{
    [CreateAssetMenu(menuName = MenuPaths.MenuBase + "Ability System/Gameplay Tag")]
    public class GameplayTagData : ScriptableObject
    {
        [SerializeField]
        private GameplayTagData parent;

        public GameplayTagData Parent => parent;
    }
}