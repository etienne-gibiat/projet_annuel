using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Boss : BotControl
{
    private bool enraged;
    private bool FullPower;
    public GameObject EnragedParticle;
    protected override void Start()
    {
        base.Start();
        actualDamage = 30;
        chaseRange = 30f;
        MaxHealth = 500f;
        EnnemyHealth = MaxHealth;
        enraged = false;
        FullPower = false;
        GainXP = 300;
        TimeBetweenAttacks = 5;
    }
    protected void FixedUpdate()
    {
        if((EnnemyHealth / MaxHealth) * 100 <= 66 && enraged == false)
        {
            GetEnraged();
        }

        if ((EnnemyHealth / MaxHealth) * 100 <= 33 && FullPower == false && enraged == true)
        {
            getFullPower();
        }
        if (invincible && transform.localScale.x < 3 && enraged == true)
        {
            transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime;

        }
        if (invincible && transform.localScale.x < 4 && FullPower == true)
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
                anim.SetTrigger("Attack");
            }

        }
        if (enraged)
        {
            if (Vector3.Distance(transform.position, target.position) < attackRange - 2)
            {
                agent.SetDestination(transform.position);
            }
        }
    }
    protected void getFullPower()
    {
        FullPower = true;
        anim.SetBool("FullPower", FullPower);
        agent.isStopped = true;
        invincible = true;
        GameObject Summon = Instantiate(EnragedParticle, transform.position, Quaternion.identity);
        Summon.transform.parent = transform;
        attackRange += 2;
        actualDamage += 10;
        TimeBetweenAttacks -= 1;
    }

    protected void GetEnraged()
    {
        enraged = true;
        anim.SetBool("Enraged", enraged);
        invincible = true;
        //transform.localScale += new Vector3(1,1,1);
        agent.speed += 2;
        agent.isStopped = true;
        actualDamage += 20;
        attackRange += 1;
        TimeBetweenAttacks -= 2;
        GameObject Summon = Instantiate(EnragedParticle, transform.position, Quaternion.identity);
        Destroy(Summon, 4);
        
    }
    public void EnragedEnded()
    {
        agent.isStopped = false;
        invincible = false;
    }

    protected override void BotControl_OnLevelChanged(object sender, System.EventArgs e)
    {

        if (!isDead)
        {
            MaxHealth += 500;
            EnnemyHealth += 500;
            agent.speed += 1f;
            actualDamage += 10;
            GainXP = 250 + (50 * Player.level);
        }
    }

}
