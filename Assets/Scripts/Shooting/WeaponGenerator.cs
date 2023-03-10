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



    int /*weaponInHandInt, nextWeaponInt,*/ weaponIndex;

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
        weaponIndexOrder.Add(0);
    }
    private void Update()
    {

    }
    public void SetWeapon(int indexChange, ref Weapon weaponInHand, ref Transform firePoint)
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
                    return;
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
                return;

                
        }
        
        weaponInHand = new Weapon(firePoint, weaponsValues[weaponIndexOrder[weaponIndex]]);
        setFirstWeapon = false;
    }

    

    
    
}
