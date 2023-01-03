using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashingEnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D myrb;
    private PlayerMovement player;
    private Vector3 myCheck;
    private CircleCollider2D childCollider;
    public bool isDashing;
    public bool canDash;
    public Vector3 dashPos;
    void Start()
    {
        myrb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        childCollider = GetComponentInChildren<CircleCollider2D>();
        isDashing = false;
        canDash = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDashing)
        {
        myCheck = Vector3.MoveTowards(this.transform.position, player.transform.position, 0.055f);
        this.transform.position = myCheck;
        }
        if (isDashing)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, dashPos, 0.1f);
        }

    }

    private void OnTriggerEnter2D(Collider2D childCollider)
    {
        if (childCollider.CompareTag("Player") && canDash)
        {
            canDash = false;
            Debug.Log("Setting dash now");
            StartCoroutine(waitForDash());
        }
    }

    private IEnumerator waitForDash()
    {
        dashPos = player.transform.position;
        isDashing = true;
        //Vector3 dashPos = player.transform.position;
        yield return new WaitForSeconds(2f);
        StartCoroutine(dashCooldown());

    }

    private IEnumerator dashCooldown()
    {
        yield return new WaitForSeconds(1f);
        isDashing = false;
        canDash = true;
    }


}
