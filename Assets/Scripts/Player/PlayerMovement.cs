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

    public int maxHealth = 100;
    public int currentHealth;
    public bool isDead;
    private HealthBar healthBar;
    public dashCooldown dashUI1;
    public dashCooldown dashUI2;
    public dashCooldown dashUI3;

    public State state;
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    //public SpriteRenderer sr;
    public Camera cam;

    Vector2 movement;
    //Vector2 mousePos;
    //Vector2 lookDir;
    Vector3 moveDir;
    Vector3 rollDir;

    public Transform weapon;

    private TrailRenderer trail;
    private float rollSpeed;

    private Color dashColor = new Color(255, 255, 255, 255);
    public Color OriginalColor;
    private Color hurtColor;
    private Color invulnerableColor;
    //public Color midwayRoll;

    public SpriteRenderer body;
    //public LayerMask layerMask;

    private int LayerIgnoreRaycast;
    private int PlayerMask;
    private float rollSpeedDropMultiplier = 5f;
    private float rollSpeedMinimum = 50f;
    public bool isInvulnerable;


    public bool canDash;
    public float timeBetweenDashes;
    private float lastDash;


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
        isDead = false;
        state = State.Normal;
        currentHealth = maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            moveDir = new Vector3(0, 0).normalized;

        }
        //Debug.Log(Time.time);
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

                transform.DOScale((new Vector3(1f, 1f, 1f)), 0.0f);

                if (!isDead)
                {
                    movement.x = Input.GetAxisRaw("Horizontal");
                    movement.y = Input.GetAxisRaw("Vertical");
                    moveDir = new Vector3(movement.x, movement.y).normalized;
                }


                if (Input.GetButtonDown("Dash") && canDash)
                {
                    if (remainingBlinks > 0)
                    {
                        AudioManager.Instance.PlaySound(playerDash);
                        rollDir = moveDir;
                        rollSpeed = 90f;
                        lastDash = Time.time;
                        currentBlinkRechargeTime = 0f;
                        state = State.Rolling;
                    }
                    else
                    {
                        AudioManager.Instance.PlaySound(cantPress, 0.0f);
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
                    gameObject.layer = PlayerMask;
                    state = State.Normal;
                    Debug.Log("Once");
                }
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:

                rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
                break;
            case State.Rolling:
                rollSpeed -= rollSpeed * rollSpeedDropMultiplier * Time.deltaTime;
                rb.velocity = rollDir * rollSpeed;
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyBullet"))
        {
            OnHit();
        }
    }
    void TakeDamage(int damage)
    {
        if (!isInvulnerable)
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }
    }

    void OnRollingEffects()
    {
        gameObject.layer = LayerIgnoreRaycast;
        transform.DOScale((new Vector3(0.8f, 0.8f, 1f)), 0.0f);
        transform.DOScale((new Vector3(1.2f, 1.2f, 1f)), 0.35f);
        body.DOColor(dashColor, 0.0f);
        trail.emitting = true;
    }

    private void OnHit()
    {
        if (currentHealth <= 0)
        {
            isDead = true;  
        }
        else
        {
            TakeDamage(20);
            StartCoroutine(hurtAnimation());
        }
    }

    private IEnumerator hurtAnimation()
    {
        isInvulnerable = true;
        body.DOColor(hurtColor, 0.0f);
        body.DOColor(invulnerableColor, 0.15f);
        yield return new WaitForSeconds(0.20f);
        body.DOColor(hurtColor, 0.0f);
        body.DOColor(invulnerableColor, 0.15f);
        yield return new WaitForSeconds(0.20f);
        body.DOColor(hurtColor, 0.0f);
        body.DOColor(invulnerableColor, 0.15f);
        yield return new WaitForSeconds(0.20f);
        body.DOColor(OriginalColor, 0.0f);
        isInvulnerable = false;
    }


    private IEnumerator waitForUpdateDash(float time)
    {
        yield return new WaitForSeconds(time);
        currentBlinkRechargeTime += Time.deltaTime;
        dashUI1.SetDashTimer(currentBlinkRechargeTime);
    }
}
