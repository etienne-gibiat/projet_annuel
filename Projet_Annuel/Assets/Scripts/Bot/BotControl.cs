using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class BotControl : MonoBehaviour
{

    [HideInInspector] public Transform target; // Cible à poursuivre et attaquer
    protected NavMeshAgent agent; //Propriété NavMeshAgent
    protected Animator anim; //Animator du bot
    protected float timerAttack; // Temps minimum entre 2 attaques
    protected float speed; // Speed du bot
    protected int rand; // Sert à random une animation d'attaque
    public float EnnemyHealth; // Vie de l'ennemi
    public float MaxHealth; // Vie maximum de l'ennemi
    protected float GainXP; //Xp donné par le bot au player
    protected bool isDead = false; // Bool qui détermine si l'ennemi est mort
    protected bool getAttacked = false; //S'est fait récemment attaqué
    protected float timerIdle = 0; //Contrôle sur le temps pour revenir dans un etat normal après avoir été attaqué
    protected Vector3 basePositions; // Positions du spawn
    protected Vector3 baseRotations; // Rotations du spawn;
    protected float chaseRange = 10f; // Distance pour chase
    protected float attackRange = 5f; // Distance pour lancer des attaques
    protected float durationAttack; // Durée de l'attaque
    public vThirdPersonInput Player; // Script associé au joueur
    protected float Distance; // Distance avec le joueur
    protected float DistanceBase; // Distance séparant le bot de sa base
    protected bool invincible = false; // Invincibilité
    protected float TimeBetweenAttacks;
    protected bool isBurning;
    protected int actualDamage; // Degats actuel du bot
    protected float initialSpeed;
    protected ArrayList listEffectCoroutine = new ArrayList();
    protected QuestManager questManager;
    protected virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; // On récupère le joueur
        Player = target.GetComponent<vThirdPersonInput>();

        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        basePositions = transform.position; // On récupère les coordonnées du spawn du bot
        Player.OnLevelChanged += BotControl_OnLevelChanged;
        initialSpeed = agent.speed;

        questManager = GameObject.Find("Quest").GetComponent<QuestManager>();
        if (transform.parent != null)
        {
            questManager.AttachToMob(transform.parent.name);
        }
}
    void Update()
    {

        if (!isDead)
        {
            // On calcule la distance entre le joueur et l'ennemi
            Distance = Vector3.Distance(target.position, transform.position);
            // On calcule la distance entre l'ennemi et sa position de base
            DistanceBase = Vector3.Distance(basePositions, transform.position);
            timerIdle -= Time.deltaTime;
            if (timerIdle <= 0 & Distance > chaseRange)
            {
                returnToIdle();
            }
            // Quand le joueur est assez rapproché ou que l'ennemi s'est fait taper récemment
            if (getAttacked || (Distance <= chaseRange))
            {
                Move();
            }
            //Gestion de l'attaque
            Attack();

            //Temps entre 2 attaques
            timerAttack += Time.deltaTime;
            //Données de la speed du NavMesh Agent transferer vers l'animator
            speed = agent.velocity.magnitude / agent.speed;
            anim.SetFloat(Animator.StringToHash("Speed"), speed);

        }
    }
    protected void Move()
    {
        agent.SetDestination(target.position);
    }
    virtual protected void Attack()
    {

    }
    protected void returnToIdle()
    {
        //Retourne à la base, s'arrêteras dans un périmètre de 2m autour de sa base car on ne peut jamais retourner à sa position exact.

        if (DistanceBase > 8)
        {
            agent.destination = basePositions;
        }
        else
        {
            //Le bot est de retour à sa base, il se régénère 
            agent.destination = transform.position;
            EnnemyHealth = Mathf.Min(EnnemyHealth + (Time.deltaTime*8) * MaxHealth / 100, MaxHealth);
            
        }
        getAttacked = false;

    }

    //Event lorsque le player LevelUp, implémenter dans les sous classes de botcontrol 
    virtual protected void BotControl_OnLevelChanged(object sender, System.EventArgs e)
    {

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
            if (!invincible)
            {
                EnnemyHealth = EnnemyHealth - damage;
                print(gameObject.name + " a subit " + damage + " points de dégâts");
            }
            if (EnnemyHealth <= 0)
            {
                Dead();
            }
            if (EnnemyHealth > MaxHealth)
            {
                EnnemyHealth = MaxHealth;
            }
        }
    }


    public void AttackBegin()
    {
        GetComponentInChildren<Arme>().setDamage(actualDamage);
        GetComponentInChildren<Arme>().Active = true;
    }

    //Méthode appelée dans l'animator, associée à une animation elle déclenche la fonction à la fin de l'attaque
    public void AttackEnd()
    {
        GetComponentInChildren<Arme>().Active = false;
    }

    protected virtual void Dead()
    {
        
        Player.getXp(GainXP);
        Player.OnLevelChanged -= BotControl_OnLevelChanged;
        AttackEnd();
        isDead = true;
        EnnemyHealth = 0;
        agent.isStopped = true;
        anim.Play("Death");
        Destroy(transform.gameObject, 5);
        if (transform.parent != null)
        {
            questManager.DetachToMob(transform.parent.name);
        }
    }
    virtual public IEnumerator Brulure(float damage,float time)
    {

        isBurning = true;
        float timeBurn = time + 0.1f;
        while (isBurning)
        {
            timeBurn -= 1f;
            if (timeBurn <= 0)
            {
                isBurning = false;
            }
            else
            {
                ApplyDamage(damage);
            }
            yield return new WaitForSeconds(1f);
        }


    }

    virtual public IEnumerator Gel(float damage, float time)
    {

        isBurning = true;
        float timeGel = time + 0.1f;
        while (isBurning)
        {
            timeGel -= 0.1f;
            if (timeGel <= 0)
            {
                isBurning = false;
            }
            else
            {
                agent.speed -= 0.1f;
            }
            yield return new WaitForSeconds(0.1f);
        }


    }

    virtual public IEnumerator Rock()
    {
         
        float accel = agent.acceleration;

        Vector3 destination = agent.transform.position - agent.destination;
        agent.velocity = destination.normalized * 8;
        agent.speed = 10;
        agent.angularSpeed = 0;
        agent.acceleration = 20;
        yield return new WaitForSeconds(0.2f);

        agent.speed = initialSpeed;
        agent.acceleration = accel;
        agent.angularSpeed = 120;



    }
    public void StopEffectCoroutines(string name)
    {
        ArrayList tmp = new ArrayList();
        foreach (IEnumerator coroutine in listEffectCoroutine)
        {
            if (coroutine.ToString().Contains(name))
            {
                StopCoroutine(coroutine);
                tmp.Add(coroutine);
                
            }
            
        }
        foreach(IEnumerator delete in tmp)
        {

            listEffectCoroutine.Remove(delete);
        }

    }
    public void addCoroutine(IEnumerator c)
    {
        
        listEffectCoroutine.Add(c);
    }
}
