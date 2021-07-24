using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    Vector3 destination;
    private AudioSource portal;

    private void Start()
    {
        portal = GetComponent<AudioSource>();
    }
    void OnCollisionEnter (Collision col) 
    {
        if (col.transform.tag == "Player")
        {
            if(this.name=="portail1")
            {
                destination = GameObject.Find("portail2").transform.position;
                portal.Play();
            }
            else
            {
                destination = GameObject.Find("portail1").transform.position;
            }

            col.transform.position = destination - Vector3.forward * 3;
            col.transform.Rotate(Vector3.up * 180);
            Camera cam = col.transform.GetComponentInChildren<Camera>();
            if (cam != null) {
                Vector3 rotation = cam.transform.eulerAngles;
                rotation.y += 180;

                cam.transform.eulerAngles = rotation;
            }


        }
    }
}
