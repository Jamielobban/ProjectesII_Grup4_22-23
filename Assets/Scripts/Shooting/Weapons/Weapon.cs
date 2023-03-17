using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Weapon
{
    
    private AudioClip nextWeapon;


    protected AudioClip powerupEmpty;
    protected AudioClip powerupMax;
    protected AudioClip powerupPressed;

    protected WeaponValues data;

    protected Mechanism weaponMechanism;
    protected Transform firePoint;

    protected GameObject player;


    [SerializeField] WeaponGenerator weapon;

    protected int? reloadKeySound;
    protected int? powerupMaxKey;
    protected int? powerupPressKey;
    protected int? nextWeaponKey;
    
    public float timer;
    bool firstEnter = true;

    public bool shotFired = false;
    public Weapon(Transform _firePoint, WeaponValues _data)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        firePoint = _firePoint;
        data = _data;
        //data.RestartValues();
        powerupPressed = Resources.Load<AudioClip>("Sounds/Powerup/powerupPressed");
        powerupEmpty = Resources.Load<AudioClip>("Sounds/Powerup/powerup0");
        powerupMax = Resources.Load<AudioClip>("Sounds/Powerup/powerupMax");
        nextWeapon = Resources.Load<AudioClip>("Sounds/NextWeapon/nextWeapon");
        switch (_data.mechanismType)
        {
        case MechanismTypes.BOLT:
                weaponMechanism = new Repeticion();
            break;
        case MechanismTypes.AUTO:
                weaponMechanism = new Automatica();
            break;
        case MechanismTypes.SEMIAUTO:
                weaponMechanism = new Seamiautomatica();
                break;
        case MechanismTypes.FLOW:
                weaponMechanism = new Flow();
            break;
        default:
            break;
        }
    }

    public string GetWeaponName()
    {
        return data.WeaponName;
    }
    public virtual int Update()
    {
        CheckShooting();

        int returnValue = InputsUpdate();

        LogicUpdate();

        return returnValue;
    }    
    public float GetReloadTimeInSec()
    {
        return data.reloadTimeInSec.RuntimeValue;
    }
    public int GetBulletsInMagazine()
    {
        return data.bulletsInMagazine.RuntimeValue;
    }

    public Sprite GetSprite()
    {
        return data.weaponSprite;
    }

    public Color GetWeaponColor()
    {
        return data.weaponColor;
    }

    public int GetBulletsPerMagazine()
    {
        return data.bulletsInMagazine.InitialValue;
    }

    public int GetCurrentMagazines()
    {
        return data.magazinesInWeapon.RuntimeValue;
    }

    public float GetKnockbackForce()
    {
        //return data.
        return data.knockbackForce.InitialValue;
    }
    public float GetKnockbackMinimum()
    {
        //return data.
        return data.knockbackMinimum.InitialValue;
    }    

    public bool GetReloadingState()
    {
        return data.reloading.RuntimeValue;
    }

    public Sprite GetEmptySprite()
    {
        return data.emptyAmmo;
    }

    public Sprite GetFullSprite()
    {
        return data.fullAmmo;
    }
    public Sprite GetFlashSprite()
    {
        return data.flashAmmo;
    }   
    private bool CheckShooting()
    {
        
        if (!data.outOfAmmo.RuntimeValue && !data.reloading.RuntimeValue && Time.timeScale != 0)
        {          
            if (weaponMechanism.Shoot(data.bulletTypePrefab, firePoint, data.fireRateinSec.RuntimeValue, data.shootSound, data.amplitudeGain.RuntimeValue, data.damageMultiplier.RuntimeValue))
            {
                shotFired = true;
                data.bulletsInMagazine.RuntimeValue--;
                LoadOrReloadWhenNeedIt();
                return true;
            }                
                                 
            
        }
        return false;
    }        
    public void SetWeaponHand(ref SpriteRenderer _sr)
    {
        _sr.sprite = data.weaponSprite;
        _sr.color = data.weaponColor;
        firePoint.localPosition = data.firePoint;
        if(data.bulletsInMagazine.RuntimeValue == 0 && data.outOfAmmo.RuntimeValue && data.magazinesInWeapon.RuntimeValue > 0)
        {
            Debug.Log("aaaaaaaaaaaaaaaaaaaaaa");
            LoadOrReloadWhenNeedIt();
        }

    }
    //protected abstract float GenerateBaseFireRate();    

    private void LogicUpdate()
    {
        if (data.reloading.RuntimeValue && Time.time - data.startReloading.RuntimeValue >= data.reloadTimeInSec.RuntimeValue +0.5f)
        {           
            data.reloading.RuntimeValue = false;
        }

        if (Time.time - data.timelastPowerupExit.RuntimeValue >= 20 && !data.powerupAvailable.RuntimeValue)
        {
            data.powerupAvailable.RuntimeValue = true;            
        }       
    }

    public void CancelReload()
    {
        data.bulletsInMagazine.RuntimeValue = 0;
        data.magazinesInWeapon.RuntimeValue++;       
        
    }

    private int InputsUpdate()
    {          
        if (Input.GetButtonDown("Reload") && data.bulletsInMagazine.RuntimeValue < data.bulletsInMagazine.InitialValue && !data.outOfAmmo.RuntimeValue && data.magazinesInWeapon.RuntimeValue > 0)
        {
            if (data.magazinesInWeapon.RuntimeValue > 0 && data.bulletsInMagazine.RuntimeValue > 0)
            {
                data.bulletsInMagazine.RuntimeValue = data.bulletsInMagazine.InitialValue;
                data.magazinesInWeapon.RuntimeValue--;
                reloadKeySound = AudioManager.Instance.LoadSound(data.reloadSound, player.transform, 0.5f, false);
                data.reloading.RuntimeValue = true;
                data.startReloading.RuntimeValue = Time.time;
            }
           
        }
        else 
        {
            //float wheelValue = Input.mouseScrollDelta.y;
            //if(wheelValue > 0)
            //{
            //    return 1;
            //}
            //if (wheelValue < 0)
            //{
            //    return -1;
            //}
        }

        return 0;
    }     

    protected void LoadOrReloadWhenNeedIt()
    {
        Debug.Log(player.transform.GetChild(0).GetChild(0).GetComponent<RightHand>().weaponGenerator.weaponIndex);
        //cinemachineShake.Instance.ShakeCamera(5f, .1f);
        if (data.bulletsInMagazine.RuntimeValue == 0)
        {
            if(data.magazinesInWeapon.RuntimeValue == 0)
            {
                data.outOfAmmo.RuntimeValue = true;
            }
            else
            {
                Debug.Log("Reloading");
                reloadKeySound = AudioManager.Instance.LoadSound(data.reloadSound, player.transform, 0.5f, false);
                //data.bulletsInMagazine.RuntimeValue = data.bulletsInMagazine.InitialValue;
                
                player.transform.GetChild(0).GetChild(0).GetComponent<RightHand>().weaponGenerator.weaponIndex++;
                Debug.Log(player.transform.GetChild(0).GetChild(0).GetComponent<RightHand>().weaponGenerator.weaponIndex);
                player.transform.GetChild(0).GetChild(0).GetComponent<RightHand>().EquipWeapon(data.WeaponName);
                //Debug.Log(player.transform.GetChild(0).GetChild(0));
                //data.startReloading.RuntimeValue = Time.time;
                //data.reloading.RuntimeValue = true;                
            }
        }
    }

    public bool GetIfOutOffAmmo()
    {
        //if (data.outOfAmmo.RuntimeValue)
        //    nextWeaponKey = AudioManager.Instance.LoadSound(nextWeapon, player.transform);
        return data.outOfAmmo.RuntimeValue;
    }


    public float GetFireRate()
    {
        return data.fireRateinSec.InitialValue;
    }
}

