using UnityEngine;
using System;
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
    public KeyCode FireInput = KeyCode.A;
    [Header("Camera Input")]
    public string rotateCameraXInput = "Mouse X";
    public string rotateCameraYInput = "Mouse Y";
    [Header("Spells")]
    public SpellManager Spells;
    [Header("Caractéristiques")]
    public float Health;
    public float MaxHealth;
    public float Mana;
    public float MaxMana;
    public float speedMana = 15f;
    public int level;
    public float xp;
    public float MaxXpBeforeLevelUp;
    private bool isDead;
    private Transform MenuLevelUp;
    [HideInInspector] public vThirdPersonController cc;
    [HideInInspector] public vThirdPersonCamera tpCamera;
    [HideInInspector] public Camera cameraMain;
    
    

    #endregion

    protected virtual void Start()
    {
        Spells = GameObject.FindWithTag("SpellManager").GetComponent<SpellManager>();
        InitializeStats();
        InitilizeController();
        InitializeTpCamera();
    }

    protected virtual void FixedUpdate()
    {
        if (!isDead)
        {
            cc.UpdateMotor();               // updates the ThirdPersonMotor methods
            cc.ControlLocomotionType();     // handle the controller locomotion type and movespeed
            cc.ControlRotationType();       // handle the controller rotation type
            Mana = Mathf.Min(Mana + (Time.deltaTime * speedMana) * MaxMana / 100, MaxMana);
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

    protected virtual void InitializeStats()
    {
        MaxHealth = 100f;
        MaxMana = 100f;
        Health = MaxHealth;
        Mana = MaxMana;
        level = 1;
        xp = 0;
        MaxXpBeforeLevelUp = 100;
        isDead = false;
        MenuLevelUp = GameObject.Find("LevelUpMenu").transform.Find("PauseMenu");
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
        AttackInput();
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
        if (Input.GetKeyDown(FireInput))
        {
            if (Mana >= 20)
            {
                Mana = Mathf.Max(Mana - 20, 0);
                cc.animator.CrossFadeInFixedTime("Fire", 0.1f);
                cc.animator.SetLayerWeight(1, 1);
                GameObject fireball = Instantiate(Spells.spellFireBall, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                fireball.GetComponent<Rigidbody>().AddForce(cameraMain.transform.TransformDirection(Vector3.forward) * 500);
                Destroy(fireball, 5);
            }
        }
    }

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

    public void getXp(float xpToGet)
    {
        xp += xpToGet;
        while(xp >= MaxXpBeforeLevelUp)
        {
            xp = xp - MaxXpBeforeLevelUp;
            level += 1;
            Spells.UpdateXp(level);
            levelUp();
            print("You won a level !");
            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }
    }
    private void levelUp()
    {
        speedMana += 2;
        MaxXpBeforeLevelUp *= 1.2f;
        MenuLevelUp.gameObject.SetActive(true);
        Time.timeScale = 0f;

    }

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