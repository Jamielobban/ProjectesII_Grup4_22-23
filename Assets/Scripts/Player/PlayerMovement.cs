using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{

    private enum State
    {
        Normal, Rolling, Hit
    }

    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    private State state;
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

    public Color dashColor;
    public Color OriginalColor;
    public Color hurtColor;
    public Color invulnerableColor;
    //public Color midwayRoll;

    public SpriteRenderer body;
    //public LayerMask layerMask;

    private int LayerIgnoreRaycast;
    private int PlayerMask;
    private float rollSpeedDropMultiplier = 5f;
    private float rollSpeedMinimum = 50f;
    public bool isInvulnerable;
    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody2D>();
        LayerIgnoreRaycast = LayerMask.NameToLayer("IgnoreEverything");
        PlayerMask = LayerMask.NameToLayer("Player");
    }

    public void Start()
    {
        state = State.Normal;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Normal:

                transform.DOScale((new Vector3(1f, 1f, 1f)), 0.0f);

                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");
                moveDir = new Vector3(movement.x, movement.y).normalized;

                if (Input.GetButtonDown("Dash"))
                {
                    rollDir = moveDir;
                    rollSpeed = 90f;
                    state = State.Rolling;
                }

                break;

            case State.Rolling:

                OnRollingEffects();
                if (rollSpeed < rollSpeedMinimum)
                {
                    state = State.Normal;
                    gameObject.layer = PlayerMask;
                    trail.emitting = false;
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
        body.DOColor(OriginalColor, 0.5f);
        trail.emitting = true;
    }

    private void OnHit()
    {
        TakeDamage(20);
        StartCoroutine(hurtAnimation());
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
}
