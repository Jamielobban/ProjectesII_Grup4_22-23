using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{

    //Mechanism[] mechanismTypes = new Mechanism[3] { new Automatica(), new Seamiautomatica(), new Repeticion() };
    //[SerializeField] GameObject[] bulletPrefabs = new GameObject[3];

    int weaponInHandInt, nextWeaponInt;

    private int arrayPositionInHand;
    private int arrayPositionOfNext;

    public static WeaponGenerator Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }    
    private void Update()
    {

    }

    public Weapon SetMyInitialWeaponAndReturnMyNext(ref Weapon weaponInHand, Transform firePoint, ref SpriteRenderer _sr)
    {
        //weaponsTypes = new Weapon[3] { new Sniper(firePoint), new Shotgun(firePoint), new Gun(firePoint) };

        int random = Random.Range(0, 3);
        //int random = 2;
        weaponInHandInt = random;
        int random2 = Random.Range(0, 3);

        if (random == 0)
        {
            weaponInHand = ReturnSniperType(random2, firePoint);
        }
        else if(random == 1)
        {
            weaponInHand = ReturnShotgunType(random2, firePoint);
        }
        else if (random == 2)
        {
            weaponInHand = ReturnGunType(random2, firePoint);
        }

        //random = 2;
        do { random = Random.Range(0, 3); } while (weaponInHandInt == random);       
        nextWeaponInt = random;
        random2 = Random.Range(0, 3);


        if (random == 0)
        {
            return ReturnSniperType(random2, firePoint);            
        }
        else if (random == 1)
        {
            return ReturnShotgunType(random2, firePoint);
        }
        else if (random == 2)
        {
            return ReturnGunType(random2, firePoint);
        }

        throw new System.NotImplementedException();
        //random = Random.Range(0, 0);
        //int random2 = Random.Range(0, 3);

        //if (random == 0)
        //{
        //    return ReturnSniperType(random2, firePoint, ref _sr);
        //}

        //return ReturnSniperType(random2, firePoint, ref _sr);

    }

    public Weapon ReturnMyNextWeapon(Transform firePoint, ref SpriteRenderer _sr)
    {
        weaponInHandInt = nextWeaponInt;
        int random = Random.Range(0, 3);
        do { random = Random.Range(0, 3); } while (random == weaponInHandInt);
        nextWeaponInt = random;
        int random2 = Random.Range(0, 3);

        if (random == 0)
        {
            return ReturnSniperType(random2, firePoint);
        }
        else if (random == 1)
        {
            return ReturnShotgunType(random2, firePoint);
        }
        else if (random == 2)
        {
            return ReturnGunType(random2, firePoint);
        }

        throw new System.NotImplementedException();
    }

    //public void ResetArrayValues(Transform firePoint)
    //{
    //    weaponsTypes = new Weapon[3] { new Sniper(firePoint), new Shotgun(firePoint), new Gun(firePoint) };
    //    mechanismTypes = new Mechanism[3] { new Automatica(), new Seamiautomatica(), new Repeticion() };
    //}

    private Sniper ReturnSniperType(int whichType, Transform _firePoint)
    {
        if(whichType == 0)
        {
            return new SniperAuto(_firePoint);
        }
        else if(whichType == 1)
        {
            return new SniperSemi(_firePoint);

        }
        else if(whichType == 2)
        {
            return new SniperBolt(_firePoint);
        }

        throw new System.NotImplementedException();

    }

    private Shotgun ReturnShotgunType(int whichType, Transform _firePoint)
    {
        if (whichType == 0)
        {
            return new ShotgunAuto(_firePoint);
        }
        else if (whichType == 1)
        {
            return new ShotgunSemiauto(_firePoint);

        }
        else if (whichType == 2)
        {
            return new ShotgunBolt(_firePoint);
        }

        throw new System.NotImplementedException();

    }

    private Gun ReturnGunType(int whichType, Transform _firePoint)
    {
        if (whichType == 0)
        {
            return new GunAuto(_firePoint);
        }
        else if (whichType == 1)
        {
            return new GunSemiauto(_firePoint);

        }
        else if (whichType == 2)
        {
            return new GunBolt(_firePoint);
        }

        throw new System.NotImplementedException();

    }



    //public void SetMechanismToWeapon(ref Mechanism myWeaponMechanism, int myWeaponPositionInArray)
    //{
    //    if(myWeaponPositionInArray == 0) //Francos
    //    {
    //        //int mechanismPos = Random.Range(1, mechanismTypes.Length); //Nomes semi o Repeticion
    //        //myWeaponMechanism = mechanismTypes[mechanismPos];            
    //        myWeaponMechanism = mechanismTypes[2];
    //    }
    //    else if (myWeaponPositionInArray == 1) //Pistolas
    //    {
    //        int mechanismPos = Random.Range(0, mechanismTypes.Length); 
    //        myWeaponMechanism = mechanismTypes[mechanismPos];
    //        myWeaponMechanism.isDoubleHand = Random.Range(0, 10) > 2; //Duales?
    //    }
    //    else if(myWeaponPositionInArray == 2)
    //    {
    //        int mechanismPos = Random.Range(0, mechanismTypes.Length); //Escopetas
    //        myWeaponMechanism = mechanismTypes[mechanismPos];
    //    }
    //}
}
