using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Boss : BotControl
{
    public AudioSource boss1;
    public AudioSource boss2;
    public AudioSource boss3;
    public AudioSource BattleCry;

    private bool enraged;
    private bool FullPower;
    public GameObject EnragedParticle;
    public GameObject JumpParticle;
    private GameObject FullPowerParticle;
    private float dissolve = -1;
    private Renderer shader;
    
    private Color InitialColor;
    protected override void Start()
    {
        base.Start();
        actualDamage = 30;
        chaseRange = 60f;
        MaxHealth = 1000f;
        EnnemyHealth = MaxHealth;
        enraged = false;
        FullPower = false;
        GainXP = 300;
        TimeBetweenAttacks = 5;
        shader = this.gameObject.GetComponentInChildren<Renderer>();
        InitialColor = shader.material.GetColor("Color_9372EFE");
        questManager.AttachToMob(transform.name);
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
                //if(FullPower)
                //    StartCoroutine(soundAttack(0.5f, 4));
                if (enraged)
                    StartCoroutine(soundAttack(1.25f, 1));
                else
                    StartCoroutine(soundAttack(1f, 0));
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
        FullPowerParticle = Instantiate(EnragedParticle, transform.position, Quaternion.identity);
        StartCoroutine(soundAttack(0.01f, 3));
        FullPowerParticle.transform.parent = transform;
        attackRange += 2;
        actualDamage += 10;
        TimeBetweenAttacks -= 1;
        
    }

    protected void GetEnraged()
    {
        enraged = true;
        anim.SetBool("Enraged", enraged);
        invincible = true;
        agent.speed += 2;
        initialSpeed += 2;
        agent.isStopped = true;
        actualDamage += 20;
        attackRange += 1;
        TimeBetweenAttacks -= 2;
        GameObject Summon = Instantiate(EnragedParticle, transform.position, Quaternion.identity);
        Destroy(Summon, 4);
        StartCoroutine(soundAttack(0.01f, 3));

    }
    public void EnragedEnded()
    {
        agent.isStopped = false;
        invincible = false;
    }
    public void ParticleJump()
    {
        GameObject groundAttack = Instantiate(JumpParticle, transform.position, transform.rotation);
        groundAttack.GetComponentInChildren<ParticleCollision>().setDamage(actualDamage / 3);
        Destroy(groundAttack, 5);

    }
    protected override void BotControl_OnLevelChanged(object sender, System.EventArgs e)
    {

        if (!isDead)
        {
            MaxHealth += 500;
            EnnemyHealth += 500;
            agent.speed += 1f;
            initialSpeed = agent.speed;
            actualDamage += 10;
            GainXP = 250 + (50 * Player.level);
        }
    }

    protected override void Dead()
    {
        base.Dead();
        Destroy(FullPowerParticle);
        StartCoroutine(Dissolve());
        questManager.DetachToMob(transform.name);
    }

    IEnumerator Dissolve()
    {

        bool isDying = true;
        dissolve = shader.material.GetFloat("Vector1_C7B8123F");
        float timeDying = 4;
        while (isDying)
        {
            timeDying -= 0.02f;
            if (timeDying <= 0)
            {
                isDying = false;
            }
            else
            {
                dissolve += 0.01f;
                shader.material.SetFloat("Vector1_C7B8123F", dissolve);
            }
            yield return new WaitForSeconds(0.02f);
        }
    }
    public override IEnumerator Brulure(float damage, float time)
    {
        isBurning = true;
        float timeBurn = time + 0.1f;
        StartCoroutine(ChangeColorBrulure());
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

    public override IEnumerator Gel(float damage, float time)
    {
        isBurning = true;
        float timeGel = time + 0.1f;
        Color MeshColor = shader.material.GetColor("Color_9372EFE");
        while (isBurning)
        {
            timeGel -= 0.1f;
            if (timeGel <= 0)
            {
                isBurning = false;
            }
            else
            {
                if(agent.speed > initialSpeed/2)
                {
                    agent.speed -= 0.1f;
                }
                
            }
            if (MeshColor.r > 1f)
            {
                MeshColor = shader.material.GetColor("Color_9372EFE");
                MeshColor.r -= 0.1f;
            }
            shader.material.SetColor("Color_9372EFE", MeshColor);
            yield return new WaitForSeconds(0.1f);
        }
        timeGel = time;
        while (timeGel >= 0f)
        {
            if(agent.speed < initialSpeed)
            {
                agent.speed += 0.1f;
            }
            
            timeGel -= 0.1f;
            if (MeshColor.r < InitialColor.r)
            {
                MeshColor = shader.material.GetColor("Color_9372EFE");
                MeshColor.r += 0.1f;
            }
            else
            {
                MeshColor = shader.material.GetColor("Color_9372EFE");
                MeshColor.r = InitialColor.r;
            }
            shader.material.SetColor("Color_9372EFE", MeshColor);
            yield return new WaitForSeconds(0.1f);
        }


    }

    public IEnumerator ChangeColorBrulure()
    {
        float timeChange = 5f;
        Color MeshColor = shader.material.GetColor("Color_9372EFE");
        while (isBurning)
        {
            timeChange -= 0.1f;
            if (MeshColor.g >= 1.0f)
            {
                MeshColor = shader.material.GetColor("Color_9372EFE");
                MeshColor.g -= 0.1f;
                MeshColor.b -= 0.1f;
            }
            shader.material.SetColor("Color_9372EFE", MeshColor);
            yield return new WaitForSeconds(0.1f);
        }
        timeChange = 5f;
        while (timeChange >= 0f)
        {
            timeChange -= 0.1f;
            if (MeshColor.g < InitialColor.g)
            {
                MeshColor = shader.material.GetColor("Color_9372EFE");
                MeshColor.g += 0.1f;
                MeshColor.b += 0.1f;
            }
            else
            {
                MeshColor = shader.material.GetColor("Color_9372EFE");
                MeshColor.g = InitialColor.g;
                MeshColor.b = InitialColor.b;
            }
            shader.material.SetColor("Color_9372EFE", MeshColor);
            yield return new WaitForSeconds(0.1f);
        }


    }

    public IEnumerator soundAttack(float time, int n) {
        if (n == 0) {
            yield return new WaitForSeconds(time);
            boss1.Play();
        }
        else if(n == 1){
            boss1.Play();
            yield return new WaitForSeconds(time);
            boss1.Play();
            StartCoroutine(soundAttack(1.7f, 2));

        }
        else if (n == 2) {
            yield return new WaitForSeconds(time);
            boss2.Play();

        }
        else if (n == 3) { 
            yield return new WaitForSeconds(time);
            BattleCry.Play();

        }


    }

}
