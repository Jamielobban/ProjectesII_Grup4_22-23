using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private enum State
    {
        Normal, Rolling
    }

    private State state;
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;
    Vector2 lookDir;
    Vector3 moveDir;
    Vector3 rollDir;

    private TrailRenderer trail;
    private float rollSpeed;
    public Color dashColor;
    public Color OriginalColor;
    //public Color midwayRoll;

    private void Awake()
    {
        trail = GetComponent<TrailRenderer>();
        sr = GetComponent<SpriteRenderer>();
        state = State.Normal;
    }
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Normal:

                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");

                mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                moveDir = new Vector3(movement.x, movement.y).normalized;

                if (Input.GetButtonDown("Dash"))
                {
                    rollDir = moveDir;
                    rollSpeed = 80f;
                    state = State.Rolling;

                }

                break;
            case State.Rolling:
                this.transform.localScale = new Vector3(1, 0.6f, 1);
                trail.emitting = true;
                sr.color = dashColor;
                float rollSpeedDropMultiplier = 5f;
                rollSpeed -= rollSpeed * rollSpeedDropMultiplier * Time.deltaTime;

                float rollSpeedMinimum = 50f;
                if (rollSpeed < rollSpeedMinimum)
                {
                    state = State.Normal;
                    trail.emitting = false;
                    sr.color = OriginalColor;
                    this.transform.localScale = new Vector3(1, 1, 1);
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

                lookDir = mousePos - rb.position;
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
                rb.rotation = angle;
                break;
            case State.Rolling:
                rb.velocity = rollDir * rollSpeed;
                break;
            default:
                break;
        }

    }
}