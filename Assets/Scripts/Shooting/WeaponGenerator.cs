using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{
    [SerializeField] Weapon[] weaponsTypes = new Weapon[3] { new Sniper(), new Shotgun(), new Gun()};
    [SerializeField] Mechanism[] mechanismTypes = new Mechanism[3] { new Automatica(), new Seamiautomatica(), new Repeticion() };
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

    public Weapon SetMyInitialWeaponAndReturnMyNext(ref Weapon weaponInHand)
    {
        arrayPositionInHand = Random.Range(0, weaponsTypes.Length);
        weaponInHand = weaponsTypes[arrayPositionInHand];       
        arrayPositionOfNext = arrayPositionInHand;

        do { arrayPositionOfNext = Random.Range(0, weaponsTypes.Length); }while(arrayPositionOfNext == arrayPositionInHand);
        return weaponsTypes[arrayPositionOfNext];
    }

    public Weapon ReturnMyNextWeapon()
    {
        arrayPositionInHand = arrayPositionOfNext;
        do { arrayPositionOfNext = Random.Range(0, weaponsTypes.Length); } while (arrayPositionOfNext == arrayPositionInHand);
        return weaponsTypes[arrayPositionOfNext];
    }

    public void ResetArrayValues()
    {
        weaponsTypes = new Weapon[3] { new Sniper(), new Shotgun(), new Gun() };
        mechanismTypes = new Mechanism[3] { new Automatica(), new Seamiautomatica(), new Repeticion() };
    }

    private void SetMechanismToWeapon(ref Mechanism myWeaponMechanism, int myWeaponPositionInArray)
    {
        if(myWeaponPositionInArray == 0) //Francos
        {
            int mechanismPos = Random.Range(1, mechanismTypes.Length); //Nomes semi o Repeticion
            myWeaponMechanism = mechanismTypes[mechanismPos];            
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
