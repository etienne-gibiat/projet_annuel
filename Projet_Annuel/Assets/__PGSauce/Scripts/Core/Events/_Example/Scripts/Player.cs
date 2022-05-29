using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PGSauce.PGEvents.Example
{
    public class Player : MonoBehaviour
    {
        public PGEventIntPlayer onDamageTaken;

        public void TakeDamage(int damage){
            // Appel de l'event
            onDamageTaken.Raise(damage, this);
        }
    }

}
