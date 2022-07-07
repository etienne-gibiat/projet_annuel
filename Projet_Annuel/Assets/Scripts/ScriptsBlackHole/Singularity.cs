using UnityEngine;
using _Elementis.Scripts.Character_Controller;
[RequireComponent(typeof(SphereCollider))]
public class Singularity : MonoBehaviour
{
    //This is the main script which pulls the objects nearby
    [SerializeField] public float GRAVITY_PULL = 100f;
    public static float m_GravityRadius = 1f;
    [SerializeField]  private float damage = 0.1f;
    [SerializeField]  private ElementisCharacterController Player;
    void Awake() {
        m_GravityRadius = GetComponent<SphereCollider>().radius;

        if(GetComponent<SphereCollider>()){
            GetComponent<SphereCollider>().isTrigger = true;
        }
    }
    
    void OnTriggerStay (Collider other) {
        if(other.GetComponent<SingularityPullable>()) {
            float gravityIntensity = Vector3.Distance(transform.position, other.transform.position) / m_GravityRadius;
            Player._controller.Move((transform.position - other.transform.position) * gravityIntensity  * GRAVITY_PULL * Time.smoothDeltaTime);
            Player.ApplyDamage(damage);
            //other.attachedRigidbody.AddForce((transform.position - other.transform.position) * gravityIntensity * other.attachedRigidbody.mass * GRAVITY_PULL * Time.smoothDeltaTime);
        }
    }

    public void setDamage(float dmg)
    {
        damage = dmg;
    }
    public void setPlayer(ElementisCharacterController p)
    {
        Player = p;
    }
}
