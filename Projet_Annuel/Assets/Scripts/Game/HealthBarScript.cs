using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarScript : MonoBehaviour
    {
    private Image HealthBar;
    public float CurrentHealth;
    private float MaxHealth;
    public Text healthText;

    private Image ManaBar;
    public float CurrentMana;
    private float MaxMana;
    private vThirdPersonInput Player;

    private void Start()
    {
        HealthBar = this.transform.Find("Vie").GetComponent<Image>();
        ManaBar = this.transform.Find("Mana").GetComponent<Image>();
        Player = FindObjectOfType<vThirdPersonInput>();
        MaxHealth = Player.MaxHealth;
        MaxMana = Player.MaxMana;
    }
    private void Update()
    {
        CurrentHealth = Player.Health;
        HealthBar.fillAmount = CurrentHealth / MaxHealth;
        healthText.text = "Health : " + CurrentHealth + "/" + MaxHealth;

        CurrentMana = Player.Mana;
        ManaBar.fillAmount = CurrentMana / MaxMana;
    }
}