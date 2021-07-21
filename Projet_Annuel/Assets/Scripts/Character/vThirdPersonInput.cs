using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections;

public class vThirdPersonInput : MonoBehaviour
{
    #region Variables       
    public event EventHandler OnLevelChanged;
    [Header("Controller Input")]
    public string horizontalInput = "Horizontal";
    public string verticallInput = "Vertical";
    public KeyCode jumpInput = KeyCode.Space;
    public KeyCode strafeInput = KeyCode.Tab;
    public KeyCode sprintInput = KeyCode.LeftShift;
    public KeyCode FireInput = KeyCode.Mouse0;
    [Header("Camera Input")]
    public string rotateCameraXInput = "Mouse X";
    public string rotateCameraYInput = "Mouse Y";
    [Header("Spells")]
    public SpellManager Spells;
    public KeyCode Feu = KeyCode.Alpha1;
    public KeyCode Eau = KeyCode.Alpha2;
    public KeyCode Terre = KeyCode.Alpha3;
    public KeyCode Air = KeyCode.Alpha4;
    private Transform HUD;
    private Image AirSpellHUD;
    private Image EarthSpellHUD;
    private Image WaterSpellHUD;
    private Image FireSpellHUD;
    [Header("Caractéristiques")]
    public float Health;
    public float MaxHealth;
    public float Mana;
    public float MaxMana;
    public float speedMana = 15f;
    public float speedHealth = 0.2f;
    public int level;
    public float xp;
    public float MaxXpBeforeLevelUp;
    [HideInInspector] public bool isDead;
    private Transform MenuLevelUp;
    public Elements ElementPicked;
    private Inventaire inventaire;
    [HideInInspector] public Transform respawn;
    [HideInInspector] public vThirdPersonController cc;
    [HideInInspector] public vThirdPersonCamera tpCamera;
    [HideInInspector] public Camera cameraMain;
    
    
    public enum Elements
    {
        Feu,
        Eau,
        Terre,
        Air
    }
    #endregion

    protected virtual void Start()
    {
        Spells = GameObject.FindWithTag("SpellManager").GetComponent<SpellManager>();
        InitializeStats();
        InitilizeController();
        InitializeTpCamera();
        StartCoroutine(SpellHUD());
    }

    protected virtual void FixedUpdate()
    {
        if (!isDead)
        {
            cc.UpdateMotor();               // updates the ThirdPersonMotor methods
            cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
            cc.ControlRotationType();       // handle the controller rotation type
            Mana = Mathf.Min(Mana + (Time.deltaTime * speedMana) * MaxMana / 100, MaxMana);
            Health = Mathf.Min(Health + (Time.deltaTime * speedHealth) * MaxHealth / 100, MaxHealth);
        }
    }

    protected virtual void Update()
    {
        if (!isDead)
        {
            InputHandle();                  // update the input methods
            cc.UpdateAnimator();            // updates the Animator Parameters
        }
    }
    
    public virtual void OnAnimatorMove()
    {
        cc.ControlAnimatorRootMotion(); // handle root motion animations 
    }

    #region Basic Locomotion Inputs

    /// <summary>
    /// Initialise les stats et l'HUD du joueur
    /// </summary>
    protected virtual void InitializeStats()
    {
        MaxHealth = 100f;
        MaxMana = 100f;
        Health = MaxHealth;
        Mana = MaxMana;
        level = 1;
        xp = 0;
        MaxXpBeforeLevelUp = 200;
        isDead = false;
        MenuLevelUp = GameObject.Find("LevelUpMenu").transform.Find("PauseMenu");
        HUD = GameObject.Find("HUD").transform;
        inventaire = HUD.Find("Inventaire").GetComponent<Inventaire>();
        AirSpellHUD = HUD.Find("AirSpellHUD").GetComponent<Image>();
        EarthSpellHUD = HUD.Find("EarthSpellHUD").GetComponent<Image>();
        WaterSpellHUD = HUD.Find("WaterSpellHUD").GetComponent<Image>();
        FireSpellHUD = HUD.Find("FireSpellHUD").GetComponent<Image>();
        respawn = GameObject.Find("Respawn").transform;

    }
    protected virtual void InitilizeController()
    {
        cc = GetComponent<vThirdPersonController>();

        if (cc != null)
            cc.Init();
    }

