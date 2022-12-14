using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFXController : MonoBehaviour
{
    public PlayerMovement player;

    private SpriteRenderer effectsRunning;
    // Update is called once per frame
    private void Start()
    {
        effectsRunning = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (player.isMoving)
        {
            //effectsRunning.enabled = true;
            if (player.angle > -90 && player.angle < 90 && player.movement.x >= 1)
            {
                //LOOKING TO THE LEFT
                effectsRunning.enabled = true;
                effectsRunning.flipX = false;
                Debug.Log("running");
                this.transform.localPosition = new Vector3(1.5f, 0, 0);
            }
            else if ((player.angle > 90 || player.angle < -90) && player.movement.x <= -1)
            {
                // LOOKING TO THE RIGHT
                effectsRunning.enabled = true;
                effectsRunning.flipX = true;
                Debug.Log("Running2toda");
                this.transform.localPosition = new Vector3(-1.475f, 0, 0);
            }
            else if (player.angle > -90 && player.angle < 90 && player.movement.x <= -1)
            {
                //LOOKING LEFT AND MOVING LEFT
                effectsRunning.enabled = true;
                effectsRunning.flipX = true;
                this.transform.localPosition = new Vector3(-1.475f, 0, 0);
                Debug.Log("Looking left and running left");
            }
            else if ((player.angle > 90 || player.angle < -90) && player.movement.x >= 1)
            {
                // LOOKING TO THE RIGHT AND MOVING RIGHT
                effectsRunning.enabled = true;
                effectsRunning.flipX = false;
                Debug.Log("Running2toda");
                this.transform.localPosition = new Vector3(1.5f, 0, 0);
            }

            if (player.angle > -90 && player.angle < 90 && player.movement.x <= -1 && player.isDashing)
            {
                effectsRunning.enabled = true;
                //effectsRunning.flipX = true;
                Debug.Log("Dashed to the left");
                this.transform.localPosition = new Vector3(1.7f, 0, 0);
            }
            else if ((player.angle > 90 || player.angle < -90) && player.movement.x >= 1 && player.isDashing)
            {
                effectsRunning.enabled = true;
                effectsRunning.flipX = false;
                this.transform.localPosition = new Vector3(-1.08f, 0, 0);
            }

            //if (player.isDashing && )
            //{

            //}
            //if(player.playerSprite.flipX == true)
            //{
            //    effectsRunning.flipX = true;
            //    this.transform.localPosition = new Vector3(-1.6f, 0f, 0f);
            //}
            //else
            //{

            //    effectsRunning.flipX = false;
            //    this.transform.localPosition = new Vector3(1.665f, 0, 0);
            //}
            //}

            //    if (angle > -90 && angle < 90)
            //    {
            //        playerSprite.flipX = true;
            //    }
            //    else
            //    {
            //        playerSprite.flipX = false;
            //    }

            //    if (angle > 0 && angle < 180)
            //    {
            //        playerSprite.sortingOrder = 12;
            //    }
            //    else
            //    {
            //        playerSprite.sortingOrder = 14;
            //    }

        }
        else
        {
            effectsRunning.enabled = false;
        }
    }

}