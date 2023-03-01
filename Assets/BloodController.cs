using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodController : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerMovement playerpos;
    private CompositeCollider2D bloodcollider;
    //int damage = 5;
    float ticRate = 0.5f;
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
           //player.TakeDamage(damage);
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
