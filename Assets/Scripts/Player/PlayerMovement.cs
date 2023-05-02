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
    public bool OnAir;
    public bool OnAirPlatform;

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
    public bool infinito;

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

    public Color OriginalColor;
    public Color hurtColor;
    public Color invulnerableColor;
    //public Color midwayRoll;

    public SpriteRenderer body;

    public GameObject rotatePoint;

    private int LayerIgnoreRaycast;
    private int PlayerMask;
    private float rollSpeedDropMultiplier = 5f;
    private float rollSpeedMinimum = 50f;
    public bool isInvulnerable;



    private Vector3 dir;

    public float angle;

    [SerializeField]
    AudioClip sonidoFuente;
    [SerializeField]
    Vector3 fuentePos;

    public AudioClip damageSound;

    bool canChange;

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
    AudioClip walking;



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
    int? parrySoundKey;
    int? walkingSoundKey;
    [SerializeField] AudioClip parrySound;


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


    bool canBlit = true;
    float blitCooldown;
    bool godMode;
    public bool isHit;
    private void Awake()
    {
        if (!infinito)
            currentHearts = PlayerPrefs.GetInt("Hearts", maxHearts);
        else
            currentHearts = maxHearts;
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


        playerDash = Resources.Load<AudioClip>("Sounds/Dash/PlayerDash");
        walking = Resources.Load<AudioClip>("Sounds/WalkPlayer");
        cantPress = Resources.Load<AudioClip>("Sounds/CantPress/cantPressSound");
        parrySound = Resources.Load<AudioClip>("Sounds/PlayerParry/Parry");
        potionsSystem = FindObjectOfType<PotionSystem>();
    }

    private void OnEnable()
    {
        if(SceneManager.GetActiveScene().name == "Bosque")
            AudioManager.Instance.LoadSound(sonidoFuente, fuentePos, 0, true);
    }

    public void Reaparecer()

    {

      SceneManager.LoadScene(PlayerPrefs.GetInt("IDScene", 1));

    }

    private IEnumerator guardarPosicion()

    {

        yield return new WaitForSeconds(0.25f);



        yield return new WaitUntil(() => (!isDashing && canMove && (((this.transform.parent.GetComponent<MovingPlatform>() == null) && !OnAir&& !OnAirPlatform)|| this.transform.parent.GetComponent<MovingPlatform>().OnPlayerStay)));

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
        if (!isDead)
        {
            OnAirPlatform = false;

            OnAir = false;

            isFall = false;

            body.enabled = true;

            isDead = false;

            canMove = true;

            disableDash = false;

            disableWeapons = false;

            body.sortingOrder = 0;

            this.transform.position = lastPositionSave;
        }
 

    }

    public void empezar()

    {



        body.sortingOrder = 0;

        isDead = false;

        canMove = true;

        disableDash = false;

        disableWeapons = false;

        canDash = true;

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

        if(((PlayerPrefs.GetInt("isDead") == 1)))
        {
            entrandoSala = false;
        }
        else
        {
            entrandoSala = true;

        }

        isDead = false;



        state = State.Rolling;

        //currentHealth = maxHealth;


        rollSpeed = 90f;

        justRolled = false;

        //backThemeKey = AudioManager.Instance.LoadSound(backgroundTheme, cam.transform, 0, true, false, 0.4f);
        //AudioManager.Instance.GetAudioFromDictionaryIfPossible(backThemeKey.Value).time = AudioManager.Instance.GetMusicTime();

        //if (backThemeKey.HasValue)

        //{

        //    AudioManager.Instance.GetAudioFromDictionaryIfPossible(backThemeKey.Value).volume = 0.4f;

        //}

    }
    // Update is called once per frame 
    void Update()
    {
        //AudioManager.Instance.SetMusicTime(AudioManager.Instance.GetAudioFromDictionaryIfPossible(backThemeKey.Value).time);

        if (Input.GetKeyDown(KeyCode.L)){
            godMode = true;
        }
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
        if (((Time.time - lastDash) >= timeBetweenDashes) && !disableDash)

        {
            if (!OnAir)
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

        if (!canBlit)
        {
            blitCooldown += Time.deltaTime;
            if (blitCooldown > 30)
            {
                blitCooldown = 0f;
                canBlit = true;
            }
        }

        //Debug.Log(walkingSoundKey.HasValue);
        switch (state)
        {
            case State.Normal:

                //transform.DOScale((new Vector3(1f, 1f, 1f)), 0.0f);

                if (canMove && !isDead)

                {

                    if (Input.GetButton("Heal") && currentHearts < maxHearts)
                    {

                        time += Time.deltaTime;

                        if(potionsSystem.amountToFill >= 50)

                        {

                            CinemachineShake.Instance.ShakeCamera(5f, .2f);

                            potionsSystem.potionPrefab.transform.DOShakePosition(0.5f, 1f, 5, 45, false,false, ShakeRandomnessMode.Full);

                        }

                        if(time >= 1f && potionsSystem.amountToFill >= 50)

                        {

                            potionsSystem.amountToFill -= 50;

                            PlayerPrefs.SetInt("Hearts", currentHearts);

                            Health();
                            potionsSystem.CheckPotionStatus();
                            time = 0;

                        }
                        if (Input.GetButtonUp("Heal") || time >= 1f)

                        {
                            potionsSystem.CheckPotionStatus();
                            time = 0;

                        }

                    }


                    movement.x = Input.GetAxisRaw("Horizontal");

                    movement.y = Input.GetAxisRaw("Vertical");

                    movement = Vector3.ClampMagnitude(movement, 1f);
                    //Debug.Log(movement.magnitude);
                    moveDir = new Vector3(movement.x, movement.y).normalized;

                    //

                    if (Input.GetKeyDown(KeyCode.Mouse1) && canBlit)

                    {

                        //Debug.Log("Parry");
                        parrySoundKey = AudioManager.Instance.LoadSound(parrySound, this.transform.position);

                        myBlit.isExpanding = true;
                        canBlit = false;

                    }

                    if (moveDir.magnitude == 1)

                    {

                        isMoving = true;
                        if (!walkingSoundKey.HasValue)
                        {
                            walkingSoundKey = AudioManager.Instance.LoadSound(walking, this.transform,0f,true,true,1f);
                        }
                        
                    }
                    else

                    {

                        isMoving = false;
                        //walkingSoundKey.
                        if (walkingSoundKey.HasValue)
                        {
                            AudioManager.Instance.RemoveAudio(walkingSoundKey.Value);
                            walkingSoundKey = null;
                        }

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

                    rb.AddForce(movement.normalized * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Force);

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

        StartCoroutine(waitForLayerChange(0.35f));

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
        if (!godMode)
        {
            if (!isDead && !isFall)

            {

                OnHit(damage);

                StartCoroutine(hurtAnimation());

            }
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

        if(!godMode) TakeDamage(damage);

        CinemachineShake.Instance.ShakeCamera(5f, .2f);

        if (currentHearts <= 0)

        {

            restart = true;

            anim.SetBool("isDead", true);

            isDead = true;

            healthUI.DrawAllEmpty();

            Debug.Log(currentHearts);

            canMove = true;

            disableDash = false;

            canDash = false;

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
        isHit = true;
        isInvulnerable = true;

        if (currentHearts % 2 == 0 && healthUI.emptyHeartArray != null)
        {
             healthUI.emptyHeartToFlash.GetComponent<Animator>().enabled = true;
        }
        else
        {
            healthUI.heartToChange.GetComponent<Animator>().enabled = true;
            //Debug.Log("Flashed half heart");
        }
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

        yield return new WaitForSeconds(0.40f);

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
