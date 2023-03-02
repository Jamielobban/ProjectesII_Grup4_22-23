using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHeart : MonoBehaviour
{
    public Sprite fullHeart, halfHeart, emptyHeart, flashHeart;
    Image heartImage;
    public HeartStatus _status;

    [SerializeField] PlayerMovement playerHeart;
    //public bool wasHalf;
    //public bool isFullHeart;
    //public bool isEmpty;
    //public bool itsThisOne;
    private void Awake()
    {
        heartImage = GetComponent<Image>();
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
    public void SetHeartImage(HeartStatus status)
    {
        switch (status)
        {
            case HeartStatus.Empty:
                heartImage.sprite = emptyHeart;
                //isEmpty = true;
                _status = status;
                break;
            case HeartStatus.Half:
                heartImage.sprite = halfHeart;
                //wasHalf = true;
                _status = status;
                break;
            case HeartStatus.Full:
                heartImage.sprite = fullHeart;
               // isEmpty = false;
                //wasHalf = false;
                
                _status = status;
                break;
        }

    }
}

public enum HeartStatus
{
    Empty = 0,
    Half = 1,
    Full = 2,
}