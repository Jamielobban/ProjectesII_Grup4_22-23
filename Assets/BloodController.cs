using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodController : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerMovement playerpos;
    private CompositeCollider2D bloodcollider;
    int damage = 1;
    float ticRate = 1.5f;
    List<PlayerMovement> players = new List<PlayerMovement>();
    CircleTransition trans;
     
    void Start()
    {
        playerpos = GameController.instance.player;
        InvokeRepeating("DealDamage",ticRate,ticRate);
        trans = FindObjectOfType<CircleTransition>();
    }
    void DealDamage() 
    {
        foreach (PlayerMovement player in players)
        {
            Debug.Log(player.currentHearts);
            //if (player.currentHearts <= 0)
            //{
            //    trans.CloseBlackScreen();
            //}
           player.TakeDamage(damage);
           player.healthUI.DrawHearts();
        }
    }
    // Update is called once per frame
    void Update()
    {

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
}
