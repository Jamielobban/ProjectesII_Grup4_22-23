using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BloodController : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerMovement playerpos;
    private CompositeCollider2D bloodcollider;
    int damage = 1;
    float ticRate = 1.5f;
    List<PlayerMovement> players = new List<PlayerMovement>();
     
    void Start()
    {
        playerpos = GameController.instance.player;
        InvokeRepeating("DealDamage",ticRate,ticRate);
    }
    void DealDamage() 
    {
        foreach (PlayerMovement player in players)
        {
            if (player.currentHearts < 1)
            {
                player.healthUI.DrawAllEmpty();
                Debug.Log("hello");
            }
            else
            {
                player.OnHit(damage);
                player.healthUI.DrawHearts();
                StartCoroutine(player.hurtAnimation());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            playerpos.moveSpeed = 5000;
            playerpos.rollSpeed = 65f;
            playerpos.isInBlood = true;
            players.Add(player);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerMovement player = collision.GetComponent<PlayerMovement>();
        if (player != null)
        {
            playerpos.moveSpeed = 10000;
            playerpos.rollSpeed = 90f;
            playerpos.isInBlood = false;
            players.Remove(player);

        }
    }

    private IEnumerator FlashHeart()
    {
        playerpos.isInvulnerable = true;

        if (playerpos.currentHearts % 2 == 0 && playerpos.healthUI.emptyHeartArray != null)
        {
            playerpos.healthUI.emptyHeartToFlash.GetComponent<Animator>().enabled = true;
            Debug.Log("In laga");
            //Debug.Log("Flashed right heart");
        }
        else
        {
            playerpos.healthUI.heartToChange.GetComponent<Animator>().enabled = true;
            //Debug.Log("Flashed half heart");
            Debug.Log("In In lava");
        }

        //Debug.Log("Now invulnerable");
        playerpos.body.DOColor(playerpos.hurtColor, 0.0f);
        playerpos.body.DOColor(playerpos.invulnerableColor, 0.15f);

        yield return new WaitForSeconds(0.20f);

        if (playerpos.currentHearts % 2 == 0)
        {
            playerpos.healthUI.emptyHeartToFlash.GetComponent<Animator>().enabled = false;
            playerpos.healthUI.emptyHeartToFlash.SetHeartImage(playerpos.healthUI.emptyHeartToFlash._emptyStatus);
        }
        else
        {
            playerpos.healthUI.heartToChange.GetComponent<Animator>().enabled = false;
            playerpos.healthUI.heartToChange.SetHeartImage(playerpos.healthUI.heartToChange._status);
        }



        playerpos.body.DOColor(playerpos.hurtColor, 0.0f);
        playerpos.body.DOColor(playerpos.invulnerableColor, 0.15f);


        yield return new WaitForSeconds(0.20f);


        playerpos.body.DOColor(playerpos.hurtColor, 0.0f);
        playerpos.body.DOColor(playerpos.invulnerableColor, 0.15f);

        yield return new WaitForSeconds(0.20f);
        playerpos.body.DOColor(playerpos.OriginalColor, 0.0f);


        //Debug.Log("No longer invlunerable");
        playerpos.isInvulnerable = false;
    }
}

