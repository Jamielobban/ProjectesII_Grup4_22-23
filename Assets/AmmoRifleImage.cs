using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoRifleImage : MonoBehaviour
{
    public Sprite fullAmmo, emptyAmmo, flashAmmo;
    [SerializeField] Image AmmoSprite;

    public AmmoStatus _status;
    // Start is called before the first frame update
    void Start()
    {
        AmmoSprite = GetComponent<Image>();
    }

    public void SetAmmoImage(AmmoStatus status)
    {
        switch (status)
        {
            case AmmoStatus.Empty:
                AmmoSprite.sprite = emptyAmmo;
                _status = status;
                break;
            case AmmoStatus.Full:
                AmmoSprite.sprite = fullAmmo;
                _status = status;
                break;
            case AmmoStatus.Flash:
                AmmoSprite.sprite = flashAmmo;
                _status = status;
                break;
        }

    }

    public enum AmmoStatus
    {
        Empty = 0,
        Full = 1,
        Flash = 2
    }

}
