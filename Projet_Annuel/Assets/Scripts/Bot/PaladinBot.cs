using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinBot : BotControl
{
    public AudioSource audioSource;
    
    protected override void Start()
    {
        base.Start();
        MaxHealth = 100;
        EnnemyHealth = MaxHealth;
        agent.speed = 4;
        GainXP = 50;
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
                StartCoroutine(soundAttack(1f, rand));
            }

        }
    } 


    protected override void BotControl_OnLevelChanged(object sender, System.EventArgs e)
    {

        if (!isDead)
        {
            GainXP = 40 + 10 * Player.level;
            MaxHealth += 30;
            EnnemyHealth += 30;
            if(agent.speed < 6f)
            {
                agent.speed += 1f;
                initialSpeed = agent.speed;
            }
            actualDamage += 5;
        }
    }

    public IEnumerator soundAttack(float time, int n) {
        int count = 0;
        if(n == 0) {
            yield return new WaitForSeconds(time);
            audioSource.Play();
        }
        else {
            yield return new WaitForSeconds(time);
            audioSource.Play();
            StartCoroutine(soundAttack(0.5f, 0));
            StartCoroutine(soundAttack(1.3f, 0));
        }
        

    }
}
