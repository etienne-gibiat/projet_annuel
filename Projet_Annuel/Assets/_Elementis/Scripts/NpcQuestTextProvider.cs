using UnityEngine;

namespace _Elementis.Scripts
{
    
    public abstract class NpcQuestTextProvider : ScriptableObject
    {
        public abstract string GetCurrentText();

        public abstract void PlayerEnteredInRange();
    }
}