using UnityEngine;

namespace _Elementis.Scripts.World_Restoration
{
    public interface IRestorable
    {
        void OnRestored();
        void OnUnRestored();
        Vector3 Position();
    }
}