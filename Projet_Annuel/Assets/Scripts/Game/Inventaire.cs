using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventaire : MonoBehaviour
{
    public bool activation = false;
    public GameObject player;
    GameObject P;
    public int[] slot;


    void Start()
    {
        GetComponent<Canvas>().enabled = false;
        P = transform.GetChild(0).gameObject;
        slot = new int[P.transform.childCount];
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            ActivateInventaire();
        }

    }


    public void ActivateInventaire()
    {

        activation = !activation;
        GetComponent<Canvas>().enabled = activation;

        if (!activation)
        {
            player.GetComponent<vThirdPersonInput>().FireInput = KeyCode.Mouse0;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            player.GetComponent<vThirdPersonInput>().rotateCameraXInput = "Mouse X";
            player.GetComponent<vThirdPersonInput>().rotateCameraYInput = "Mouse Y";
        }
        else
        {
            player.GetComponent<vThirdPersonInput>().FireInput = KeyCode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            player.GetComponent<vThirdPersonInput>().rotateCameraXInput = "Analog X";
            player.GetComponent<vThirdPersonInput>().rotateCameraYInput = "Analog Y";

        }
    }
}
