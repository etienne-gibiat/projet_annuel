using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public Transform MenuLevelUp;
    private Transform pauseTransform;
    private SpellManager spells;
    private vThirdPersonInput Player;
    private float delay;
    private bool isOver = false;
    // Start is called before the first frame update
    void Start()
    {
        delay = 5.0f;
        pauseTransform = GameObject.Find("HUD").transform.Find("PausePanel").transform;
        MenuLevelUp = GameObject.Find("HUD").transform.Find("LevelUpMenu").GetChild(0).transform;
        spells = GameObject.Find("SpellManager").GetComponent<SpellManager>();
        Player = GameObject.FindWithTag("Player").GetComponent<vThirdPersonInput>();
        Player.OnLevelChanged += GameManager_OnLevelChanged;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenuChangeState();
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


    public void PauseMenuChangeState()
    {
        if (pauseTransform.GetChild(0).gameObject.activeSelf || pauseTransform.GetChild(1).gameObject.activeSelf)
        {
            pauseTransform.GetChild(0).gameObject.SetActive(false);
            pauseTransform.GetChild(1).gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
        }
        else
        {
            pauseTransform.GetChild(0).gameObject.SetActive(true);
            pauseTransform.GetChild(1).gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
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
        Player.speedHealth += 0.2f;
        disableMenu(MenuLevelUp);
    }
    public void UpgradeMana()
    {
        Player.MaxMana += 20;
        Player.Mana = Player.MaxMana;
        Player.speedMana += 1;
        disableMenu(MenuLevelUp);
    }
    public void UpgradeDamage()
    {
        spells.damageSupp += 5;
        disableMenu(MenuLevelUp);
    }

    public void ControlsMenuChangeState()
    {
        if (pauseTransform.GetChild(1).gameObject.activeSelf)
        {
            pauseTransform.GetChild(1).gameObject.SetActive(false);
        }
        else
        {
            pauseTransform.GetChild(0).gameObject.SetActive(false);
            pauseTransform.GetChild(1).gameObject.SetActive(true);
        }
    }
    private void disableMenu(Transform menu)
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        menu.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    private void GameManager_OnLevelChanged(object sender, System.EventArgs e)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MenuLevelUp.gameObject.SetActive(true);
        
        Time.timeScale = 0f;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1.0f;
        Application.Quit();
    }
}
