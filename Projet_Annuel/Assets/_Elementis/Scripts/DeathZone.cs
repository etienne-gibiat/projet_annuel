using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _Elementis.Scripts
{
    public class DeathZone : MonoBehaviour
    {
        private Vector3 _lastPos;

        private void Update()
        {
            _lastPos = transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            var dieable = other.gameObject.GetComponentInParent<IDieable>();
            if (dieable != null)
            {
                dieable.Die((transform.position - _lastPos));
            }
        }
        
        
    }
}