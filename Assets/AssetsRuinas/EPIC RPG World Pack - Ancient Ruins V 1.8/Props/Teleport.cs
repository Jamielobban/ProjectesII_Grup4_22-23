using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public Animator teleport;
    public SpriteRenderer player;
    public PlayerMovement playerMov;

    public bool enter;
    public Animator transitionAnim;
    // Start is called before the first frame update
    void Start()
    {
       if(enter)
        {
            if(PlayerPrefs.GetInt("Teleport", 0) == 1)
            {
                PlayerPrefs.SetInt("Teleport", 0);
                enterRoom();                
                player.enabled = false;
                playerMov.disableDash = true;
                playerMov.GetComponentInChildren<RightHand>().weaponEquiped = false;
                playerMov.canMove = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void enterRoom()
    {
        transitionAnim.SetTrigger("enter");
        Invoke("enterTransition", 0.5f);

    }
    void exitRoom()
    {
        transitionAnim.SetTrigger("exit");
        Invoke("scene", 0.5f);

    }

    void disablePlayer()
    {
        player.enabled = false;
    }
    void enablePlayer()
    {
        player.enabled = true;
        playerMov.disableDash = true;
        playerMov.GetComponentInChildren<RightHand>().weaponEquiped = false;
        playerMov.canMove = false;
    }
    
    void enterTransition()
    {
        teleport.SetTrigger("Teleport");
        Invoke("enablePlayer", 0.1f);

    }
    void scene()
    {
        SceneManager.LoadScene("Bosque");

    }
    public void teleportPlayer()
    {
        PlayerPrefs.SetInt("Teleport", 1);
        teleport.SetTrigger("Teleport");
        Invoke("disablePlayer", 0.1f);
        Invoke("exitRoom",1f);
        playerMov.disableDash = true;
        playerMov.GetComponentInChildren<RightHand>().weaponEquiped = false;
        playerMov.canMove = false;
    }
}
