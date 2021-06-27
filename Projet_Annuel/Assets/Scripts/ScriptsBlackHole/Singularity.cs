using UnityEngine;
[RequireComponent(typeof(SphereCollider))]
public class Singularity : MonoBehaviour
{
    //This is the main script which pulls the objects nearby
    [SerializeField] public float GRAVITY_PULL = 100f;
    public static float m_GravityRadius = 1f;
    private float damage = 0.1f;
    private vThirdPersonInput Player;
    void Awake() {
        m_GravityRadius = GetComponent<SphereCollider>().radius;

        if(GetComponent<SphereCollider>()){
            GetComponent<SphereCollider>().isTrigger = true;
        }
        Player = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<vThirdPersonInput>();
    }
    
    void OnTriggerStay (Collider other) {
        if(other.attachedRigidbody && other.GetComponent<SingularityPullable>()) {
            float gravityIntensity = Vector3.Distance(transform.position, other.transform.position) / m_GravityRadius;
            other.attachedRigidbody.AddForce((transform.position - other.transform.position) * gravityIntensity * other.attachedRigidbody.mass * GRAVITY_PULL * Time.smoothDeltaTime);
        }
        if(other.tag == "Player")
        {
            Player.ApplyDamage(0.1f);
        }
    }

    public void setDamage(float dmg)
    {
        damage = dmg;
    }
}
