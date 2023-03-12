using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public enum State
    {
        Normal, Rolling, Hit
    }
    [SerializeField]
    AudioClip backgroundTheme;
    int? backThemeKey;
    [SerializeField] Animator anim;
    [SerializeField] Animator fxAnim;
    public SpriteRenderer playerSprite;
    public Transform firePoint;
    public RightHand _rightHand;
    public float knockbackForce;
    public float knockbackForceDrop = 5f;
    public float knockbackMinimum;
    public bool knockbackSet;


    public int maxHearts;
    public int currentHearts;
    public float currentHealth;


    public bool isDead;
    private HealthBar healthBar;
    public bool isDashing;
    public GameObject floorBlood;

    public bool isInBlood;

    public bool entrandoSala;
    public float knockbackForceCheck;

    public State state;
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    //public SpriteRenderer sr;
    public Camera cam;
    public RightHand weaponHand;

    public Vector2 movement;
    //Vector2 mousePos;
    //Vector2 lookDir;
    Vector3 moveDir;
    public Vector3 rollDir;

    private TrailRenderer trail;
    public float rollSpeed;
    public bool knockback;
    public Color dashColor;
    public Color OriginalColor;
    public Color hurtColor;
    public Color invulnerableColor;
    //public Color midwayRoll;

    public SpriteRenderer body;
    public SpriteRenderer[] weaponSprites;
    //public LayerMask layerMask;

    public GameObject rotatePoint;

    private int LayerIgnoreRaycast;
    private int PlayerMask;
    private float rollSpeedDropMultiplier = 5f;
    private float rollSpeedMinimum = 50f;
    public bool isInvulnerable;

    private Vector3 dir;
    public float angle;

    public AudioClip damageSound;

    public bool canDash;
    public float timeBetweenDashes;
    private float lastDash;
    public bool canMove = false;
    public bool isMoving = false;
    public bool isFall;
    [SerializeField] private int maxBlinks = 3;
    [SerializeField] private float blinkRechargeTime;


    AudioClip playerDash;
    AudioClip cantPress;

    public float dashTimer;
    public float dashTimer2;
    public float dashTime;
    public float currentBlinkRechargeTime = 0f;
    public float currentBlinkRechargeTime2 = 0f;
    public float currentBlinkRechargeTime3 = 0f;
    [SerializeField] private int remainingBlinks;
    [SerializeField] public HeartSystem healthUI;

    float rollSpeedCheck;

    int? cantPressSoundKey;
    int? dashSoundKey;
    int? damageSoundKey;

    bool justRolled;

    public BlitController myBlit;

    public bool disableDash;
    public bool disableWeapons;
    CheckpointsList list;

    public bool endTutorial;

    public bool restart;
    public Vector3 lastPositionSave;

    float time = 0;

    [SerializeField] PotionSystem potionsSystem;

    private void Awake()
    {        currentHearts = PlayerPrefs.GetInt("Hearts", maxHearts);

        healthUI = FindObjectOfType<HeartSystem>();
        isFall = false;
        restart = false;
        StartCoroutine(guardarPosicion());

        lastPositionSave = this.transform.position;
        endTutorial = false;
        healthUI.DrawHearts();

        if(GameObject.FindGameObjectWithTag("CheckPoints") != null)
        list = GameObject.FindGameObjectWithTag("CheckPoints").GetComponent<CheckpointsList>();

        //DontDestroyOnLoad(this);

        trail = GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody2D>();
        LayerIgnoreRaycast = LayerMask.NameToLayer("IgnoreEverything");
        PlayerMask = LayerMask.NameToLayer("Player");

        playerDash = Resources.Load<AudioClip>("Sounds/Dash/dashEffect2");
        cantPress = Resources.Load<AudioClip>("Sounds/CantPress/cantPressSound");

        potionsSystem = FindObjectOfType<PotionSystem>();
    }

    public void Reaparecer()
    {
      SceneManager.LoadScene(PlayerPrefs.GetInt("IDScene"));
    }
    private IEnumerator guardarPosicion()
    {
        yield return new WaitForSeconds(2f);

        //yield return new WaitUntil(() => (!isDashing&&canMove&&this.transform.parent.GetComponent<MovingPlatform>() == null));
        lastPositionSave = this.transform.position;

        StartCoroutine(guardarPosicion());
    }
    public void SpawnSalaPrincipal()
    {

        currentHearts = maxHearts;
        healthUI.DrawHearts();

    }

    public void reaparecerCaida()
    {
        isFall = false;
        body.enabled = true;
        isDead = false;
        canMove = true;
        disableDash = false;
        disableWeapons = false;
        body.sortingOrder = 0;


        this.transform.position = lastPositionSave;
    }
    public void empezar()
    {

        body.sortingOrder = 0;
        isDead = false;
        canMove = true;
        disableDash = false;
        disableWeapons = false;
        currentHearts = maxHearts;
        healthUI.DrawHearts();

        this.transform.position = list.actualSpawn.position;

    }
    public void reiniciar()
    {

        currentHearts = maxHearts;
        healthUI.DrawHearts();

    }
    public void Start()
    {
        canDash = true;
        entrandoSala = true;
        isDead = false;

        state = State.Rolling;
        //currentHealth = maxHealth;
        currentHearts = maxHearts;
        rollSpeed = 90f;
        justRolled = false;
        backThemeKey = AudioManager.Instance.LoadSound(backgroundTheme, this.transform, 0, true, false, 0.4f);
        //if (backThemeKey.HasValue)
        //{
        //    AudioManager.Instance.GetAudioFromDictionaryIfPossible(backThemeKey.Value).volume = 0.4f;
        //}
    }
    // Update is called once per frame
    void Update()
    {

        if(disableWeapons && rotatePoint.activeSelf == true)
        {
            rotatePoint.SetActive(false);
        }
        else if(!disableWeapons && rotatePoint.activeSelf == false)
        {
            rotatePoint.SetActive(true);

        }

        dir = rotatePoint.transform.position - firePoint.transform.position;
        angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //Debug.Log(angle);

        if (angle > -90 && angle < 90 && canMove)
        {
            playerSprite.flipX = true;
        }
        else if (canMove)
        {
            playerSprite.flipX = false;
        }

        if ((angle > 0 && angle < 180)&&canMove)
        {
            playerSprite.sortingOrder = 0;
        }
        else if(canMove)
        {
            playerSprite.sortingOrder = 1;
        }

        weaponSprites = rotatePoint.GetComponentsInChildren<SpriteRenderer>();


        if (((Time.time - lastDash) >= timeBetweenDashes)&& !disableDash)
        {
            canDash = true;
        }
        else
        {
            canDash = false;
        }

        //dashController();
        if (remainingBlinks == 2)
        {
            //dashUI1.SetDashTimer(0);
            //currentBlinkRechargeTime += Time.deltaTime;
            //dashUI1.SetDashTimer(currentBlinkRechargeTime);
            if ((Time.time - lastDash) >= timeBetweenDashes)
            {
                //dashUI1.SetDashTimer(currentBlinkRechargeTime);
                currentBlinkRechargeTime += Time.deltaTime;
            }
            if (currentBlinkRechargeTime >= blinkRechargeTime)
            {
                remainingBlinks++;
                currentBlinkRechargeTime = 0f;
            }
        }
        if (remainingBlinks == 1)
        {
            //dashUI2.SetDashTimer(0);
            //currentBlinkRechargeTime += Time.deltaTime;
            //dashUI1.SetDashTimer(currentBlinkRechargeTime);
            if ((Time.time - lastDash) >= timeBetweenDashes)
            {
                currentBlinkRechargeTime2 += Time.deltaTime;
                //dashUI2.SetDashTimer(currentBlinkRechargeTime2);
            }
            if (currentBlinkRechargeTime2 >= blinkRechargeTime)
            {
                remainingBlinks++;
                currentBlinkRechargeTime2 = 0f;
            }
        }
        //Debug.Log(time);
        if (remainingBlinks == 0)
        {
            //dashUI3.SetDashTimer(0);
            //currentBlinkRechargeTime += Time.deltaTime;
            //dashUI1.SetDashTimer(currentBlinkRechargeTime);
            if ((Time.time - lastDash) >= timeBetweenDashes)
            {
                currentBlinkRechargeTime3 += Time.deltaTime;
                //dashUI3.SetDashTimer(currentBlinkRechargeTime3);
            }
            if (currentBlinkRechargeTime3 >= blinkRechargeTime)
            {
                remainingBlinks++;
                currentBlinkRechargeTime3 = 0f;
            }
        }
        if (isDashing && isMoving)
        {
            anim.SetBool("isDashing", true);
            fxAnim.SetBool("isDashingFX", true);
        }
        else
        {
            anim.SetBool("isDashing", false);
        }

        if (isMoving)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        switch (state)
        {
            case State.Normal:

                //transform.DOScale((new Vector3(1f, 1f, 1f)), 0.0f);

                if (canMove && !isDead)
                {
                    if (Input.GetButton("Heal"))
                    {
                        time += Time.deltaTime;
                        if(potionsSystem.amountToFill > 50)
                        {
                            CinemachineShake.Instance.ShakeCamera(5f, .2f);
                            potionsSystem.potionPrefab.transform.DOShakePosition(0.5f, 1f, 5, 45, false,false, ShakeRandomnessMode.Full);
                        }
                        if(time >= 1f && potionsSystem.amountToFill >= 50)
                        {
                            potionsSystem.amountToFill -= 50;
                            potionsSystem.CheckPotionStatus();
                            Health();
                        }
                    }
                    if (Input.GetButtonUp("Heal") || time >= 1.5f)
                    {
                        time = 0;
                    }
                    movement.x = Input.GetAxisRaw("Horizontal");
                    movement.y = Input.GetAxisRaw("Vertical");
                    moveDir = new Vector3(movement.x, movement.y).normalized;
                    //
                    if (Input.GetButtonDown("Parry"))
                    {
                        Debug.Log("Parry");
                        myBlit.isExpanding = true;
                    }
                    if (moveDir.magnitude == 1)
                    {
                        isMoving = true;
                    }
                    else
                    {
                        isMoving = false;
                    }
                    // Debug.Log(movement.x + "This is X axis");
                }
                else
                {
                    movement.x = 0;
                    movement.y = 0;
                }

                if (Input.GetButtonDown("Dash") && canDash && isMoving)
                {
                    if (remainingBlinks > 0)
                    {
                        isDashing = true;
                        dashTimer = Time.time;
                        //anim.SetBool("isDashing", true);
                        dashSoundKey = AudioManager.Instance.LoadSound(playerDash, this.gameObject.transform);
                        rollDir = moveDir;
                        lastDash = Time.time;
                        currentBlinkRechargeTime = 0f;
                        state = State.Rolling;
                    }
                    else
                    {
                        cantPressSoundKey = AudioManager.Instance.LoadSound(cantPress, this.gameObject.transform);
                    }

                }

                break;

            case State.Rolling:

                if (remainingBlinks <= 0) { state = State.Normal; }

                //remainingBlinks--;
                OnRollingEffects();
                if (justRolled)
                {
                    rollSpeedCheck = rollSpeed;
                    justRolled = false;
                }
                if (rollSpeedCheck < rollSpeedMinimum)
                {
                    remainingBlinks--;
                    trail.emitting = false;
                    //body.DOColor(OriginalColor, 0.5f);
                    //for(int i = 0; i < weaponSprites.Length; i++)
                    //{
                    //    weaponSprites[i].DOColor(weaponHand.GetColor(), 0.5f);
                    //}
                    //isDashing = false;
                    justRolled = true;
                    dashTimer2 = Time.time;
                    dashTime = dashTimer2 - dashTimer;
                    state = State.Normal;
                    //Debug.Log("Once");
                }
                break;
        }
        //Debug.Log(knockbackForce);
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:
                if (!knockback)
                {
                    rb.AddForce(movement * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Force);
                }
                else
                {
                    //rb.AddForce(movement * 500000 * Time.fixedDeltaTime, ForceMode2D.Force);
                    OnKnockbackShoot(knockbackForceCheck, _rightHand.weaponInHand.GetKnockbackMinimum());
                }
                break;
            case State.Rolling:
                rollSpeedCheck -= rollSpeedCheck * rollSpeedDropMultiplier * Time.deltaTime;
                rb.velocity = rollDir * rollSpeedCheck;
                break;
            default:
                break;
        }
    }

    public void OnKnockbackShoot(float knockbackForce, float knockbackMinimum)
    {
        if (!knockbackSet)
        {
            knockbackForceCheck = _rightHand.weaponInHand.GetKnockbackForce();
            knockbackSet = true;
        }

        knockbackForceCheck -= _rightHand.weaponInHand.GetKnockbackForce() * Time.deltaTime;
        if (knockbackForceCheck < _rightHand.weaponInHand.GetKnockbackMinimum())
        {
            knockback = false;
            knockbackSet = false;
        }
        //rb.AddForce(movement * 500 * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }
    public void TakeDamage(int damage)
    {
        if (!isInvulnerable)
        {
            if (currentHearts > 0)
            {
                currentHearts -= damage;
            }
            else
            {
                currentHearts = 0;
            }
            GameObject.Instantiate(floorBlood, this.transform.position, this.transform.rotation);
            damageSoundKey = AudioManager.Instance.LoadSound(damageSound, this.gameObject.transform);
            //healthBar.SetHealth(currentHealth);
        }
    }


    public void TakeLavaDamage()
    {
        if (!isInvulnerable)
        {
            currentHealth -= 5;
            healthBar.SetHealth(currentHealth);
        }
    }


    void OnRollingEffects()
    {
        StartCoroutine(waitForLayerChange(0.25f));
        gameObject.layer = LayerIgnoreRaycast;
        //transform.DOScale((new Vector3(0.9f, 0.7f, 1f)), 0.0f);
        //transform.DOScale((new Vector3(1.2f, 1.2f, 1f)), 0.35f);
        //body.DOColor(dashColor, 0.0f);
        //for (int i = 0; i < weaponSprites.Length; i++)
        //{
        //    weaponSprites[i].DOColor(weaponHand.GetColor(), 0.5f);
        //}
        trail.emitting = true;
    }

    public void GetDamage(int damage)
    {
        //Debug.Log(damage);
        if(!isDead && !isFall)
        {
            OnHit(damage);
            StartCoroutine(hurtAnimation());
        }


    }

    public void Health()
    {
        currentHearts +=2;

        if (currentHearts > maxHearts)
        {
            currentHearts = maxHearts;
        }

        healthUI.DrawHearts();
    }
    public void OnHit(int damage)
    {
        TakeDamage(damage);

        if (currentHearts <= 0)
        {
            restart = true;

            isDead = true;
            healthUI.DrawAllEmpty();
            Debug.Log(currentHearts);
            canMove = true;
            disableDash = false;
            disableWeapons = false;

            //Debug.Log("Dead");
        }
        else
        {
            healthUI.DrawHearts();
            //Debug.Log("Still alive");
        }
    }

    private IEnumerator hurtAnimation()
    {
        isInvulnerable = true;

        if (currentHearts % 2 == 0 && healthUI.emptyHeartArray != null)
        {
                healthUI.emptyHeartToFlash.GetComponent<Animator>().enabled = true;
                //Debug.Log("Flashed right heart");
        }
        else
        {
            healthUI.heartToChange.GetComponent<Animator>().enabled = true;
            //Debug.Log("Flashed half heart");
        }

        //Debug.Log("Now invulnerable");
        body.DOColor(hurtColor, 0.0f);
        body.DOColor(invulnerableColor, 0.15f);

        yield return new WaitForSeconds(0.20f);

        if (currentHearts % 2 == 0)
        {
            healthUI.emptyHeartToFlash.GetComponent<Animator>().enabled = false;
            healthUI.emptyHeartToFlash.SetHeartImage(healthUI.emptyHeartToFlash._emptyStatus);
        }
        else
        {
            healthUI.heartToChange.GetComponent<Animator>().enabled = false;
            healthUI.heartToChange.SetHeartImage(healthUI.heartToChange._status);
        }



        body.DOColor(hurtColor, 0.0f);
        body.DOColor(invulnerableColor, 0.15f);


        yield return new WaitForSeconds(0.20f);


        body.DOColor(hurtColor, 0.0f);
        body.DOColor(invulnerableColor, 0.15f);

        yield return new WaitForSeconds(0.20f);
        body.DOColor(OriginalColor, 0.0f);

       
        //Debug.Log("No longer invlunerable");
        isInvulnerable = false;

    }

    private IEnumerator waitForUpdateDash(float time)
    {
        yield return new WaitForSeconds(time);
        currentBlinkRechargeTime += Time.deltaTime;
        //dashUI1.SetDashTimer(currentBlinkRechargeTime);
    }


    private IEnumerator waitForLayerChange(float waitTime)
    {
        gameObject.layer = LayerIgnoreRaycast;
        yield return new WaitForSeconds(waitTime);
        gameObject.layer = PlayerMask;
        fxAnim.SetBool("isDashingFX", false);
        isDashing = false;

        //anim.SetBool("isDashing", false);
    }

    private IEnumerator TakeLavaCoroutine(float time) {
        InvokeRepeating("TakeLavaDamage()", 0.5f, 1f);
        yield return new WaitForSeconds(time);
    }
}
