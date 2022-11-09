using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RightHand : MonoBehaviour
{
    Weapon weaponInHand, nextWeapon;
    [SerializeField] Transform firePoint;
    [SerializeField]  SpriteRenderer sr;

    public PowerUpTimer powerUpTimer;
    public PowerUpTimer reloadBarTimer;
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
    private bool firstTime3 = true;


    private void Start()
    {
        nextWeapon = WeaponGenerator.Instance.SetMyInitialWeaponAndReturnMyNext(ref weaponInHand, firePoint, ref sr);
        weaponInHand.SetWeaponHand(ref sr);
    }

    private void Update()
    {
        if (weaponInHand.GetReloadingState())
        {
            if (firstTime3)
            {
                reloadBarTimer.SetMaxTime(weaponInHand.GetReloadTimeInSec() + 0.5f);
            }
                reloadTimer += Time.deltaTime;
                reloadBarTimer.SetTime(reloadTimer);
            if (reloadTimer > weaponInHand.GetReloadTimeInSec() + 0.5f)
            {
                reloadTimer = 0f;
                firstTime3 = false;
            }
        }

        //Weapon powerup UI
        if (!weaponInHand.GetState())
        {
            if(powerUpTimer.GetMaxTime() < 20)
            {
                powerUpTimer.SetMaxTime(20);
                firstTime = true;

            }
            //Debug.Log("Normal");
            powerUpTimer.SetTime(weaponInHand.GetTime());
        }
        else
        {
            if (firstTime)
            {
                powerUpTimer.SetMaxTime(weaponInHand.SetTimeLeftPowerup());
                firstTime = false;
            }
            powerUpTimer.SetTime(weaponInHand.GetTimeLeftPowerup());
            //Debug.Log("Activated");
        }

        //Weapon ammo UI
        bulletsInMagazine.text = weaponInHand.GetBulletsInMagazine().ToString();
        bulletsPerMagazine.text = weaponInHand.GetBulletsPerMagazine().ToString();
        magazineNumber.text = weaponInHand.GetCurrentMagazines().ToString();

       // Debug.Log(weaponInHand.GetBulletsInMagazine());

       
        weaponInHand.Update();

        if (weaponInHand.GetIfOutOffAmmo())
        {
            timeToPass = weaponInHand.GetTime();
            weaponInHand = nextWeapon;
            weaponInHand.SetWeaponHand(ref sr);
            weaponInHand.SetTime(timeToPass);
            nextWeapon = WeaponGenerator.Instance.ReturnMyNextWeapon(firePoint, ref sr);
            firstTime = true;
        }
    }



}
