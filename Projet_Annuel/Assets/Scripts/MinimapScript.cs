using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{

    public Transform player;
    public Camera mainCamera;
    public GameObject cursor;

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, mainCamera.transform.eulerAngles.y, 0f);

        cursor.transform.rotation = Quaternion.Euler(0f, 0f, -player.eulerAngles.y + mainCamera.transform.eulerAngles.y); 
    }
}
