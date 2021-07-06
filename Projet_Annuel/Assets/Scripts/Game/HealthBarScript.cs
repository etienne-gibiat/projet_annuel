using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private TextMeshProUGUI healthText;
    private TextMeshProUGUI manaText;
    private TextMeshProUGUI manaTextPerSecond;
    private TextMeshProUGUI healthTextPerSecond;
    private Text LevelText;
    private float manaPerSecond;
    private void Start()
    {
        
        HealthBar = this.transform.Find("Vie").GetComponent<Image>();
        ManaBar = this.transform.Find("Mana").GetComponent<Image>();
        XpBar = this.transform.Find("XpFront").GetComponent<Image>();
        LevelText = this.transform.Find("Level").GetComponentInChildren<Text>();
        healthText = this.transform.Find("HealthText").GetComponent<TextMeshProUGUI>();
        manaText = this.transform.Find("ManaText").GetComponent<TextMeshProUGUI>();
        manaTextPerSecond = this.transform.Find("ManaTextPerSecond").GetComponent<TextMeshProUGUI>();
        healthTextPerSecond = this.transform.Find("HealthTextPerSecond").GetComponent<TextMeshProUGUI>();
        Player = FindObjectOfType<vThirdPersonInput>();
        MaxHealth = Player.MaxHealth;
        MaxMana = Player.MaxMana;
        MaxXp = Player.MaxXpBeforeLevelUp;

    }
    private void Update()
    {
        // ************ Health ***************
        MaxHealth = Player.MaxHealth;
        CurrentHealth = Player.Health;
        HealthBar.fillAmount = CurrentHealth / MaxHealth;
        healthText.text = CurrentHealth.ToString("n0") + "/" + MaxHealth;
        healthTextPerSecond.text = "+" + ((Player.speedHealth) * MaxHealth / 100).ToString("n1");
        // ************ Mana ***************

        MaxMana = Player.MaxMana;
        CurrentMana = Player.Mana;
        ManaBar.fillAmount = CurrentMana / MaxMana;
        manaText.text = CurrentMana.ToString("n0") + "/" + MaxMana;
        manaTextPerSecond.text = "+" + (( Player.speedMana) * MaxMana / 100).ToString("n1");
        // ************ Xp ***************

        CurrentXp = Player.xp;
        XpBar.fillAmount = CurrentXp / MaxXp;
        LevelText.text = Player.level.ToString();

    }
}