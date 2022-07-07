using System;
using UnityEngine;

namespace _Elementis.Scripts.Character_Controller
{
    public class Shooter : MonoBehaviour, IShooter
    {
        public InputController inputs;
        public float shootForce = 4f; // Maybe change it over time with the bow drawing
        public float shootDistance = 50f;
        public GameObject projectile;

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
        /// <summary>
        /// Function called by the animation of shoot
        /// </summary>
        public void Shoot()
        {
            inputs.isShooting = false;
            var shootRay = Cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            Vector3 direction = shootRay.direction;
            Transform Arrow = Instantiate(projectile.transform, transform.position + direction * 2 + new Vector3(0, 1.2f, 0), Quaternion.LookRotation(direction));
            Arrow.gameObject.GetComponent<Arme>().setForce(shootForce);
            Arrow.gameObject.GetComponent<Arme>().setShooter(this);
            Arrow.gameObject.GetComponent<Arme>().Active = true;
            Destroy(Arrow.gameObject, 10);
            Arrow.GetComponent<Rigidbody>().AddForce(direction * 25,ForceMode.Impulse);
            
        }
    }
}