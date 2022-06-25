using UnityEngine;
using UnityEngine.Events;

namespace _Elementis.Scripts.World_Restoration
{
    public class RestorableUnityEvent : MonoBehaviour, IRestorable
    {
        public UnityEvent onRestored;
        public UnityEvent onUnrestored;
        public void OnRestored()
        {
            onRestored.Invoke();
        }

        public void OnUnRestored()
        {
            onUnrestored.Invoke();
        }

        public Vector3 Position()
        {
            return transform.position;
        }
    }
}