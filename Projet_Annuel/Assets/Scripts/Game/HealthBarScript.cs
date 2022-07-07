using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using _Elementis.Scripts.Character_Controller;
public class HealthBarScript : MonoBehaviour
    {
    // ************ Health ***************
    public Image HealthBar;
    public float CurrentHealth;
    private float MaxHealth;

    // ************ Mana ***************
    
    public Image ManaBar;
    public float CurrentMana;
    private float MaxMana;

    /*// ************ XP ***************
    private Image XpBar;
    public float CurrentXp;
    private float MaxXp;*/

    // ************ Player ************
    public ElementisCharacterController Player;
    private Text LevelText;
    private float manaPerSecond;
    private void Start()
    {
        MaxHealth = Player.MaxHealth;
        MaxMana = Player.MaxMana;

    }
    private void Update()
    {
        // ************ Health ***************
        MaxHealth = Player.MaxHealth;
        CurrentHealth = Player.Health;
        HealthBar.fillAmount = CurrentHealth / MaxHealth;
        // ************ Mana ***************

        MaxMana = Player.MaxMana;
        CurrentMana = Player.Mana;
        ManaBar.fillAmount = CurrentMana / MaxMana;
        // ************ Xp ***************

        /*CurrentXp = Player.xp;
        XpBar.fillAmount = CurrentXp / Player.MaxXpBeforeLevelUp;
        LevelText.text = Player.level.ToString();*/

    }
}