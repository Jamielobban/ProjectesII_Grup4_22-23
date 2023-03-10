using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using DG.Tweening;

public class RightHand : MonoBehaviour
{
    public Weapon weaponInHand, nextWeapon;
    [SerializeField] Transform firePoint;
    [SerializeField] SpriteRenderer sr;

    //public PowerUpTimer powerUpTimer;
    //public PowerUpTimer reloadBarTimer;
    [SerializeField] AmmoUISystem ammoUI;
    private RecoilScript _recoilSript;
    //public GameObject powerUpBar;
    //public GameObject reloadBar;
    //public TextMeshProUGUI bulletsInMagazine;
    //public TextMeshProUGUI bulletsPerMagazine;
    //public TextMeshProUGUI magazineNumber;
    private float reloadTimer = 0f;
    private float startedReload;
    //private delegate void OnPowerupDelegate();
    //private OnPowerupDelegate actionOnPowerup;

    public float timeToPass;
    private bool firstTime = true;
    private bool firstTime1 = true;
    public bool firstTime3 = true;
    public bool firstTime4 = true;

    public bool isCharing;
    public bool hasCharged;
    public bool isGoingDown;

    public Color reloadingColor;
    public Color fullChargeColor;
    public Color usePowerUpColor;

    Image powerUpBarColor;
    //Material playerMat;    
    float timeEndShake;

    enum PowerUpState { RELOADING, USING, FULL };
    PowerUpState powerUpState;

    public Image actualWeaponUI, nextWeaponUI;
    

    private void Awake()
    {
        _recoilSript = GetComponent<RecoilScript>();
        nextWeapon = WeaponGenerator.Instance.SetMyInitialWeaponAndReturnMyNext(ref weaponInHand, firePoint);
        weaponInHand.SetWeaponHand(ref sr);
        //reloadBar.SetActive(false);
        //powerUpBarColor = powerUpBar.GetComponent<Image>();

        UpdateUIWeapons();
        powerUpState = PowerUpState.RELOADING;
        //playerMat = GetComponentInParent<PlayerMovement>().body.material;
        //shakeSeq = DOTween.Sequence();
    }

    void UpdateUIWeapons()
    {
        ammoUI.DrawAmmo();
        //actualWeaponUI.sprite = weaponInHand.GetSprite();
        //nextWeaponUI.sprite = nextWeapon.GetSprite();

        //actualWeaponUI.color = weaponInHand.GetWeaponColor();
        //nextWeaponUI.color = nextWeapon.GetWeaponColor();
    }
    private void Update()
    {
        //Debug.Log(playerMat);
        switch (powerUpState)
        {
            case PowerUpState.RELOADING:
                //powerUpBarColor.color = reloadingColor;
                firstTime4 = true;
                break;
            case PowerUpState.FULL:
                powerUpBarColor.color = fullChargeColor;

                break;
            case PowerUpState.USING:
                powerUpBarColor.color = usePowerUpColor;
                if (firstTime4)
                {
                    //Debug.Log(weaponInHand.GetTimeLeftPowerup());
                    CinemachineShake.Instance.ShakeCamera(5f, weaponInHand.GetTimeLeftPowerup());
                    firstTime4 = false;
                }
                break;
            default:
                break;
        }
        //Reload Bar
        if (weaponInHand.GetReloadingState())
        {
            //Debug.Log("1");
            //reloadBar.SetActive(true);
            if (firstTime3)
            {
                //ammoUI.CreateEmptyAmmo();
                //StartCoroutine(UIUpdate(weaponInHand.GetReloadTimeInSec()));
                //EmptyAmmo();
                ammoUI.StartCoroutine(ammoUI.ReloadAmmo(weaponInHand.GetReloadTimeInSec()));
                //.Log(firstTime3);
                firstTime3 = false;
            }
            reloadTimer += Time.deltaTime;
            //reloadBarTimer.SetTime(reloadTimer);
            if (reloadTimer > weaponInHand.GetReloadTimeInSec() + 0.5f)
            {
                //foreach (AmmoRifleImage image in ammoUI.rifleAmmoArray)
                //{
                //    image.GetComponent<AmmoRifleImage>().SetAmmoImage(AmmoRifleImage.AmmoStatus.Full);

                //}
                //Debug.Log(weaponInHand.GetReloadingState());
                firstTime3 = true;
                //reloadBar.SetActive(false);
                reloadTimer = 0f;
            }
        }
        else
        {
            //ammoUI.DrawAmmo();
            //Debug.Log("Update in update else");
        }

        weaponInHand.Update();

        if (weaponInHand.GetIfOutOffAmmo())
        {
            //Debug.Log("in");
            //if (reloadBar.activeSelf)
            //{
            //    firstTime3 = true;
            //    reloadBar.SetActive(false);
            //    reloadTimer = 0f;
            //}
            timeToPass = weaponInHand.GetTime();
            weaponInHand = nextWeapon;
            weaponInHand.SetWeaponHand(ref sr);
            weaponInHand.SetTime(timeToPass);
            nextWeapon = WeaponGenerator.Instance.ReturnMyNextWeapon(firePoint);
            Debug.Log("Update in update right hand");
            UpdateUIWeapons();

        }

        if (weaponInHand.FixedUpdate())
        {
            //  ShootShake();
        }
    }

    private void FixedUpdate()
    {

        if (weaponInHand.shotFired && !weaponInHand.GetReloadingState())
        {
            UpdateUIWeapons();
            //Debug.Log("Update in fixed");
            weaponInHand.shotFired = false;
        }
    }

    void ShootShake()
    {
        //if(playerMat.GetFloat("_ShakeUvSpeed") == 0)
        //{
        //    playerMat.SetFloat("_ShakeUvSpeed", 20);
        //}
        //timeEndShake = Time.time + 0.13f;
    }

 
    public Color GetColor()
    {
        return weaponInHand.GetWeaponColor();
    }

    public Weapon GetWeaponInHand()
    {
        return weaponInHand;
    }

}
