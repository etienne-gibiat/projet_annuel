using System;
using UnityEngine.Rendering.PostProcessing;

namespace PGSauce.Gameplay.AbilitySystem.Ability_System
{
    [Serializable]
    public struct GameplayEffectPeriod
    {
        [Min(0)] public float period;
        public bool executeImmediately;
        public bool StrictlyPositivePeriod => period > 0;
    }
}