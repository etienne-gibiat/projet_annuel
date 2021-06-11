using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpell : MonoBehaviour
{
    private BotControl bot;
    private float damage;
    private SpellManager spell;
    private void Start()
    {
        spell = GameObject.FindWithTag("SpellManager").GetComponent<SpellManager>();
        damage = spell.EarthDamage;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ennemy")
        {
            bot = collision.gameObject.GetComponentInParent<BotControl>();
            bot.ApplyDamage(damage);
            bot.StopEffectCoroutines("Rock");
            IEnumerator Rock = bot.Rock();
            bot.StartCoroutine(Rock);
            bot.addCoroutine(Rock);
            Destroy(gameObject,2);
        }
    }
}
