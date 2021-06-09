using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject EnnemyToSpawn;
    public float frequence;
    private float timeSpend = 0;
     void Update()
    {
        if(timeSpend >= frequence)
        {
            Instantiate(EnnemyToSpawn, gameObject.transform.position + Random.insideUnitSphere,transform.rotation);
            timeSpend = 0;
        }

        timeSpend += Time.deltaTime;
    }
}
