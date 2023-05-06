using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PotionUI : MonoBehaviour
{
    public RectTransform myUIObjectTransform;
    public Sprite zero, ten, twenty, thirty, forty, fifty, sixty, seventy, eighty, ninety, hundred, flash;
    Image potionImage;
    public PotionStatus _status;
    public PotionStatus _emptyStatus;
    bool moving = true;

    [SerializeField] PlayerMovement playerPotion;
    //public bool wasHalf;
    //public bool isFullHeart;
    //public bool isEmpty;
    //public bool itsThisOne;

    private void Start()
    {
        if (moving)
        {
            myUIObjectTransform.DOScale(new Vector3(1.05f, 1.05f, 1f), 0.3f).SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void Awake()
    {
        _emptyStatus = PotionStatus.Zero;
        potionImage = GetComponent<Image>();
        playerPotion = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        Debug.Log(_status);
        Debug.Log(moving);
        if (_status >= PotionStatus.Fifty)
        {
            if (!moving)
            {
                moving = true;
                myUIObjectTransform.DOScale(new Vector3(1.05f, 1.05f, 1f), 0.3f).SetLoops(-1, LoopType.Yoyo);
            }
        }
        else
        {
            if (moving)
            {
                moving = false;
                myUIObjectTransform.DOKill();
                myUIObjectTransform.localScale = Vector3.one;
            }
        }
    }
    public void SetPotionImage(PotionStatus status)
    {
        switch (status)
        {
            case PotionStatus.Zero:
                potionImage.sprite = zero;
                //isEmpty = true;
                _status = status;
                break;
            case PotionStatus.Ten:
                potionImage.sprite = ten;
                //wasHalf = true;
                _status = status;
                break;
            case PotionStatus.Twenty:
                potionImage.sprite = twenty;
                // isEmpty = false;
                //wasHalf = false;

                _status = status;
                break;
            case PotionStatus.Flash:
                potionImage.sprite = flash;
                _status = status;
                break;
            case PotionStatus.Thirty:
                potionImage.sprite = thirty;
                _status = status;
                break;
            case PotionStatus.Forty:
                potionImage.sprite = forty;
                _status = status;
                break;
            case PotionStatus.Fifty:
                potionImage.sprite = fifty;
                _status = status;
                break;
            case PotionStatus.Sixty:
                potionImage.sprite = sixty;
                _status = status;
                break;
            case PotionStatus.Seventy:
                potionImage.sprite = seventy;
                _status = status;
                break;
            case PotionStatus.Eighty:
                potionImage.sprite = eighty;
                _status = status;
                break;
            case PotionStatus.Ninety:
                potionImage.sprite = ninety;
                _status = status;
                break;
            case PotionStatus.Hundred:
                potionImage.sprite = hundred;
                _status = status;
                break;
        }

    }
}

public enum PotionStatus
{
    Zero = 0,
    Ten = 1,
    Twenty = 2,
    Thirty = 3,
    Forty = 4,
    Fifty = 5,
    Sixty = 6,
    Seventy = 7,
    Eighty = 8,
    Ninety = 9,
    Hundred = 10,
    Flash = 11
}
