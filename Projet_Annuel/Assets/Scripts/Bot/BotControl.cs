using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using _Elementis.Scripts.Character_Controller;
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
    public ElementisCharacterController Player; // Script associé au joueur
    protected float Distance; // Distance avec le joueur
    protected float DistanceBase; // Distance séparant le bot de sa base
    protected bool invincible = false; // Invincibilité
    protected float TimeBetweenAttacks;
    protected bool isBurning;
    protected int actualDamage; // Degats actuel du bot
    protected float initialSpeed;
    protected Arme arme;
    protected ArrayList listEffectCoroutine = new ArrayList();
    protected virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform; // On récupère le joueur
        Player = target.GetComponentInChildren<ElementisCharacterController>();
        target = Player.transform;

        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        basePositions = transform.position; // On récupère les coordonnées du spawn du bot
        //Player.OnLevelChanged += BotControl_OnLevelChanged;
        initialSpeed = agent.speed;
        arme = GetComponentInChildren<Arme>();
}
    protected virtual void Update()
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
            if (agent.enabled && (getAttacked || (Distance <= chaseRange)))
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
    /// <summary>
    /// Bouge le bot vers le joueur
    /// </summary>
    virtual protected void Move()
    {
        agent.SetDestination(Player.transform.position);
    }
    /// <summary>
    /// Gestion de l'attaque du bot
    /// </summary>
    virtual protected void Attack()
    {

    }
    /// <summary>
    /// Retourne à la base et se soigne une fois à la base
    /// </summary>
    protected void returnToIdle()
    {
        //Retourne à la base, s'arrêteras dans un périmètre de 2m autour de sa base car on ne peut jamais retourner à sa position exact.

        if (DistanceBase > 8 && agent.enabled)
        {
            agent.destination = basePositions;
        }
        else
        {
            if (agent.enabled)
            {
                //Le bot est de retour à sa base, il se régénère 
                agent.destination = transform.position;
            }
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

    /// <summary>
    /// Applique les dégats au bot
    /// </summary>
    /// <param name="damage">dégats à appliquer</param>
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

    /// <summary>
    /// Méthode appelée dans l'animator, associée à une animation elle déclenche la fonction au début de l'attaque
    /// </summary>
    public virtual void AttackBegin()
    {
        if(arme != null)
        {
            arme.setDamage(actualDamage);
            arme.Active = true;
        }
       
    }

    /// <summary>
    /// Méthode appelée dans l'animator, associée à une animation elle déclenche la fonction à la fin de l'attaque
    /// </summary>
    public virtual void AttackEnd()
    {
        
        if(arme != null)
        {
            arme.Active = false;
        }
        
    }

    /// <summary>
    /// Mort du bot
    /// </summary>
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
    }

    /// <summary>
    /// Effet de Brulure
    /// </summary>
    /// <param name="damage">Dégats par seconde</param>
    /// <param name="time">Durée de l'effet de brulure</param>
    /// <returns></returns>
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

    /// <summary>
    /// Effet de Gel
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="time">Durée du gel</param>
    /// <returns></returns>
    virtual public IEnumerator Gel(float damage, float time) {
        isBurning = true;
        float timeGel = time + 0.1f;
        while (isBurning) {
            timeGel -= 0.1f;
            if (timeGel <= 0) {
                isBurning = false;
            }
            else {
                if (agent.speed > initialSpeed / 2) {
                    agent.speed -= 0.1f;
                }

            }
            yield return new WaitForSeconds(0.1f);
        }
        timeGel = time;
        while (timeGel >= 0f) {
            if (agent.speed < initialSpeed) {
                agent.speed += 0.1f;
            }

            timeGel -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }


    }

    /// <summary>
    /// Effet repoussant après un coup de roche
    /// </summary>
    /// <returns></returns>
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


    /// <summary>
    /// Effet repoussant après un coup de roche
    /// </summary>
    /// <returns></returns>
    virtual public IEnumerator Tornado(Transform pos) {

        float accel = agent.acceleration;

        Vector3 destination = pos.position - agent.transform.position;
        agent.velocity = destination.normalized * 8;
        agent.speed = 10;
        agent.angularSpeed = 0;
        agent.acceleration = 20;
        yield return new WaitForSeconds(0.2f);

        agent.speed = initialSpeed;
        agent.acceleration = accel;
        agent.angularSpeed = 120;



    }

    /// <summary>
    /// Arrête les coroutines par nom
    /// </summary>
    /// <param name="name"></param>
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
    /// <summary>
    /// Ajoute une coroutine dans la liste des coroutines
    /// </summary>
    /// <param name="c"></param>
    public void addCoroutine(IEnumerator c)
    {
        
        listEffectCoroutine.Add(c);
    }
}
