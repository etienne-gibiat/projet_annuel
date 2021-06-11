using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarScript : MonoBehaviour
    {
    // ************ Health ***************
    private Image HealthBar;
    public float CurrentHealth;
    private float MaxHealth;

    // ************ Mana ***************
    
    private Image ManaBar;
    public float CurrentMana;
    private float MaxMana;

    // ************ XP ***************
    private Image XpBar;
    public float CurrentXp;
    private float MaxXp;

    // ************ Player ************
    private vThirdPersonInput Player;
    public Text healthText;
    private Text LevelText;

    private void Start()
    {
        
        HealthBar = this.transform.Find("Vie").GetComponent<Image>();
        ManaBar = this.transform.Find("Mana").GetComponent<Image>();
        XpBar = this.transform.Find("XpFront").GetComponent<Image>();
        LevelText = this.transform.Find("Level").GetComponentInChildren<Text>();
        Player = FindObjectOfType<vThirdPersonInput>();
        MaxHealth = Player.MaxHealth;
        MaxMana = Player.MaxMana;
        MaxXp = Player.MaxXpBeforeLevelUp;

    }
    private void Update()
    {
        // ************ Health ***************
        CurrentHealth = Player.Health;
        HealthBar.fillAmount = CurrentHealth / MaxHealth;
        healthText.text = CurrentHealth + "/" + MaxHealth;
        // ************ Mana ***************
        CurrentMana = Player.Mana;
        ManaBar.fillAmount = CurrentMana / MaxMana;

        // ************ Xp ***************

        CurrentXp = Player.xp;
        XpBar.fillAmount = CurrentXp / MaxXp;
        LevelText.text = Player.level.ToString();

    }
}