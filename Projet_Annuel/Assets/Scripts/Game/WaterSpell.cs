using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpell : MonoBehaviour
{
    private BotControl bot;
    private float damage;
    private SpellManager spell;
    private void Start()
    {
        spell = GameObject.FindWithTag("SpellManager").GetComponent<SpellManager>();
        damage = spell.WaterDamage;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ennemy")
        {

            bot = other.gameObject.GetComponentInParent<BotControl>();
            bot.ApplyDamage(damage);
            bot.StopEffectCoroutines("Gel");
            IEnumerator gel = bot.Gel(damage, 5);
            bot.StartCoroutine(gel);
            bot.addCoroutine(gel);
            StartCoroutine(destroy());
        }
    }

    virtual public IEnumerator destroy() {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }


}
