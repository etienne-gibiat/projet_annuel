using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using MonKey.Extensions;
using Sirenix.OdinInspector;

namespace PGSauce.Core.FSM.WithSo
{
    /// <summary>
    /// Non generic Scriptable Object FSM state
    /// </summary>
    public abstract class SoStateBase : ScriptableObject
    {
        [SerializeField] private bool isNullState;
        [SerializeField] [HideIf("IsNullState")]
        private string stateName;

        /// <summary>
        /// Used for debugging
        /// </summary>
        public int debugIndex;

        /// <summary>
        /// Name of the state, can be overriden in the variable stateName
        /// NULL if null
        /// </summary>
        public string StateName => IsNullState ? "NULL" : (stateName.IsNullOrEmpty() ? name : stateName);
        /// <summary>
        /// Is null state
        /// </summary>
        public bool IsNullState => isNullState;
    }
}
