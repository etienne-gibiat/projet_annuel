using _Elementis.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Elementis.Scripts.Character_Controller;

public class Arme : MonoBehaviour
{

    public bool Active = false;
    public int degats = 10;
    protected IShooter shooter;
    protected Vector3 initialPosition;
    protected float shootForce;
    protected Rigidbody rb;
    protected Collider col;


    private void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }

    /// <summary>
    /// Trigger sur les armes qui n'ont pas besoins de collision
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (Active)
        {
            if (other.transform.tag == "Player")
            {
                other.transform.GetComponent<ElementisCharacterController>().ApplyDamage(degats);
                Active = false;

            }
        }
    }

    /// <summary>
    /// Test de collision avec une arme possédant des collisions
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (Active)
        {
            Debug.Log(collision.transform.name);
            var shootable = collision.gameObject.GetComponentInParent<IShootable>();
            if (shootable != null)
            {
                shootable.OnShot(shooter, (transform.position - initialPosition).normalized * shootForce, transform.position);
                Active = false;
                
            }
            if(collision.transform.tag != "Ennemy")
            {
                Active = false;
            }
            if (collision.transform.tag == "Player")
            {
                collision.transform.GetComponent<ElementisCharacterController>().ApplyDamage(degats);
                Active = false;

            }

        }
    }

    public void setShooter(IShooter shooter)
    {
        this.shooter = shooter;
    }

    public void setForce(float force)
    {
        shootForce = force;
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
