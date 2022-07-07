using _Elementis.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Elementis.Scripts.Character_Controller;
using PGSauce.Core.PGDebugging;

public class Arrow : Arme
{




    /// <summary>
    /// Test de collision avec une arme possédant des collisions
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnCollisionEnter(Collision collision)
    {
        if (Active)
        {
            var shootable = collision.gameObject.GetComponentInParent<IShootable>();
            if (shootable != null)
            {
                shootable.OnShot(shooter, (transform.position - initialPosition).normalized * shootForce, transform.position);
                Active = false;
            }
            if (collision.transform.tag == "Ennemy")
            {
                collision.transform.GetComponentInParent<BotControl>().ApplyDamage(degats);
                PGDebug.Message($"DAMAGE: {degats} ").Log();
                Active = false;


            }
            rb.isKinematic = true;
            transform.Translate(Vector3.forward * 0.3f);
            col.enabled = false;
            transform.parent = collision.transform;

        }
    }
}
