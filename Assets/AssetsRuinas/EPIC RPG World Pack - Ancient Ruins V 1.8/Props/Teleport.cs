using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    public Animator teleport;
    public SpriteRenderer player;
    public PlayerMovement playerMov;
    public GameObject square;

    public bool enter;
    public Animator transitionAnim;
    public int llave;
    // Start is called before the first frame update
    void Start()
    {
       if(enter)
        {
            if(PlayerPrefs.GetInt("Teleport", 0) == 1)
            {
                if(PlayerPrefs.GetInt("LLave1", 0) == 1 || PlayerPrefs.GetInt("LLave2", 0) == 1)
                {
                    square.SetActive(true);

                    PlayerPrefs.SetInt("Teleport", 0);
                    transitionAnim.SetTrigger("enter");

                    Invoke("enterTransition", 7f);
                    player.enabled = false;
                    playerMov.disableDash = true;
                    playerMov.GetComponentInChildren<RightHand>().weaponEquiped = false;
                    playerMov.canMove = false;
                }
                else
                {
                    square.SetActive(true);

                    PlayerPrefs.SetInt("Teleport", 0);
                    enterRoom();                
                    player.enabled = false;
                    playerMov.disableDash = true;
                    playerMov.GetComponentInChildren<RightHand>().weaponEquiped = false;
                    playerMov.canMove = false;
                }
      



            }
            else
            {
                square.SetActive(false);
            }
        }
        else
        {
            square = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void enterRoom()
    {
        transitionAnim.SetTrigger("enter");
        Invoke("enterTransition", 2f);

    }
    void exitRoom()
    {
        transitionAnim.SetTrigger("exit");
        Invoke("scene", 1f);

    }

    void disablePlayer()
    {
        player.enabled = false;
    }
    void enablePlayer()
    {
        player.enabled = true;
        playerMov.disableDash = false;
        playerMov.GetComponentInChildren<RightHand>().weaponEquiped = true;
        playerMov.canMove = true;
    }
    
    void enterTransition()
    {
        teleport.SetTrigger("Teleport");
        Invoke("enablePlayer", 0.25f);

    }
    void scene()
    {
        SceneManager.LoadScene("Bosque");

    }
    public void teleportPlayer()
    {
        PlayerPrefs.SetInt("Teleport", 1);
        teleport.SetTrigger("Teleport");
        Invoke("disablePlayer", 0.25f);
        Invoke("exitRoom",1f);
        playerMov.disableDash = true;
        playerMov.GetComponentInChildren<RightHand>().weaponEquiped = false;
        playerMov.canMove = false;
        if(PlayerPrefs.GetInt("Final"+llave, 0) == 0)
            PlayerPrefs.SetInt("LLave"+llave, 1);

    }
}
