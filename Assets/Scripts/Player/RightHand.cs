using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RightHand : MonoBehaviour
{
    Weapon weaponInHand, nextWeapon;
    [SerializeField] Transform firePoint;
    [SerializeField]  SpriteRenderer sr;
    public PowerUpTimer powerUpTimer;

    //private delegate void OnPowerupDelegate();
    //private OnPowerupDelegate actionOnPowerup;

    public float timeToPass;
    private bool firstTime = true;

    private void Start()
    {
        nextWeapon = WeaponGenerator.Instance.SetMyInitialWeaponAndReturnMyNext(ref weaponInHand, firePoint, ref sr);
        weaponInHand.SetWeaponHand(ref sr);
    }

    private void Update()
    {
        if (!weaponInHand.GetState())
        {
            if(powerUpTimer.GetMaxTime() < 20)
            {
                powerUpTimer.SetMaxTime(20);
            }
            //Debug.Log("Normal");
            powerUpTimer.SetTime(weaponInHand.GetTime());
        }
        else
        {
            if (firstTime)
            {
                powerUpTimer.SetMaxTime(weaponInHand.SetTimeLeftPowerup());
                Debug.Log("Hola");
                firstTime = false;
            }
            powerUpTimer.SetTime(weaponInHand.GetTimeLeftPowerup());
            //Debug.Log("Activated");
        }
       
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
