using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Galaxy : MonoBehaviour
{
    Transform me;
    float InitialY;
    bool monte;
    float speed = 3f;
    Transform parent;

    private void Start()
    {
        me = GetComponent<Transform>();
        InitialY = me.position.y;
        monte = true;
        parent = this.transform.parent;
    }
    private void Update()
    {
        me.Rotate(new Vector3(0, 1, 0));

        if (monte)
        {
            if(me.position.y < InitialY + 20)
            {
                me.position += Vector3.up * Time.deltaTime * speed;
            }
            else
            {
                monte = false;
            }
        }
        else
        {
            if (me.position.y > InitialY - 20)
            {
                me.position += Vector3.down * Time.deltaTime * speed ;
            }
            else
            {
                monte = true;
            }

        }
    }
}
