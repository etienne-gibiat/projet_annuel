using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public GameObject imgCoquelicot;
    public GameObject imgViolette;
    public GameObject imgOrchidee;

    public GameObject imgHealth;
    public GameObject imgMana;
    public GameObject imgXP;

    public GameObject HUDCollected;

    private void Start()
    {
        HUDCollected.GetComponent<Canvas>().enabled = false;
    }

    void LateUpdate()
    {
        switch (CollectThings.idCollected)
        {
            case 1:
                HUDCollected.GetComponentInChildren<Image>().sprite = imgCoquelicot.GetComponent<Image>().sprite;
                HUDCollected.GetComponent<Canvas>().enabled = true;
                StartCoroutine(TextChange());
                CollectThings.idCollected = -1;
                break;
            case 0:
                HUDCollected.GetComponentInChildren<Image>().sprite = imgViolette.GetComponent<Image>().sprite;
                HUDCollected.GetComponent<Canvas>().enabled = true;
                StartCoroutine(TextChange());
                CollectThings.idCollected = -1;
                break;
            case 2:
                HUDCollected.GetComponentInChildren<Image>().sprite = imgOrchidee.GetComponent<Image>().sprite;
                HUDCollected.GetComponent<Canvas>().enabled = true;
                StartCoroutine(TextChange());
                CollectThings.idCollected = -1;
                break;
            case 4:
                HUDCollected.GetComponentInChildren<Image>().sprite = imgHealth.GetComponent<Image>().sprite;
                HUDCollected.GetComponent<Canvas>().enabled = true;
                StartCoroutine(TextChange());
                CollectThings.idCollected = -1;
                break;
            case 5:
                HUDCollected.GetComponentInChildren<Image>().sprite = imgMana.GetComponent<Image>().sprite;
                HUDCollected.GetComponent<Canvas>().enabled = true;
                StartCoroutine(TextChange());
                CollectThings.idCollected = -1;
                break;
            case 6:
                HUDCollected.GetComponentInChildren<Image>().sprite = imgXP.GetComponent<Image>().sprite;
                HUDCollected.GetComponent<Canvas>().enabled = true;
                StartCoroutine(TextChange());
                CollectThings.idCollected = -1;
                break;
            default:
                break;
        }
    }

    public IEnumerator TextChange()
    {
        yield return new WaitForSeconds(1f);
        HUDCollected.GetComponent<Canvas>().enabled = false;
    }
}
