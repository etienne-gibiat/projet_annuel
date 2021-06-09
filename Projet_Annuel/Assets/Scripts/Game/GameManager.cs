using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject MenuLevelUp;
    private Transform pauseTransform;
    private SpellManager spells;
    private vThirdPersonInput Player;
    private float delay;
    private bool isOver = false;
    public CursorLockMode cursorLockMode = CursorLockMode.Locked;
    public bool cursorVisible = false;
    // Start is called before the first frame update
    void Start()
    {
        delay = 5.0f;
        pauseTransform = GameObject.Find("HUD").transform.Find("PausePanel").transform;
        spells = GameObject.Find("SpellManager").GetComponent<SpellManager>();
        Player = GameObject.Find("Player").GetComponent<vThirdPersonInput>();
        Player.OnLevelChanged += GameManager_OnLevelChanged;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseTransform.GetChild(0).gameObject.activeSelf)
            {
                pauseTransform.GetChild(0).gameObject.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                pauseTransform.GetChild(0).gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
        if(delay <= 0)
        {
            Time.timeScale = 0;
        }
        if (isOver)
        {
            delay-=Time.deltaTime;
        }
    }

    public void GameOver()
    {
        isOver = true;
        GameObject.Find("HUD").transform.Find("GameOver").gameObject.SetActive(true);
    }
    public void UpgradeHp()
    {
        Player.MaxHealth += 20;
        Player.Health = Player.MaxHealth;
        disableMenu(MenuLevelUp);
    }
    public void UpgradeMana()
    {
        Player.MaxMana += 20;
        Player.Mana = Player.MaxMana;
        disableMenu(MenuLevelUp);
    }
    public void UpgradeDamage()
    {
        spells.damageSupp += 5;
        disableMenu(MenuLevelUp);
    }

    private void disableMenu(GameObject menu)
    {
        menu.SetActive(false);
        Time.timeScale = 1;
    }

    private void GameManager_OnLevelChanged(object sender, System.EventArgs e)
    {
        MenuLevelUp.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
}
