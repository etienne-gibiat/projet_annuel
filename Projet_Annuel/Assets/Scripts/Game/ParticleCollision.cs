using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Elementis.Scripts.Character_Controller;

public class ParticleCollision : MonoBehaviour
{

    private bool alreadyAttacked = false;
    private float damage = 20;
    private void OnParticleCollision(GameObject other)
    {
        if (!alreadyAttacked)
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<ElementisCharacterController>().ApplyDamage(damage);
                alreadyAttacked = true;
            }
        }
    }
    public void setDamage(float degats)
    {
        damage = degats;
    }

}
