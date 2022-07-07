using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnnemyHealthBar : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public GameObject HealthBarUI;
    public Slider slider;
    private BotControl scriptBot;
    private Cinemachine.CinemachineVirtualCamera cam;
    private Transform player;
    private void Start()
    {
        
        scriptBot = GetComponent<BotControl>();
        StartCoroutine(InitializeVariable());
        
        slider = HealthBarUI.transform.Find("Slider").GetComponent<Slider>();
        //cam = scriptBot.target.GetComponentInChildren<Camera>();
    }

    private IEnumerator InitializeVariable()
    {
        yield return new WaitUntil(hasPlayer);
        player = scriptBot.Player.transform.parent;
        cam = player.GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
    }

    private bool hasPlayer()
    {
        return scriptBot.Player != null;
    }
    private void LateUpdate()
    {
        if (cam != null)
        {
            HealthBarUI.transform.LookAt(cam.transform);
        }
        health = scriptBot.EnnemyHealth;
        maxHealth = scriptBot.MaxHealth;
        slider.value = CalculateHealth();
    }

    private float CalculateHealth()
    {
        return health / maxHealth;
    }

}
