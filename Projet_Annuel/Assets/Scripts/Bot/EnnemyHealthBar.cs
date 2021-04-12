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
    private Camera cam;
    private void Start()
    {
        
        scriptBot = GetComponent<BotControl>();
        cam = scriptBot.target.GetComponentInChildren<Camera>();
    }
    private void Update()
    {
        HealthBarUI.transform.LookAt(cam.transform);
        health = scriptBot.EnnemyHealth;
        maxHealth = scriptBot.MaxHealth;
        slider.value = CalculateHealth();
        if (health < maxHealth)
        {
            HealthBarUI.SetActive(true);
        }
    }

    private float CalculateHealth()
    {
        return health / maxHealth;
    }

}
