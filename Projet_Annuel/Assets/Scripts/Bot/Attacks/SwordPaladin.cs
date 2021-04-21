using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordPaladin : MonoBehaviour
{
    Collider mySword;
    private void Start()
    {
        mySword = GetComponent<Collider>();
    }
    public float damage = 20f;
    private void OnTriggerEnter(Collider other)
    {
        if (mySword.enabled == true) { 
            if (other.tag == "Player")
            {
                other.GetComponent<vThirdPersonInput>().ApplyDamage(damage);
            }
        }
    }

    public void setDamage(float dmg)
    {
        damage = dmg;
    }
}
