using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectThings : MonoBehaviour
{
    public AudioSource collectSound;
    public string prefabName;
    public static string prefabCollected;

    public static int violetteCount;
    public static int coquelicotCount;
    public static int orchideeCount;

    public static int idCollected = -1;


    private void OnTriggerEnter(Collider other)
    {
        collectSound.Play();
        prefabCollected = prefabName;
        switch (prefabCollected)
        {
            case "Violette":
                ++violetteCount;
                idCollected = 0;
                break;
            case "Coquelicot":
                ++coquelicotCount;
                idCollected = 1;
                break;
            case "Orchidee":
                ++orchideeCount;
                idCollected = 2;
                break;
            default:
                break;
        }
        Destroy(gameObject);
    }

}
