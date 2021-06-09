using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    Vector3 destination;
    void OnCollisionEnter (Collision col) 
    {
        if(this.name=="portail1")
        {
            destination = GameObject.Find("portail2").transform.position;
        }
        else
        {
            destination = GameObject.Find("portail1").transform.position;
        }

        col.transform.position = destination - Vector3.forward * 3;
        col.transform.Rotate(Vector3.up * 180);

        Vector3 rotation = col.transform.GetComponentInChildren<Camera>().transform.eulerAngles;
        rotation.y += 180;

        col.transform.GetComponentInChildren<Camera>().transform.eulerAngles = rotation;
    }
}
