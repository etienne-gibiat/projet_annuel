using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TornadoSpell : MonoBehaviour
{
    public Transform pullingCenter;
    private Vector3 pullingCenterPosition;
    public AnimationCurve pullingCenterCurve;
    public float pullForce;
    public AnimationCurve pullForceCurve;
    public float refreshrate;
        private BotControl bot;
        private float damage;
        private SpellManager spell;
        private void Start() {
            spell = GameObject.FindWithTag("SpellManager").GetComponent<SpellManager>();
            damage = spell.TornadoDamage;
        pullingCenterPosition = new Vector3(pullingCenter.position.x, pullingCenter.position.y, pullingCenter.position.z);
    }
        //private void OnCollisionEnter(Collision collision) {
        //    if (collision.transform.tag == "Ennemy") {
        //        bot = collision.gameObject.GetComponentInParent<BotControl>();
        //        bot.ApplyDamage(damage);
        //        bot.StopEffectCoroutines("Tornado");
        //        IEnumerator Rock = bot.Rock();
        //        bot.StartCoroutine(Rock);
        //        bot.addCoroutine(Rock);
        //        Destroy(gameObject, 2);
        //    }
        //}

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Ennemy") {
            bot = other.gameObject.GetComponentInParent<BotControl>();
            bot.ApplyDamage(damage);
            bot.StopEffectCoroutines("Tornado");
            IEnumerator Tornado = bot.Tornado(transform);
            bot.StartCoroutine(Tornado);
            bot.addCoroutine(Tornado);
            //other.gameObject.GetComponentInParent<NavMeshAgent>().enabled = false;
            //StartCoroutine(IncreasePull(other, true));
            //StartCoroutine(Stop(other));
        }
            
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Ennemy") {
            //other.gameObject.GetComponentInParent<NavMeshAgent>().enabled = true;
            //StartCoroutine(IncreasePull(other, false));
        }
    }

    IEnumerator IncreasePull(Collider co, bool pull) {
        if (pull) {

            float pullForceCoef = pullForceCurve.Evaluate(((Time.time * 0.1f) % pullForceCurve.length));

            Vector3 forceDirection = pullingCenter.position - co.transform.position;

            //co.attachedRigidbody.AddForce(forceDirection.normalized * pullForce * Time.deltaTime);
            
            //co.GetComponent<Rigidbody>().AddForce(forceDirection.normalized * pullForce * co.attachedRigidbody.mass * pullForceCoef * Time.smoothDeltaTime);
            //co.attachedRigidbody.AddForce(forceDirection.normalized * pullForce * co.attachedRigidbody.mass * pullForceCoef * Time.smoothDeltaTime);
            //float centerCoef = pullingCenterCurve.Evaluate(((Time.time * 0.1f) % pullingCenterCurve.length));
            //pullingCenter.position = new Vector3(pullingCenter.position.x, pullingCenterPosition.y * centerCoef, pullingCenter.position.z);

            yield return refreshrate;
            StartCoroutine(IncreasePull(co, pull));
        }
        else {
            StopCoroutine(IncreasePull(co, pull));
            Stop(co);
        }
    }

    virtual public IEnumerator Stop(Collider other) {
        yield return new WaitForSeconds(3);
        other.gameObject.GetComponentInParent<NavMeshAgent>().enabled = true;
        other.attachedRigidbody.velocity.Set(0, 0, 0);

    }
}
