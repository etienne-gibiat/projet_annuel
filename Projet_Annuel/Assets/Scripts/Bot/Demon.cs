using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : BotControl
{

    public Transform BlackHole;
    public Vector3 v3AverageVelocity;
    public Vector3 v3AverageAcceleration;
    private Vector3 v3PrevVel;
    private Vector3 v3PrevAccel;
    public Vector3 v3PrevPos;
    public Vector3 v3Pos;
    private Vector3 v3Ret;
    public Vector3 v3Velocity;
    private Vector3 BlackHoleScale = new Vector3(1,1,1);
    protected override void Start()
    {
        base.Start();
        MaxHealth = 300;
        EnnemyHealth = MaxHealth;
        agent.speed = 3;
        GainXP = 70;
        actualDamage = 20;
        TimeBetweenAttacks = 8;
        attackRange = 20;
        chaseRange = 40;
    }
    protected override void Update()
    {
        base.Update();
        rand = UnityEngine.Random.Range(0, 1000);
        if(rand == 5)
        {
            anim.SetTrigger("BattleCry");
        }
    }

    private void LateUpdate()
    {
        StartCoroutine(Check());
    }
    protected override void Attack()
    {
        if (Vector3.Distance(transform.position, target.position) < attackRange)
        {
            anim.SetTrigger("Attack");

        }
    }
    public override void AttackBegin()
    {
        Vector3 posPlayer = GetProjectedPosition(1f);
        Transform obj = Instantiate(BlackHole, posPlayer + new Vector3(0,3,0),Quaternion.identity);
        obj.localScale = BlackHoleScale;
        obj.GetComponentInChildren<Singularity>().setDamage(actualDamage / 200);
        Destroy(obj.gameObject, 3);

    }

    

    public void BattleCry()
    {
        StartCoroutine(BattleCryCoroutine());
    }

    private IEnumerator BattleCryCoroutine()
    {
        float time = 1f;
        agent.isStopped = true;
        while(time > 0)
        {
            transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            time -= 0.2f;
            yield return new WaitForSeconds(0.2f);
        }
        agent.isStopped = false;
        agent.speed += 0.1f;
        actualDamage += 5;
        EnnemyHealth += 30;
        MaxHealth += 30;
        attackRange += 4;
        chaseRange += 8;
        BlackHoleScale += new Vector3(0.2f, 0.2f, 0.2f);

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
        }
    }


    /// <summary>
    /// Check en continus la position suivante du joueur
    /// </summary>
    /// <returns></returns>
    protected IEnumerator Check()
    {
        yield return new WaitForEndOfFrame();
        v3Velocity = Player.GetComponent<Rigidbody>().velocity;
        Vector3 v3Accel = v3Velocity - v3PrevVel;
        v3AverageVelocity = v3Velocity;
        v3AverageAcceleration = v3Accel;

        v3Pos = GetProjectedPosition(0.4f);

        v3PrevPos = Player.transform.position;
        v3PrevVel = v3Velocity;
        v3PrevAccel = v3Accel;

    }

    /// <summary>
    /// Donne la projection du joueur fTime secondes plus tard
    /// </summary>
    /// <param name="fTime">Secondes dans le temps</param>
    /// <returns></returns>
    public Vector3 GetProjectedPosition(float fTime)
    {
        v3Ret = Player.transform.position + (v3AverageVelocity * Time.deltaTime * (fTime / Time.deltaTime)) + (0.5f * v3AverageAcceleration * Time.deltaTime * Mathf.Pow(fTime / Time.deltaTime, 2));
        return v3Ret;
    }
}
