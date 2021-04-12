using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class BotControl: MonoBehaviour
{

    [HideInInspector] public Transform target; // Cible à poursuivre et attaquer
    private NavMeshAgent agent; //Propriété NavMeshAgent
    private Animator anim; //Animator du bot
    private float timerAttack; // Temps minimum entre 2 attaques
    private float speed; // Speed du bot
    private int rand; // Sert à random une animation d'attaque
    public float EnnemyHealth; // Vie de l'ennemi
    public float MaxHealth; // Vie maximum de l'ennemi
    private bool isDead = false; // Bool qui détermine si l'ennemi est mort
    private bool getAttacked = false; //S'est fait récemment attaqué
    private float timerIdle = 0; //Contrôle sur le temps pour revenir dans un etat normal après avoir été attaqué
    private Vector3 basePositions; // Positions du spawn
    private float chaseRange = 10f; // Distance pour chase
    private float attackRange = 5f; // Distance pour lancer des attaques
    private MeshCollider mySword;
    private float durationAttack; // Durée de l'attaque
    private void Start()
    {
        MaxHealth = 100f;
        target = GameObject.FindGameObjectWithTag("Player").transform;
        EnnemyHealth = MaxHealth;
        anim = GetComponent<Animator>();
        agent = GetComponent(typeof(NavMeshAgent)) as NavMeshAgent;
        basePositions = transform.position;
        mySword = this.transform.GetComponentInChildren<MeshCollider>();
        //this.transform.TryGetComponent<MeshCollider>(out mySword);
        mySword.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
    void Update()
    {

        if (!isDead)
        {
            returnToIdle();
            timerAttack += Time.deltaTime;
            speed = agent.velocity.magnitude / agent.speed;
            Move();
            Attack();
            anim.SetFloat(Animator.StringToHash("Speed"), speed);
        }
    }

    private void Move()
    {
        if (getAttacked || Vector3.Distance(transform.position,target.position) <= chaseRange)
        {
            agent.SetDestination(target.position);
        }
    }
    private void Attack()
    {
        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            if (timerAttack >= 5)
            {
                mySword.enabled = true;
                timerAttack = 0;
                rand = UnityEngine.Random.Range(0, 2);
                if (rand == 0)
                {
                    durationAttack = 2;
                    anim.Play("SwordAttack");
                }
                if (rand == 1)
                {
                    durationAttack = 4;
                    anim.Play("AlternativeAttack");
                }
            }
            
        }
        if (timerAttack > durationAttack)
        {
            mySword.enabled = false;
        }
    }
    private void returnToIdle()
    {
        if(timerIdle > 0) {
            timerIdle -= Time.deltaTime;
        }
        else
        {
            agent.destination = basePositions;
            getAttacked = false;
        }
    }
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = UnityEngine.Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }

    public void ApplyDamage(float damage)
    {
        getAttacked = true;
        timerIdle = 10;
        if (!isDead)
        {
            EnnemyHealth = EnnemyHealth - damage;
            print(gameObject.name + " a subit " + damage + " points de dégâts");
            if(EnnemyHealth <= 0)
            {
                Dead();
            }
            if(EnnemyHealth > MaxHealth)
            {
                EnnemyHealth = MaxHealth;
            }
        }
    }

    private void Dead()
    {
        isDead = true;
        agent.isStopped = true;
        anim.Play("die");
        Destroy(transform.gameObject, 5);

    }
}
