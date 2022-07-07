using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBot : BotControl
{
    public Transform _targetDestination;
    private bool enraged = false;
    public Transform head;
    public GameObject flameThrower;
    public Arme arme1;
    public Arme arme2;
    public Arme armeHead;

    protected override void Start()
    {
        base.Start();
        agent.enabled = false;

        MaxHealth = 1000;
        EnnemyHealth = MaxHealth;
        agent.speed = 3;
        GainXP = 700;
        actualDamage = 20;
        TimeBetweenAttacks = 5;
        attackRange = 20;
        chaseRange = 300;
        invincible = true;
    }
    private void LateUpdate()
    {
        var lookPos = Player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);

        if ((EnnemyHealth / MaxHealth) * 100 <= 50 && enraged == false)
        {
            GetEnraged();
        }
        if (invincible && transform.localScale.x < 3 && enraged == true)
        {
            transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime;

        }
    }
    protected override void Attack()
    {
        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
           

            if (timerAttack >= TimeBetweenAttacks)
            {
                timerAttack = 0;
                rand = UnityEngine.Random.Range(0, 2);
                anim.SetTrigger("Attack");
                anim.SetInteger("TypeAttack", rand);
            }

        }
    }

    public void FireAttack()
    {
        GameObject fire = Instantiate(flameThrower, head);
        fire.transform.parent = head;
        fire.transform.rotation = Quaternion.Euler(90, 90, 0);
        Destroy(fire, 2);
    }

    protected void GetEnraged()
    {
        enraged = true;
        anim.SetBool("Enraged", enraged);
        invincible = true;
        agent.speed += 2;
        initialSpeed += 2;
        agent.isStopped = true;
        actualDamage += 10;
        attackRange += 10;
        TimeBetweenAttacks -= 2;

    }

    public void StopEnraged()
    {
        agent.isStopped = false;
        invincible = false;
    }

    public void AttackBeginHead()
    {
        if(armeHead != null)
        {
            armeHead.setDamage(actualDamage);
            armeHead.Active = true;
        }
    }

    public void AttackEndHead()
    {
        if (armeHead != null)
        {
            armeHead.Active = false;
        }
    }
    public override void AttackBegin()
    {
        if (arme1 != null && arme2 != null)
        {
            arme1.setDamage(actualDamage);
            arme2.setDamage(actualDamage);
            arme1.Active = true;
            arme2.Active = true;
        }

    }

    /// <summary>
    /// Méthode appelée dans l'animator, associée à une animation elle déclenche la fonction à la fin de l'attaque
    /// </summary>
    public override void AttackEnd()
    {

        if (arme1 != null && arme2 != null)
        {
            arme1.Active = false;
            arme2.Active = false;
        }

    }

    public void StartBattle()
    {
        anim.SetBool("isReady", true);
    }

    public void goUpsideDestination()
    {
        StartCoroutine(moveDestination(_targetDestination.position));
    }

    private IEnumerator moveDestination(Vector3 dest)
    {
        while(Vector3.Distance(transform.position, dest) > 0.5)
        {
            float step = agent.speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, dest, step);
            yield return new WaitForSeconds(0.1f);
        }
        anim.SetBool("IsOnTopOfDestination", true);
        invincible = false;
        transform.Find("HUDEnnemy").gameObject.SetActive(true);
        agent.enabled = true;
    }
    protected override void Move()
    {
        if(Vector3.Distance(transform.position, Player.transform.position) < attackRange - 2)
        {
            agent.speed = 0;
        }
        else
        {
            agent.speed = initialSpeed;
            base.Move();
        }
    }
    protected override void BotControl_OnLevelChanged(object sender, System.EventArgs e)
    {

        if (!isDead)
        {
            GainXP = 60 + 10 * Player.level;
            MaxHealth += 30;
            EnnemyHealth += 30;
            if (agent.speed < 6f)
            {
                agent.speed += 1f;
                initialSpeed = agent.speed;
            }
            actualDamage += 5;
            attackRange += 3;
        }
    }

}
