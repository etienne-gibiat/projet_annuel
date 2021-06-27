using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arme : MonoBehaviour
{

    public bool Active = false;
    public int degats = 10;

    /// <summary>
    /// Trigger sur les armes qui n'ont pas besoins de collision
    /// </summary>
    /// <param name="other"></param>
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



    /// <summary>
    /// Test de collision avec une arme possédant des collisions
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (Active)
        {
            if (collision.transform.tag == "Player")
            {
                collision.transform.GetComponent<vThirdPersonInput>().ApplyDamage(degats);
                
            }
            if(collision.transform.tag != "Ennemy")
            {
                Active = false;
            }
            
        }
    }

    /// <summary>
    /// Définis les dégats de l'arme
    /// </summary>
    /// <param name="damage"></param>
    public void setDamage(int damage)
    {
        degats = damage;
    }
}