    protected virtual void InitializeTpCamera()
    {
        if (tpCamera == null)
        {
            tpCamera = FindObjectOfType<vThirdPersonCamera>();
            if (tpCamera == null)
                return;
            if (tpCamera)
            {
                tpCamera.SetMainTarget(this.transform);
                tpCamera.Init();
            }
        }
    }

    protected virtual void InputHandle()
    {
        MoveInput();
        CameraInput();
        SprintInput();
        StrafeInput();
        JumpInput();
        SpellInput();
        AttackInput();
        
    }

    
    protected virtual void SpellInput()
    {
        if (Input.GetKeyDown(Feu))
        {
            ElementPicked = Elements.Feu;
        }
        if (Input.GetKeyDown(Eau))
        {
            ElementPicked = Elements.Eau;
        }
        if (Input.GetKeyDown(Terre))
        {
            ElementPicked = Elements.Terre;
        }
        if (Input.GetKeyDown(Air))
        {
            ElementPicked = Elements.Air;
            
        }
    }
    public virtual void MoveInput()
    {
        cc.input.x = Input.GetAxis(horizontalInput);
        cc.input.z = Input.GetAxis(verticallInput);
    }

    protected virtual void CameraInput()
    {
        if (!cameraMain)
        {
            if (!Camera.main) Debug.Log("Missing a Camera with the tag MainCamera, please add one.");
            else
            {
                cameraMain = Camera.main;
                cc.rotateTarget = cameraMain.transform;
            }
        }

        if (cameraMain)
        {
            cc.UpdateMoveDirection(cameraMain.transform);
        }

        if (tpCamera == null)
            return;

        var Y = Input.GetAxis(rotateCameraYInput);
        var X = Input.GetAxis(rotateCameraXInput);
        var FOV = Input.GetAxis("Mouse ScrollWheel");
        if(FOV < 0f)
        {
            print("up");
        }
        if (FOV > 0f)
        {
            print("down");
        }
        tpCamera.RotateCamera(X, Y);
    }

    protected virtual void StrafeInput()
    {
        if (Input.GetKeyDown(strafeInput))
            cc.Strafe();
    }

    protected virtual void SprintInput()
    {
        if (Input.GetKeyDown(sprintInput))
            cc.Sprint(true);
        else if (Input.GetKeyUp(sprintInput))
            cc.Sprint(false);
    }

    /// <summary>
    /// Conditions to trigger the Jump animation & behavior
    /// </summary>
    /// <returns></returns>
    protected virtual bool JumpConditions()
    {
        return cc.isGrounded && cc.GroundAngle() < cc.slopeLimit && !cc.isJumping && !cc.stopMove;
    }

