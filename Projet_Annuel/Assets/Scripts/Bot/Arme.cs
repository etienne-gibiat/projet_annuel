using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arme : MonoBehaviour
{

    public bool Active = false;
    public int degats = 10;
    private void OnTriggerEnter(Collider other)
    {
        if (Active)
        {
            if(other.tag == "Player")
            {
                other.GetComponent<vThirdPersonInput>().ApplyDamage(degats);
            }
        }
    }

    public void setDamage(int damage)
    {
        degats = damage;
    }
}
