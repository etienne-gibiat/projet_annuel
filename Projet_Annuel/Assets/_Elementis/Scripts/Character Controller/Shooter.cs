using System;
using UnityEngine;

namespace _Elementis.Scripts.Character_Controller
{
    public class Shooter : MonoBehaviour, IShooter
    {
        public InputController inputs;
        public float shootForce = 4f; // Maybe change it over time with the bow drawing
        public float shootDistance = 50f;

        private Camera _cam;

        private Camera Cam
        {
            get
            {
                if (!_cam){
                    _cam = Camera.main;
                }

                return _cam;
            }
        }

        private void Update()
        {
            if (inputs.isAiming)
            {
                if (inputs.isShooting)
                {
                    Shoot();
                    inputs.isShooting = false;
                }
            }
        }

        private void Shoot()
        {
            var shootRay = Cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            if (Physics.Raycast(shootRay, out var info, shootDistance))
            {
                Debug.DrawRay(shootRay.origin, shootRay.direction * 1000, Color.red, 5);
                var shootable = info.collider.gameObject.GetComponentInParent<IShootable>();
                if (shootable != null)
                {
                    shootable.OnShot(this, (info.point - transform.position).normalized * shootForce, info.point);
                }
            }
            
        }
    }
}