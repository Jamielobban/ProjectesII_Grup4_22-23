using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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

                transform.DOScale((new Vector3(1f, 1f, 1f)), 0.0f);

                movement.x = Input.GetAxisRaw("Horizontal");
                movement.y = Input.GetAxisRaw("Vertical");

                mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
                moveDir = new Vector3(movement.x, movement.y).normalized;

                if (Input.GetButtonDown("Dash"))
                {
                    rollDir = moveDir;
                    rollSpeed = 90f;
                    state = State.Rolling;
                }

                break;

            case State.Rolling:

                transform.DOScale((new Vector3(0.8f, 0.8f, 1f)), 0.0f);
                transform.DOScale((new Vector3(1.2f, 1.2f, 1f)), 0.35f);
                sr.DOColor(dashColor, 0.0f);
                sr.DOColor(OriginalColor, 0.5f);
                trail.emitting = true;
                float rollSpeedDropMultiplier = 5f;
                rollSpeed -= rollSpeed * rollSpeedDropMultiplier * Time.deltaTime;
                //cam.DOShakePosition(0.4f, 1f, 1, 1, true, 0);
                float rollSpeedMinimum = 50f;
                if (rollSpeed < rollSpeedMinimum)
                {
                    state = State.Normal;
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
