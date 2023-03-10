using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{
    [SerializeField]
    List<int> weaponIndexOrder = new List< int >();
    [SerializeField]
    WeaponValues[] weaponsValues = new WeaponValues[4]; //Sniper, shotgun, Pistol - Auto, Semi, Bolt
    

    //Mechanism[] mechanismTypes = new Mechanism[3] { new Automatica(), new Seamiautomatica(), new Repeticion() };

    //[SerializeField] GameObject[] bulletPrefabs = new GameObject[3];



    int weaponIndex;

    //private int arrayPositionInHand;
    //private int arrayPositionOfNext;
    public float totalTime;
    bool setFirstWeapon = true;

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
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        weaponIndex = 0;
       // weaponIndexOrder.Add(3);
    }
    private void Update()
    {
        //Debug.Log(weaponIndexOrder.Count);
    }
    public bool SetWeapon(int indexChange, ref Weapon weaponInHand, ref Transform firePoint)
    {
        switch (indexChange)
        {
            case -1:
                if(weaponIndex <= 0)
                {
                    weaponIndex = weaponIndexOrder.Count - 1;
                }
                else
                {
                    weaponIndex--;
                }
                break;
            case 0:
                if (!setFirstWeapon)
                {
                    return false;
                }
                else
                {
                    setFirstWeapon = false;
                }
                break;
            case 1:
                if (weaponIndex >= weaponIndexOrder.Count - 1)
                {
                    weaponIndex = 0;
                }
                else
                {
                    weaponIndex++;
                }
                break;            
            default:
                return false;

                
        }
        
        weaponInHand = new Weapon(firePoint, weaponsValues[weaponIndexOrder[weaponIndex]]);
        setFirstWeapon = false;
        return true;
    }

    public void EquipWeapon(string weaponName, ref Weapon weaponInHand, ref Transform firePoint)
    {        
        for(int i = 0; i < weaponsValues.Length; i++)
        {
            //Debug.Log(weaponsValues[i].WeaponName);

            if (weaponsValues[i].WeaponName == weaponName)
            {               
                weaponIndexOrder.Add(i);
            }
        }

        if(weaponInHand == null)
        {
            this.SetWeapon(0, ref weaponInHand, ref firePoint);
        }
    }
    

    
    
}
