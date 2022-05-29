using UnityEngine;

namespace PGSauce.Core.FSM.WithSo
{
    /// <summary>
    /// Non generic Scriptable Object FSM decision
    /// </summary>
    public abstract class SoDecisionBase : ScriptableObject
    {
        /// <summary>
        /// Name of the decision
        /// </summary>
        public string DecisionName => name;
    }
}
