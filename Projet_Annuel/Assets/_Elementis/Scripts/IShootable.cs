using UnityEngine;

namespace _Elementis.Scripts
{
    public interface IShootable
    {
        void OnShot(IShooter shooter, Vector3 shootForce, Vector3 shootPoint);
    }
}