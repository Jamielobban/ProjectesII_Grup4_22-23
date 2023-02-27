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

    public PowerUpTimer powerUpTimer;
    public PowerUpTimer reloadBarTimer;

    private RecoilScript _recoilSript;
    public GameObject powerUpBar;
    public GameObject reloadBar;
    public TextMeshProUGUI bulletsInMagazine;
    public TextMeshProUGUI bulletsPerMagazine;
    public TextMeshProUGUI magazineNumber;
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
    

    private void Start()
    {
        _recoilSript = GetComponent<RecoilScript>();
        nextWeapon = WeaponGenerator.Instance.SetMyInitialWeaponAndReturnMyNext(ref weaponInHand, firePoint);
        weaponInHand.SetWeaponHand(ref sr);
        reloadBar.SetActive(false);
        //powerUpBarColor = powerUpBar.GetComponent<Image>();

        UpdateUIWeapons();
        powerUpState = PowerUpState.RELOADING;
        //playerMat = GetComponentInParent<PlayerMovement>().body.material;
        //shakeSeq = DOTween.Sequence();
    }

    void UpdateUIWeapons()
    {
        actualWeaponUI.sprite = weaponInHand.GetSprite();
        nextWeaponUI.sprite = nextWeapon.GetSprite();

        actualWeaponUI.color = weaponInHand.GetWeaponColor();
        nextWeaponUI.color = nextWeapon.GetWeaponColor();
    }
    private void Update()
    {
        //if (playerMat.GetFloat("_ShakeUvSpeed") != 0 && timeEndShake <= Time.time)
        //{
        //    playerMat.SetFloat("_ShakeUvSpeed", 0);
        //}

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
            reloadBar.SetActive(true);
            if (firstTime3)
            {
                reloadBarTimer.SetMaxTime(weaponInHand.GetReloadTimeInSec() + 0.5f);
                firstTime = false;
            }
            reloadTimer += Time.deltaTime;
            reloadBarTimer.SetTime(reloadTimer);
            if (reloadTimer > weaponInHand.GetReloadTimeInSec() + 0.5f)
            {
                firstTime3 = true;
                reloadBar.SetActive(false);
                reloadTimer = 0f;
            }
        }

        //Weapon powerup UI
        //if (!weaponInHand.GetState())
        //{

        //    //if (powerUpTimer.GetMaxTime() <= 20)
        //    //{
        //    //    powerUpTimer.SetMaxTime(20);
        //    //    firstTime = true;

        //    //}
        //    //if (powerUpBarColor.fillAmount == 1)
        //    //{
        //    //    powerUpState = PowerUpState.FULL;

        //    //}
        //    //else
        //    //{
        //    //    powerUpState = PowerUpState.RELOADING;

        //    //}

        //    //powerUpTimer.SetTime(weaponInHand.GetTime());
        //}
        //if (weaponInHand.GetState())
        //{
        //    powerUpState = PowerUpState.USING;

        //    if (firstTime)
        //    {
        //        powerUpTimer.SetMaxTime(weaponInHand.SetTimeLeftPowerup());
        //        firstTime = false;
        //    }

        //    powerUpTimer.SetTime(weaponInHand.GetTimeLeftPowerup());

        //    //powerUpBar.color = new Color(202,187,43,255);

        //    //CBBC2B
        //    //394AA6

        //    //Debug.Log("Activated");
        //}

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
            reloadBar.SetActive(true);
            if (firstTime3)
            {
                reloadBarTimer.SetMaxTime(weaponInHand.GetReloadTimeInSec() + 0.5f);
                firstTime = false;
            }
            reloadTimer += Time.deltaTime;
            reloadBarTimer.SetTime(reloadTimer);
            if (reloadTimer > weaponInHand.GetReloadTimeInSec() + 0.5f)
            {
                firstTime3 = true;
                reloadBar.SetActive(false);
                reloadTimer = 0f;
            }
        }

        //Weapon powerup UI
        //if (!weaponInHand.GetState())
        //{

        //    //if (powerUpTimer.GetMaxTime() <= 20)
        //    //{
        //    //    powerUpTimer.SetMaxTime(20);
        //    //    firstTime = true;

        //    //}
        //    //if (powerUpBarColor.fillAmount == 1)
        //    //{
        //    //    powerUpState = PowerUpState.FULL;

        //    //}
        //    //else
        //    //{
        //    //    powerUpState = PowerUpState.RELOADING;

        //    //}

        //    //powerUpTimer.SetTime(weaponInHand.GetTime());
        //}
        //if (weaponInHand.GetState())
        //{
        //    powerUpState = PowerUpState.USING;

        //    if (firstTime)
        //    {
        //        powerUpTimer.SetMaxTime(weaponInHand.SetTimeLeftPowerup());
        //        firstTime = false;
        //    }

        //    powerUpTimer.SetTime(weaponInHand.GetTimeLeftPowerup());

        //    //powerUpBar.color = new Color(202,187,43,255);

        //    //CBBC2B
        //    //394AA6

        //    //Debug.Log("Activated");
        //}

        //Weapon ammo UI
        bulletsInMagazine.text = weaponInHand.GetBulletsInMagazine().ToString();
        bulletsPerMagazine.text = weaponInHand.GetBulletsPerMagazine().ToString();
        magazineNumber.text = weaponInHand.GetCurrentMagazines().ToString();

        // Debug.Log(weaponInHand.GetBulletsInMagazine());


        weaponInHand.Update();

        if (weaponInHand.GetIfOutOffAmmo())
        {
            //Debug.Log("in");
            if (reloadBar.activeSelf)
            {
                firstTime3 = true;
                reloadBar.SetActive(false);
                reloadTimer = 0f;
            }
            timeToPass = weaponInHand.GetTime();
            weaponInHand = nextWeapon;
            weaponInHand.SetWeaponHand(ref sr);
            weaponInHand.SetTime(timeToPass);
            nextWeapon = WeaponGenerator.Instance.ReturnMyNextWeapon(firePoint);
            firstTime = true;
            UpdateUIWeapons();

        }

        if (weaponInHand.FixedUpdate())
        {
            ShootShake();
        }
    }

    private void FixedUpdate()
    {
        
    }

    void ShootShake()
    {
        //if(playerMat.GetFloat("_ShakeUvSpeed") == 0)
        //{
        //    playerMat.SetFloat("_ShakeUvSpeed", 20);
        //}
        //timeEndShake = Time.time + 0.13f;
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
