using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{

    public enum State
    {
        Normal, Rolling, Hit
    }

    public RightHand _rightHand;
    public float knockbackForce;
    public float knockbackForceDrop = 5f;
    public float knockbackMinimum;
    public bool knockbackSet;
    public int maxHealth = 100;
    public float currentHealth;
    public bool isDead;
    private HealthBar healthBar;
    public dashCooldown dashUI1;
    public dashCooldown dashUI2;
    public dashCooldown dashUI3;

    public GameObject floorBlood;

    public float knockbackForceCheck;

    public State state;
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    //public SpriteRenderer sr;
    public Camera cam;
    public RightHand weaponHand;

    Vector2 movement;
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
    public SpriteRenderer[]weaponSprites;
    //public LayerMask layerMask;

    public GameObject rotatePoint;

    private int LayerIgnoreRaycast;
    private int PlayerMask;
    private float rollSpeedDropMultiplier = 5f;
    private float rollSpeedMinimum = 50f;
    public bool isInvulnerable;
    

    public AudioClip damageSound;

    public bool canDash;
    public float timeBetweenDashes;
    private float lastDash;
    public bool canMove = false;
    public bool isMoving = false;

    [SerializeField] private int maxBlinks = 3;
    [SerializeField] private float blinkRechargeTime;


    AudioClip playerDash;
    AudioClip cantPress;


    public float currentBlinkRechargeTime = 0f;
    public float currentBlinkRechargeTime2 = 0f;
    public float currentBlinkRechargeTime3 = 0f;
    [SerializeField] private int remainingBlinks;
    private void Awake()
    {
        // GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>().enemiesInRoom.Remove(this.gameObject);
        healthBar = Canvas.FindObjectOfType<HealthBar>();
        trail = GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody2D>();
        LayerIgnoreRaycast = LayerMask.NameToLayer("IgnoreEverything");
        PlayerMask = LayerMask.NameToLayer("Player");
        dashUI1.SetMaxDashTimer(blinkRechargeTime);
        dashUI2.SetMaxDashTimer(blinkRechargeTime);
        dashUI3.SetMaxDashTimer(blinkRechargeTime);
        healthBar.SetMaxHealth(maxHealth);
        playerDash = Resources.Load<AudioClip>("Sounds/Dash/dashEffect2");
        cantPress = Resources.Load<AudioClip>("Sounds/CantPress/cantPressSound");
    }

    public void Start()
    {
        _rightHand = GetComponentInChildren<RightHand>();
      
        isDead = false;
        state = State.Normal;
        currentHealth = maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        if(_rightHand  == null)
        {
            Debug.Log("nOTHING FOUND");
        }
        else
        {
            Debug.Log(_rightHand.weaponInHand.GetKnockbackMinimum());
        }
        weaponSprites = rotatePoint.GetComponentsInChildren<SpriteRenderer>();


        if ((Time.time - lastDash) >= timeBetweenDashes)
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
            dashUI1.SetDashTimer(0);
            //currentBlinkRechargeTime += Time.deltaTime;
            //dashUI1.SetDashTimer(currentBlinkRechargeTime);
            if ((Time.time - lastDash) >= timeBetweenDashes)
            {
                dashUI1.SetDashTimer(currentBlinkRechargeTime);
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
            dashUI2.SetDashTimer(0);
            //currentBlinkRechargeTime += Time.deltaTime;
            //dashUI1.SetDashTimer(currentBlinkRechargeTime);
            if ((Time.time - lastDash) >= timeBetweenDashes)
            {
                currentBlinkRechargeTime2 += Time.deltaTime;
                dashUI2.SetDashTimer(currentBlinkRechargeTime2);
            }
            if (currentBlinkRechargeTime2 >= blinkRechargeTime)
            {
                remainingBlinks++;
                currentBlinkRechargeTime2 = 0f;
            }
        }
        if (remainingBlinks == 0)
        {
            dashUI3.SetDashTimer(0);
            //currentBlinkRechargeTime += Time.deltaTime;
            //dashUI1.SetDashTimer(currentBlinkRechargeTime);
            if ((Time.time - lastDash) >= timeBetweenDashes)
            {
                currentBlinkRechargeTime3 += Time.deltaTime;
                dashUI3.SetDashTimer(currentBlinkRechargeTime3);
            }
            if (currentBlinkRechargeTime3 >= blinkRechargeTime)
            {
                remainingBlinks++;
                currentBlinkRechargeTime3 = 0f;
            }
        }
    
        switch (state)
        {
            case State.Normal:

                //transform.DOScale((new Vector3(1f, 1f, 1f)), 0.0f);

                if (canMove && !isDead)
                {
                    if (Input.GetButtonDown("Heal"))
                    {
                        currentHealth = 99999;
                        healthBar.SetHealth(currentHealth);
                    }
                    movement.x = Input.GetAxisRaw("Horizontal");
                    movement.y = Input.GetAxisRaw("Vertical");
                    moveDir = new Vector3(movement.x, movement.y).normalized;
                    //
                    if(moveDir.magnitude == 1)
                    {
                        isMoving = true;
                    }
                    else
                    {
                        isMoving = false;
                    }
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
                        AudioManager.Instance.PlaySound(playerDash, this.gameObject.transform);
                        rollDir = moveDir;
                        rollSpeed = 90f;
                        lastDash = Time.time;
                        currentBlinkRechargeTime = 0f;
                        state = State.Rolling;
                    }
                    else
                    {
                        AudioManager.Instance.PlaySound(cantPress, this.gameObject.transform);
                    }

                }

                break;

            case State.Rolling:
                
                if (remainingBlinks <= 0) { state = State.Normal; }

                //remainingBlinks--;
                OnRollingEffects();


                if (rollSpeed < rollSpeedMinimum)
                {
                    remainingBlinks--;
                    trail.emitting = false;
                    body.DOColor(OriginalColor, 0.5f);
                    for(int i = 0; i < weaponSprites.Length; i++)
                    {
                        weaponSprites[i].DOColor(weaponHand.GetColor(), 0.5f);
                    }
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
                    rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
                    knockbackForce = 90f;
                }
                else
                {
                    OnKnockbackShoot(knockbackForceCheck, _rightHand.weaponInHand.GetKnockbackMinimum());
                }  
                break;
            case State.Rolling:
                rollSpeed -= rollSpeed * rollSpeedDropMultiplier * Time.deltaTime;
                rb.velocity = rollDir * rollSpeed;
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
    }
    void TakeDamage(float damage)
    {
        if (!isInvulnerable)
        {
            currentHealth -= damage;
            GameObject.Instantiate(floorBlood, this.transform.position, this.transform.rotation);
            AudioManager.Instance.PlaySound(damageSound, this.gameObject.transform);
            healthBar.SetHealth(currentHealth);
        }
    }

    void OnRollingEffects()
    {
        StartCoroutine(waitForLayerChange(0.85f));
        gameObject.layer = LayerIgnoreRaycast;
        transform.DOScale((new Vector3(0.9f, 0.7f, 1f)), 0.0f);
        transform.DOScale((new Vector3(1.2f, 1.2f, 1f)), 0.35f);
        body.DOColor(dashColor, 0.0f);
        for (int i = 0; i < weaponSprites.Length; i++)
        {
            weaponSprites[i].DOColor(weaponHand.GetColor(), 0.5f);
        }
        trail.emitting = true;
    }

    public void GetDamage(float damage)
    {
        //Debug.Log(damage);
        OnHit(damage);

    }

    private void OnHit(float damage)
    {
        TakeDamage(damage);
        StartCoroutine(hurtAnimation());
        if (currentHealth <= 0)
        {
            isDead = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private IEnumerator hurtAnimation()
    {
        isInvulnerable = true;
        body.DOColor(hurtColor, 0.0f);
        body.DOColor(invulnerableColor, 0.15f);
        for (int i = 0; i < weaponSprites.Length; i++)
        {
            weaponSprites[i].DOColor(hurtColor, 0.0f);
        }
        for (int i = 0; i < weaponSprites.Length; i++)
        {
            weaponSprites[i].DOColor(invulnerableColor, 0.15f);
        }        
        yield return new WaitForSeconds(0.20f);
        body.DOColor(hurtColor, 0.0f);
        body.DOColor(invulnerableColor, 0.15f);
        for (int i = 0; i < weaponSprites.Length; i++)
        {
            weaponSprites[i].DOColor(hurtColor, 0.0f);
        }
        for (int i = 0; i < weaponSprites.Length; i++)
        {
            weaponSprites[i].DOColor(invulnerableColor, 0.15f);
        }
        yield return new WaitForSeconds(0.20f);
        body.DOColor(hurtColor, 0.0f);
        body.DOColor(invulnerableColor, 0.15f);
        for (int i = 0; i < weaponSprites.Length; i++)
        {
            weaponSprites[i].DOColor(hurtColor, 0.0f);
        }
        for (int i = 0; i < weaponSprites.Length; i++)
        {
            weaponSprites[i].DOColor(invulnerableColor, 0.15f);
        }
        yield return new WaitForSeconds(0.20f);
        body.DOColor(OriginalColor, 0.0f);
        for (int i = 0; i < weaponSprites.Length; i++)
        {
            weaponSprites[i].DOColor(weaponHand.GetColor(), 0.5f);
        }
        isInvulnerable = false;
        
    }

    private IEnumerator waitForUpdateDash(float time)
    {
        yield return new WaitForSeconds(time);
        currentBlinkRechargeTime += Time.deltaTime;
        dashUI1.SetDashTimer(currentBlinkRechargeTime);
    }
    public IEnumerator waitForKnockback(float time)
    {
        yield return new WaitForSeconds(time);
        //knockback = false;
        Debug.Log("Waiting");
    }

    private IEnumerator waitForLayerChange(float waitTime)
    {
        gameObject.layer = LayerIgnoreRaycast;
        yield return new WaitForSeconds(waitTime);
        gameObject.layer = PlayerMask;
    }
}
