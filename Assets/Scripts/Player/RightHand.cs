using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using DG.Tweening;

public class RightHand : MonoBehaviour
{
    public Weapon weaponInHand;
    [SerializeField] Transform firePoint;
    [SerializeField] SpriteRenderer sr;

    
    [SerializeField] AmmoUISystem ammoUI;
    private RecoilScript _recoilSript;
   
    private float reloadTimer = 0f;
    private float startedReload;

    bool canChangeWeapon;
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

    int index;
    float timeEndShake;
    public bool weaponEquiped = false;
    enum PowerUpState { RELOADING, USING, FULL }; 
    public Image actualWeaponUI, nextWeaponUI;

    public bool stuffSetted = false;
    [SerializeField] WeaponGenerator weapon;
    private void Awake()
    {
        weapon = GameObject.FindGameObjectWithTag("WeaponGenerator").GetComponent<WeaponGenerator>();
        ammoUI = FindObjectOfType<AmmoUISystem>();
        canChangeWeapon = true;
    }

    public void UpdateUIWeapons()
    {
        ammoUI.DrawAmmo();        
    }
    private void Update()
    {
        //Reload Bar        

        if (weaponEquiped)
        {
            if(weapon.weaponIndexOrder.Count != 0)
            {

                if (weaponInHand.GetReloadingState())
                {
                    if (firstTime3)
                    {
                        ammoUI.StartCoroutine(ammoUI.ReloadAmmo(weaponInHand.GetReloadTimeInSec()));
                        canChangeWeapon = false;
                        firstTime3 = false;
                    }
                    reloadTimer += Time.deltaTime;
                    if (reloadTimer > weaponInHand.GetReloadTimeInSec() + 0.5f)
                    {
                        firstTime3 = true;
                        canChangeWeapon = true;
                        reloadTimer = 0f;
                    }
                }

                //Debug.Log(weaponInHand.Update());
                if (canChangeWeapon)
                {
                    if (WeaponGenerator.Instance.SetWeapon(weaponInHand.Update(), ref weaponInHand, ref firePoint))
                    {
                        weaponInHand.SetWeaponHand(ref sr);
                        UpdateUIWeapons();
                    }
                }
                

                if (!stuffSetted && weaponEquiped)
                {
                    _recoilSript = GetComponent<RecoilScript>();
                    weaponInHand.SetWeaponHand(ref sr);
                    UpdateUIWeapons();
                    stuffSetted = true;
                }

                if (weaponInHand.GetIfOutOffAmmo())
                {

                
                    weaponInHand.SetWeaponHand(ref sr);

                    //nextWeapon = WeaponGenerator.Instance.ReturnMyNextWeapon(firePoint);---------------------------
                    //Debug.Log("Update in update right hand");
                    UpdateUIWeapons();

                }
            }
        }

        
        
    }
    

    private void FixedUpdate()
    {
        if (weaponEquiped)
        {
            if (weapon.weaponIndexOrder.Count != 0)
            {
                if (weaponInHand.shotFired)
                {
                    UpdateUIWeapons();
                    //Debug.Log("Update in fixed");
                    weaponInHand.shotFired = false;
                }
            }
        }        
    }    
    
    public Color GetColor()
    {
        return weaponInHand.GetWeaponColor();
    }

    public Weapon GetWeaponInHand()
    {
        return weaponInHand;
    }

    public void EquipWeapon(string weaponName)
    {        
        WeaponGenerator.Instance.EquipWeapon(weaponName, ref weaponInHand, ref firePoint);
        UpdateUIWeapons();
        weaponInHand.SetWeaponHand(ref sr);
        weaponEquiped = true;
    }

    public void EquipWeaponModoInfinito(string weaponName)
    {
        WeaponGenerator.Instance.EquipWeapon(weaponName, ref weaponInHand, ref firePoint);
        UpdateUIWeapons();
        weaponInHand.SetWeaponHand(ref sr);
        weaponEquiped = true;
        WeaponGenerator.Instance.setAmmo(weaponName);
    }
}
