using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameTroopers.UI;
using PGSauce.Core.PGDebugging;
using PGSauce.Unity;
using Sirenix.OdinInspector;

namespace PGSauce.Core.Utilities
{
    public class Clearer : PGMonoBehaviour, IOnMenuShowStarted, IOnMenuFocused
    {
        #region Public And Serialized Fields
        [SerializeField] private BoolProvider destroyIf;
        [SerializeField] private bool invert;
        [SerializeField] private GameObject target;
        #endregion
        #region Private Fields
        #endregion
        #region Properties
        #endregion
        #region Unity Functions

        #endregion
        #region Public Methods
        public void OnMenuShowStarted()
        {
            TryToggle();
        }
        public void OnMenuFocused()
        {
            TryToggle();
        }
        #endregion
        #region Private Methods
        private void TryToggle()
        {
            var destroy = destroyIf.GetValue();
            if (invert)
            {
                destroy = !destroy;
            }

            if (target == null)
            {
                target = gameObject;
            }

            target.SetActive(!destroy);
        }
        #endregion
    }
}
