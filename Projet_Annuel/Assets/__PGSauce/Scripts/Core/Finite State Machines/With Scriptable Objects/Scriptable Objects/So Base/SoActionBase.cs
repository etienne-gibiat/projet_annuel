using UnityEngine;

namespace PGSauce.Core.FSM.WithSo
{
    /// <summary>
    /// Non generic Scriptable Object FSM action
    /// </summary>
    public abstract class SoActionBase : ScriptableObject
    {
        [SerializeField] private string actionName;

        /// <summary>
        /// The name of the action
        /// </summary>
        public string ActionName { get => actionName; }
    }
}
