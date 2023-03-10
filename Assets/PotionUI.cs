using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionUI : MonoBehaviour
{
    public Sprite fullPotion, quarterPotion, halfPotion, threeQuarterPotion, emptyPotion, flashPotion;
    Image potionImage;
    public PotionStatus _status;
    public PotionStatus _emptyStatus;

    [SerializeField] PlayerMovement playerPotion;
    //public bool wasHalf;
    //public bool isFullHeart;
    //public bool isEmpty;
    //public bool itsThisOne;
    private void Awake()
    {
        _emptyStatus = PotionStatus.Empty;
        potionImage = GetComponent<Image>();
        playerPotion = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        //if(isEmpty && wasHalf)
        //{
        //    Debug.Log("Play Animation");
        //    //itsThisOne = true; 
        //    //wasHalf = false;
        //    //
        //    //GetComponent<Animator>().enabled = true;
        //}
    }
    public void SetPotionImage(PotionStatus status)
    {
        switch (status)
        {
            case PotionStatus.Empty:
                potionImage.sprite = emptyPotion;
                //isEmpty = true;
                _status = status;
                break;
            case PotionStatus.Half:
                potionImage.sprite = halfPotion;
                //wasHalf = true;
                _status = status;
                break;
            case PotionStatus.Full:
                potionImage.sprite = fullPotion;
                // isEmpty = false;
                //wasHalf = false;

                _status = status;
                break;
            case PotionStatus.Flash:
                potionImage.sprite = flashPotion;
                _status = status;
                break;
            case PotionStatus.Quarter:
                potionImage.sprite = quarterPotion;
                _status = status;
                break;
            case PotionStatus.ThreeQuarter:
                potionImage.sprite = threeQuarterPotion;
                _status = status;
                break;
        }

    }
}

public enum PotionStatus
{
    Empty = 0,
    Quarter = 1,
    Half = 2,
    ThreeQuarter = 3,
    Full = 4,
    Flash = 5
}