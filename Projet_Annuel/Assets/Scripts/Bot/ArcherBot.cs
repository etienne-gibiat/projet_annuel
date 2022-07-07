using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBot : BotControl
{

    public Transform Fleche;
    public Vector3 v3AverageVelocity;
    public Vector3 v3AverageAcceleration;
    private Vector3 v3PrevVel;
    private Vector3 v3PrevAccel;
    public Vector3 v3PrevPos;
    public Vector3 v3Pos;
    private Vector3 v3Ret;
    public Vector3 v3Velocity;
    protected override void Start()
    {
        base.Start();
        MaxHealth = 200;
        EnnemyHealth = MaxHealth;
        agent.speed = 4;
        GainXP = 70;
        actualDamage = 20;
        TimeBetweenAttacks = 3;
        attackRange = 30;
        chaseRange = 60;
        arme = Fleche.GetComponent<Arme>();
        arme.Active = true;
        arme.setDamage(actualDamage);
    }
    private void LateUpdate()
    {
        var lookPos = Player.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
        StartCoroutine(Check());
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
    public override void AttackBegin()
    {
        Vector3 direction = AlgoProjection();
        Transform Arrow = Instantiate(Fleche, transform.position + direction * 2 + new Vector3(0, 1.2f, 0), Quaternion.LookRotation(-direction));
        //Arrow.GetComponent<Rigidbody>().AddForce(direction * 200);
        Destroy(Arrow.gameObject, 10);
        Arrow.GetComponent<Rigidbody>().AddForce((direction + new Vector3(0, 0.05f, 0)) * 200);

    }

    /// <summary>
    /// Permet de prédire les mouvements du joueur
    /// </summary>
    /// <returns>Retourne la direction vers laquelle il faut viser</returns>
    protected Vector3 AlgoProjection()
    {
        float fSpeed = 40;
        int iMaxIterations = 100;
        float fBaseCheckTime = 0.15f;
        float fTimePerCheck = 0.05f;
        float fDistance = 9999;
        Vector3 direction = (v3Pos - this.transform.position).normalized;




        int iIterations = 0;

        float fCheckTime = fBaseCheckTime;
        Vector3 v3TargetPosition = GetProjectedPosition(fBaseCheckTime);
        Debug.DrawLine(this.transform.position, v3TargetPosition, Color.red, 3);

        //Predict projectile position
        Vector3 v3PredictedProjectilePosition = this.transform.position + ((v3TargetPosition - this.transform.position).normalized * fSpeed * fCheckTime);
        Debug.DrawLine(this.transform.position, v3PredictedProjectilePosition, Color.green, 3);
        fDistance = (v3TargetPosition - v3PredictedProjectilePosition).magnitude;

        while (fDistance > 1.5f && iIterations < iMaxIterations)
        {
            iIterations++;
            fCheckTime += fTimePerCheck;
            v3TargetPosition = GetProjectedPosition(fCheckTime);
            v3PredictedProjectilePosition = this.transform.position + ((v3TargetPosition - this.transform.position).normalized * fSpeed * fCheckTime);
            Debug.DrawLine(this.transform.position, v3PredictedProjectilePosition, Color.green, 3);
            fDistance = (v3TargetPosition - v3PredictedProjectilePosition).magnitude;
        }

        Vector3 v3Velocity = (v3TargetPosition - this.transform.position).normalized;
        return v3Velocity;

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
    /// <summary>
    /// Check en continus la position suivante du joueur
    /// </summary>
    /// <returns></returns>
    protected IEnumerator Check()
    {
        yield return new WaitForEndOfFrame();
        v3Velocity = Player.GetComponent<CharacterController>().velocity;
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
        v3Ret = Player.transform.position+ (v3AverageVelocity * Time.deltaTime * (fTime / Time.deltaTime)) + (0.5f * v3AverageAcceleration * Time.deltaTime * Mathf.Pow(fTime / Time.deltaTime, 2));
        return v3Ret;
    }
}
