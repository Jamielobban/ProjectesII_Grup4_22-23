using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{
    [SerializeField]
    public List<int> weaponIndexOrder = new List<int>();
    [SerializeField]
    public WeaponValues[] weaponsValues = new WeaponValues[4]; //Sniper, shotgun, Pistol - Auto, Semi, Bolt


    //Mechanism[] mechanismTypes = new Mechanism[3] { new Automatica(), new Seamiautomatica(), new Repeticion() };

    //[SerializeField] GameObject[] bulletPrefabs = new GameObject[3];



    int weaponIndex;

    //private int arrayPositionInHand;
    //private int arrayPositionOfNext;
    public float totalTime;
    bool setFirstWeapon = true;

    int currentWeapon;
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
            GetWeapons();
        }
    }

    private void Start()
    {
        restartStates();
        weaponIndex = 0;

        for (int i = 0; i < weaponsValues.Length; i++)
        {
            //Debug.Log(weaponsValues[i].WeaponName);

            if (PlayerPrefs.GetInt(weaponsValues[i].WeaponName + "Desbloqueada") == 1)
            {
                weaponIndexOrder.Add(i);

            }
        }

        //if (weaponIndexOrder.Count != 0)
        //{
        //    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<RightHand>().weaponInHand = new Weapon(GameObject.FindGameObjectWithTag("PlayerFirePoint").transform, weaponsValues[currentWeapon]);
        //    GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<RightHand>().weaponEquiped = true;
        //}

        // weaponIndexOrder.Add(3);
    }

    public void saveWeaponsState()
    {
        for (int i = 0; i < weaponsValues.Length; i++)
        {
            weaponsValues[i].SavePlayerPrefs();

        }
        PlayerPrefs.SetInt("CurrentWeapon", currentWeapon);

    }

    public bool getWeaponUnlock(string name)
    {
        bool a = false;
        for (int i = 0; i < weaponsValues.Length; i++)
        {
            if (weaponsValues[i].WeaponName == name)
            {
                a = weaponsValues[i].unLock;
            }
        }
        return a;


    }
    public void restartStates()
    {
        weaponsValues[1].restartWeapon();
        weaponsValues[0].restartWeapon();

        weaponsValues[2].restartWeapon();
        weaponsValues[3].restartWeapon();


        currentWeapon = PlayerPrefs.GetInt("CurrentWeapon", 0);

        GetWeapons();

    }
    public void GetWeapons()
    {
        if (weaponIndexOrder.Count != 0)
        {
            if (GameObject.FindGameObjectWithTag("Player") != null)
            {
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<RightHand>().weaponInHand = new Weapon(GameObject.FindGameObjectWithTag("PlayerFirePoint").transform, weaponsValues[currentWeapon]);
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<RightHand>().weaponEquiped = true;
            }

        }
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
                if (weaponIndex <= 0)
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
        currentWeapon = weaponIndexOrder[weaponIndex];


        setFirstWeapon = false;
        return true;
    }

    public void EquipWeapon(string weaponName, ref Weapon weaponInHand, ref Transform firePoint)
    {
        for (int i = 0; i < weaponsValues.Length; i++)
        {
            //Debug.Log(weaponsValues[i].WeaponName);

            if (weaponsValues[i].WeaponName == weaponName)
            {
                weaponIndexOrder.Add(i);
                weaponsValues[i].unLock = true;
                weaponInHand = new Weapon(firePoint, weaponsValues[i]);
                currentWeapon = i;

            }
        }

        if (weaponInHand == null)
        {

            this.SetWeapon(0, ref weaponInHand, ref firePoint);
            currentWeapon = 0;

        }
    }




}
