using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{
    Weapon[] weaponsTypes = new Weapon[3];
    //Mechanism[] mechanismTypes = new Mechanism[3] { new Automatica(), new Seamiautomatica(), new Repeticion() };
    //[SerializeField] GameObject[] bulletPrefabs = new GameObject[3];

    Weapon nextWeapon;

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
        
        int random = Random.Range(0, 0);
        if(random == 0)
        {
            weaponInHand = ReturnSniperType(Random.Range(0, 2), firePoint, ref _sr);
        }

        random = Random.Range(0, 0);
        int random2 = Random.Range(0, 3);

        if (random == 0)
        {
            return ReturnSniperType(random2, firePoint, ref _sr);
        }

        return ReturnSniperType(random2, firePoint, ref _sr);

    }

    public Weapon ReturnMyNextWeapon()
    {
        arrayPositionInHand = arrayPositionOfNext;
        do { arrayPositionOfNext = Random.Range(0, weaponsTypes.Length); } while (arrayPositionOfNext == arrayPositionInHand);
        return weaponsTypes[arrayPositionOfNext];
    }

    //public void ResetArrayValues(Transform firePoint)
    //{
    //    weaponsTypes = new Weapon[3] { new Sniper(firePoint), new Shotgun(firePoint), new Gun(firePoint) };
    //    mechanismTypes = new Mechanism[3] { new Automatica(), new Seamiautomatica(), new Repeticion() };
    //}

    private Sniper ReturnSniperType(int whichType, Transform _firePoint, ref SpriteRenderer _sr)
    {
        if(whichType == 0)
        {
            return new SniperAuto(_firePoint,ref _sr);
        }
        else if(whichType == 1)
        {
            return new SniperSemi(_firePoint, ref _sr);

        }
        else if(whichType == 2)
        {
            return new SniperBolt(_firePoint, ref _sr);
        }
        return new SniperBolt(_firePoint, ref _sr);

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
