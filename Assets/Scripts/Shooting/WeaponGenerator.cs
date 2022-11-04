using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{
    Weapon[] weaponsTypes = new Weapon[3];
    Mechanism[] mechanismTypes = new Mechanism[3] { new Automatica(), new Seamiautomatica(), new Repeticion() };
    [SerializeField] GameObject[] bulletPrefabs = new GameObject[3];

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

    public Weapon SetMyInitialWeaponAndReturnMyNext(ref Weapon weaponInHand, Transform firePoint)
    {
        weaponsTypes = new Weapon[3] { new Sniper(firePoint), new Shotgun(firePoint), new Gun(firePoint) };

        //arrayPositionInHand = Random.Range(0, weaponsTypes.Length);
        //weaponInHand = weaponsTypes[arrayPositionInHand];       
        //arrayPositionOfNext = arrayPositionInHand;

        //do { arrayPositionOfNext = Random.Range(0, weaponsTypes.Length); }while(arrayPositionOfNext == arrayPositionInHand);
        //return weaponsTypes[arrayPositionOfNext];
        weaponInHand = weaponsTypes[0];
        weaponInHand.bulletTypePrefab = bulletPrefabs[0];

        return weaponInHand;

    }

    public Weapon ReturnMyNextWeapon()
    {
        arrayPositionInHand = arrayPositionOfNext;
        do { arrayPositionOfNext = Random.Range(0, weaponsTypes.Length); } while (arrayPositionOfNext == arrayPositionInHand);
        return weaponsTypes[arrayPositionOfNext];
    }

    public void ResetArrayValues(Transform firePoint)
    {
        weaponsTypes = new Weapon[3] { new Sniper(firePoint), new Shotgun(firePoint), new Gun(firePoint) };
        mechanismTypes = new Mechanism[3] { new Automatica(), new Seamiautomatica(), new Repeticion() };
    }

    public void SetMechanismToWeapon(ref Mechanism myWeaponMechanism, int myWeaponPositionInArray)
    {
        if(myWeaponPositionInArray == 0) //Francos
        {
            //int mechanismPos = Random.Range(1, mechanismTypes.Length); //Nomes semi o Repeticion
            //myWeaponMechanism = mechanismTypes[mechanismPos];            
            myWeaponMechanism = mechanismTypes[2];
        }
        else if (myWeaponPositionInArray == 1) //Pistolas
        {
            int mechanismPos = Random.Range(0, mechanismTypes.Length); 
            myWeaponMechanism = mechanismTypes[mechanismPos];
            myWeaponMechanism.isDoubleHand = Random.Range(0, 10) > 2; //Duales?
        }
        else
        {
            int mechanismPos = Random.Range(0, mechanismTypes.Length);
            myWeaponMechanism = mechanismTypes[mechanismPos];
        }
    }
}
