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
        if (amountToFill > 100)
        {
            amountToFill = 100;
        }
    }

    public void FlashPotion()
    {
        potion.SetPotionImage(PotionStatus.Flash);
        //Debug.Log("asdaksd");
    }

    public void CheckPotionStatus()
    {
        //Debug.Log("Checking");
        //if(amountToFill > 100)
        //{
        //    amountToFill = 100;
        //}

            //if (amountToFill >= 99)
            //{

            //    potion.SetPotionImage(PotionStatus.Full);
            //}
        if (amountToFill >= 100)
        {
            potion.SetPotionImage(PotionStatus.Hundred);
        }
        else if (amountToFill >= 90)
        {
            potion.SetPotionImage(PotionStatus.Ninety);
        }
        else if (amountToFill >= 80)
        {
            potion.SetPotionImage(PotionStatus.Eighty);
        }
        else if (amountToFill >= 70)
        {
            potion.SetPotionImage(PotionStatus.Seventy);
        }
        else if (amountToFill >= 60)
        {
            potion.SetPotionImage(PotionStatus.Sixty);
        }
        else if (amountToFill >= 50)
        {
            potion.SetPotionImage(PotionStatus.Fifty);
        }
        else if (amountToFill >= 40)
        {
            potion.SetPotionImage(PotionStatus.Forty);
        }
        else if (amountToFill >= 30)
        {
            potion.SetPotionImage(PotionStatus.Thirty);
        }
        else if (amountToFill >= 20)
        {
            potion.SetPotionImage(PotionStatus.Twenty);
        }
        else if (amountToFill >= 10)
        {
            potion.SetPotionImage(PotionStatus.Ten);
        }
        else if(amountToFill >= 0)
        {
            potion.SetPotionImage(PotionStatus.Zero);
        }
    }
}
