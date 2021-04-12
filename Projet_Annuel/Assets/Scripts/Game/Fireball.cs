using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private BotControl bot;
    private float damage;
    private SpellManager spell;

    private void Start()
    {
        spell = GameObject.FindWithTag("SpellManager").GetComponent<SpellManager>();
        damage = spell.FireDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ennemy")
        {
            bot = other.gameObject.GetComponentInParent<BotControl>();
            bot.ApplyDamage(damage);
            Destroy(gameObject);
            //collision.
        }
    }
}
