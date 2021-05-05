using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinBot : BotControl
{
    
    protected override void Start()
    {
        base.Start();
        MaxHealth = 100;
        EnnemyHealth = MaxHealth;
        agent.speed = 4;
        GainXP = 40;
        actualDamage = 20;
        TimeBetweenAttacks = 5;
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


    protected override void BotControl_OnLevelChanged(object sender, System.EventArgs e)
    {

        if (!isDead)
        {
            MaxHealth += 30;
            EnnemyHealth += 30;
            agent.speed += 1f;
            actualDamage += 10;
        }
    }
}
