using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControllerTest : MonoBehaviour
{
    public Image[] heartContainers;
    public Image[] staminContainers;
    public Image[] explosion;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            //Heal
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            //Damage
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            //Pickup heart
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            //Pickup heart at max
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            //Backdrop
        } 
    }
}
