using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PGSauce.Gameplay.AbilitySystem
{
    public class Projectile : MonoBehaviour
    {
        public Ability_System.AbilitySystem Target;
        [SerializeField]
        private Vector3 Speed;
        [SerializeField]
        private Vector3 Acceleration;

        public IEnumerator TravelToTarget()
        {
            while (Vector3.Distance(Target.transform.position, this.transform.position) > 0.2)
            {
                // Direction of travel
                var direction = (Target.transform.position - this.transform.position).normalized;
                this.transform.position += Vector3.Scale(direction, Speed) * Time.deltaTime;
                Speed += Acceleration * Time.deltaTime;
                yield return null;
            }

            yield break;

        }
    }
}