    /// <summary>
    /// Input to trigger the Jump 
    /// </summary>
    protected virtual void JumpInput()
    {
        if (Input.GetKeyDown(jumpInput) && JumpConditions())
            cc.Jump();
    }
    protected virtual void AttackInput()
    {
        // Lancer un spell
        if (Time.timeScale > 0f)
        {
            if (Input.GetKeyDown(FireInput))
            {
                GameObject Spell = new GameObject();
                    switch (ElementPicked)
                    {
                        case Elements.Feu:
                        if (Mana >= 20)
                        {
                            animateSpell();
                            Spell = Instantiate(Spells.spellFeu, transform.position + new Vector3(0, 1, 0), cameraMain.transform.rotation);
                            Spell.GetComponent<Rigidbody>().AddForce(cameraMain.transform.TransformDirection(Vector3.forward) * 500);
                            Mana = Mathf.Max(Mana - 20, 0);
                        }
                            break;
                        case Elements.Eau:
                        if (Mana >= 20)
                        {
                            animateSpell();
                            Spell = Instantiate(Spells.spellEau, transform.position + new Vector3(0, (float)0.08, 0), Quaternion.Euler(0, cameraMain.transform.eulerAngles.y, 0));
                            Mana = Mathf.Max(Mana - 20, 0);
                        }
                            break;
                        case Elements.Terre:
                        if (Mana >= 40)
                        {
                            animateSpell();
                            Spell = Instantiate(Spells.spellTerre, transform.position + new Vector3(0, 3, 0), new Quaternion(-90, 0, 0, 0));
                            Spell.GetComponent<Rigidbody>().AddForce(cameraMain.transform.TransformDirection(Vector3.forward) * 1500);
                            Mana = Mathf.Max(Mana - 40, 0);
                        }
                                //Spell.transform.eulerAngles = Quaternion.LookRotation(cameraMain.transform.eulerAngles).eulerAngles;
                                break;
                        case Elements.Air:
                        if (Mana >= 50)
                        {
                            animateSpell();
                            Mana = Mathf.Max(Mana - 50, 0);
                            Spell = Instantiate(Spells.spellAir, transform.position + cameraMain.transform.forward * 20 + new Vector3(0, 100, 0), Quaternion.identity);
                            RaycastHit hit;
                            // Does the ray intersect any objects excluding the player layer
                            if (Physics.Raycast(Spell.transform.position, Spell.transform.TransformDirection(new Vector3(0, -1, 0)), out hit, Mathf.Infinity, LayerMask.GetMask("Default")))
                            {
                                Vector3 pos = new Vector3(Spell.transform.position.x, Spell.transform.position.y - hit.distance, Spell.transform.position.z);
                                Spell.transform.position = pos;
                            }
                        }
                            break;

                    }
                    if(ElementPicked == Elements.Terre)
                {
                    Destroy(Spell, 10);
                }
                else
                {
                    Destroy(Spell, 5);
                }
                    
            }
        }
    }
    private void animateSpell()
    {
        cc.animator.CrossFadeInFixedTime("Fire", 0.1f);
        cc.animator.SetLayerWeight(1, 1);
    }
    /// <summary>
    /// Gestion de l'HUD des spells
    /// </summary>
    /// <returns></returns>
    IEnumerator SpellHUD()
    {
        while (true)
        {
            switch (ElementPicked)
            {
                case Elements.Feu:
                    AirSpellHUD.color = EarthSpellHUD.color = WaterSpellHUD.color = new Color32(255, 255, 255, 80);
                    FireSpellHUD.color = new Color32(255, 255, 255, 255);
                    break;
                case Elements.Eau:
                    AirSpellHUD.color = EarthSpellHUD.color = FireSpellHUD.color = new Color32(255, 255, 255, 80);
                    WaterSpellHUD.color = new Color32(255, 255, 255, 255);
                    break;
                case Elements.Terre:
                    AirSpellHUD.color = FireSpellHUD.color = WaterSpellHUD.color = new Color32(255, 255, 255, 80);
                    EarthSpellHUD.color = new Color32(255, 255, 255, 255);
                    break;
                case Elements.Air:
                    FireSpellHUD.color = EarthSpellHUD.color = WaterSpellHUD.color = new Color32(255, 255, 255, 80);
                    AirSpellHUD.color = new Color32(255, 255, 255, 255);
                    break;

            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    /// <summary>
    /// Dommages appliqués au joueur
    /// </summary>
    /// <param name="damage"></param>
    public void ApplyDamage(float damage)
    {
        if (!isDead)
        {
            Health = Health - damage;
            print(gameObject.name + " a subit " + damage + " points de dégâts");
            if (Health <= 0)
            {
                Dead();
            }
            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }
        }
    }

    /// <summary>
    /// Donne de l'xp au joueur
    /// </summary>
    /// <param name="xpToGet">xp à donner</param>
    public void getXp(float xpToGet)
    {
        xp += xpToGet;
        while(xp >= MaxXpBeforeLevelUp)
        {
            xp = xp - MaxXpBeforeLevelUp;
            level += 1;
            Spells.UpdateXp(level);
            levelUp();
            if (inventaire.activation)
            {
                inventaire.ActivateInventaire();
            }
            print("You won a level !");
            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }
    }
    /// <summary>
    /// Permet la montée de niveau du joueur
    /// </summary>
    private void levelUp()
    {
        speedMana += 2;
        speedHealth += 0.1f;
        MaxXpBeforeLevelUp *= 1.8f;
        
    }

    /// <summary>
    /// Le joueur meurt
    /// </summary>
    private void Dead()
    {
        cc.animator.Play("Death");
        isDead = true;
        CapsuleCollider cap;
        cap = GetComponent<CapsuleCollider>();
        cap.enabled = false;
        cap = GetComponentsInChildren<CapsuleCollider>()[1];
        cap.enabled = true;
        GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
    }
    #endregion
}