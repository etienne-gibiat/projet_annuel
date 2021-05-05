using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                other.GetComponent<vThirdPersonInput>().ApplyDamage(damage);
                alreadyAttacked = true;
            }
        }
    }
    public void setDamage(float degats)
    {
        damage = degats;
    }

}
