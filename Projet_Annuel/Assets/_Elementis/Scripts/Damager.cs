using PGSauce.Unity;
using UnityEngine;

namespace _Elementis.Scripts
{
    public class Damager : PGMonoBehaviour
    {
        public float damage = 5f;
        private void OnTriggerEnter(Collider other)
        {
            var dieable = other.gameObject.GetComponentInParent<IDamageable>();
            if (dieable != null)
            {
                dieable.TakeDamage(damage);
            }
        }

    }
}