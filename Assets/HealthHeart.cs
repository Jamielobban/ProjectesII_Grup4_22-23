using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHeart : MonoBehaviour
{
    public Sprite fullHeart, halfHeart, emptyHeart;
    Image heartImage;
    public HeartStatus _status;

    private void Awake()
    {
        heartImage = GetComponent<Image>();
    }

    public void SetHeartImage(HeartStatus status)
    {
        switch (status)
        {
            case HeartStatus.Empty:
                heartImage.sprite = emptyHeart;
                _status = status;
                break;
            case HeartStatus.Half:
                heartImage.sprite = halfHeart;
                _status = status;
                break;
            case HeartStatus.Full:
                heartImage.sprite = fullHeart;
                _status = status;
                break;
        }

    }


}

public enum HeartStatus
{
    Empty = 0,
    Half = 1,
    Full = 2
}