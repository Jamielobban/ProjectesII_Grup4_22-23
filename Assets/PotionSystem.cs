using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSystem : MonoBehaviour
{
    public GameObject potionPrefab;

    public PotionUI potion;

    public int amountToFill;

    // Start is called before the first frame update
    void Start()
    {

        amountToFill = PlayerPrefs.GetInt("Potions", 0);
        if(GameObject.FindGameObjectWithTag("Player") != null)
        CheckPotionStatus();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FlashPotion()
    {
        potion.SetPotionImage(PotionStatus.Flash);
        //Debug.Log("asdaksd");
    }

    public void CheckPotionStatus()
    {
        //Debug.Log("Checking");
        if (amountToFill >= 75)
        {

            potion.SetPotionImage(PotionStatus.Full);
        }
        else if (amountToFill >= 50)
        {
            potion.SetPotionImage(PotionStatus.ThreeQuarter);
        }
        else if (amountToFill >= 25)
        {
            potion.SetPotionImage(PotionStatus.Half);
        }
        else if (amountToFill >= 1)
        {
            potion.SetPotionImage(PotionStatus.Quarter);
        }
    }
}
