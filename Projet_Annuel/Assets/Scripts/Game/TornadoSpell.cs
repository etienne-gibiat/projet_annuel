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
        private SpellManager spell;
        private void Start() {
            spell = GameObject.FindWithTag("SpellManager").GetComponent<SpellManager>();
            pullingCenterPosition = new Vector3(pullingCenter.position.x, pullingCenter.position.y, pullingCenter.position.z);
        }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Ennemy") {
            bot = other.gameObject.GetComponentInParent<BotControl>();
            bot.StopEffectCoroutines("Tornado");
            IEnumerator Tornado = bot.Tornado(transform);
            bot.StartCoroutine(Tornado);
            bot.addCoroutine(Tornado);
        }
            
    }


    IEnumerator IncreasePull(Collider co, bool pull) {
        if (pull) {

            float pullForceCoef = pullForceCurve.Evaluate(((Time.time * 0.1f) % pullForceCurve.length));

            Vector3 forceDirection = pullingCenter.position - co.transform.position;

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
