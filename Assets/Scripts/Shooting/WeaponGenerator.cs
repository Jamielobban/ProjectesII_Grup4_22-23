using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponGenerator : MonoBehaviour
{

    [SerializeField]

    WeaponValues[] weaponsValues = new WeaponValues[7]; //Sniper, shotgun, Pistol - Auto, Semi, Bolt

    //Mechanism[] mechanismTypes = new Mechanism[3] { new Automatica(), new Seamiautomatica(), new Repeticion() };

    //[SerializeField] GameObject[] bulletPrefabs = new GameObject[3];



    int weaponInHandInt, nextWeaponInt;

    private int arrayPositionInHand;
    private int arrayPositionOfNext;
    public float totalTime;

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
    private void Update()
    {

    }

    private void Start()

    {



    }

    public Weapon SetMyInitialWeaponAndReturnMyNext(ref Weapon weaponInHand, Transform firePoint)
    {



        int random = Random.Range(0, 4);


        weaponInHandInt = random;

        if (random == 0)
        {
            weaponInHand = new SniperAuto(firePoint, weaponsValues[0]);
        }
        else if (random == 1)

        {

            weaponInHand = new SniperBolt(firePoint, weaponsValues[1]);

        }
        else if (random == 2)

        {

            weaponInHand = new ShotgunAuto(firePoint, weaponsValues[2]);

        }
        else if (random == 3)

        {

            weaponInHand = new GunSemiauto(firePoint, weaponsValues[3]);

        }

        //else if(random == 3)

        //{

        //    weaponInHand = new ShotgunAuto(firePoint, weaponsValues[3]);

        //}

        //else if (random == 4)

        //{

        //    weaponInHand = new GunAuto(firePoint, weaponsValues[4]);

        //}
        //else if (random == 5)

        //{

        //    weaponInHand = new Sawgun(firePoint, weaponsValues[5]);

        //}
        //else if (random == 6)

        //{

        //    weaponInHand = new ElectricGun(firePoint, weaponsValues[6]);

        //}



        do { random = Random.Range(0, 4); } while (weaponInHandInt == random);



        nextWeaponInt = random;



        if (random == 0)
        {
            return new SniperAuto(firePoint, weaponsValues[0]);

        }
        else if (random == 1)

        {

            return new SniperBolt(firePoint, weaponsValues[1]);

        }
        else if (random == 2)

        {

            return new ShotgunAuto(firePoint, weaponsValues[2]);

        }
        else if (random == 3)

        {

            return new GunSemiauto(firePoint, weaponsValues[3]);

        }

        //else if(random == 3){



        //    return new ShotgunAuto(firePoint, weaponsValues[3]);

        //}

        //else if (random == 4)

        //{

        //    return new GunAuto(firePoint, weaponsValues[4]);

        //}
        //else if (random == 5)
        //{

        //    return new Sawgun(firePoint, weaponsValues[5]);

        //}
        //else if (random == 6)

        //{

        //    return new ElectricGun(firePoint, weaponsValues[6]);

        //}



        throw new System.NotImplementedException();



    }

    public Weapon ReturnMyNextWeapon(Transform firePoint)
    {
        weaponInHandInt = nextWeaponInt;
        int random = Random.Range(0, 4);

        do { random = Random.Range(0, 4); } while (random == weaponInHandInt);

        nextWeaponInt = random;





        if (random == 0)
        {
            return new SniperAuto(firePoint, weaponsValues[0]);
        }
        else if (random == 1)
        {
            return new SniperBolt(firePoint, weaponsValues[1]);
        }
        else if (random == 2)
        {
            return new ShotgunAuto(firePoint, weaponsValues[2]);
        }
        else if (random == 3)
        {
            return new GunSemiauto(firePoint, weaponsValues[3]);
        }
        //else if (random == 3)
        //{
        //    return new ShotgunAuto(firePoint, weaponsValues[3]);
        //}

        //else if (random == 4)
        //{
        //    return new GunAuto(firePoint, weaponsValues[4]);
        //}
        //else if (random == 5)

        //{

        //    return new Sawgun(firePoint, weaponsValues[5]);

        //}
        //else if (random == 6)

        //{

        //    return new ElectricGun(firePoint, weaponsValues[6]);

        //}



        throw new System.NotImplementedException();
    }

    //public void ResetArrayValues(Transform firePoint)
    //{
    //    weaponsTypes = new Weapon[3] { new Sniper(firePoint), new Shotgun(firePoint), new Gun(firePoint) };
    //    mechanismTypes = new Mechanism[3] { new Automatica(), new Seamiautomatica(), new Repeticion() };
    //}

    //private Sniper ReturnSniperType(int whichType, Transform _firePoint)
    //{
    //    if(whichType == 0)
    //    {
    //        return new SniperAuto(_firePoint, weaponsValues[0]);
    //    }
    //    else if(whichType == 1)
    //    {
    //        return new SniperSemi(_firePoint, weaponsValues[1]);

    //    }
    //    else if(whichType == 2)
    //    {
    //        return new SniperBolt(_firePoint, weaponsValues[2]);
    //    }

    //    throw new System.NotImplementedException();

    //}

    //private Shotgun ReturnShotgunType(int whichType, Transform _firePoint)
    //{
    //    if (whichType == 0)
    //    {
    //        return new ShotgunAuto(_firePoint, weaponsValues[3]);
    //    }
    //    else if (whichType == 1)
    //    {
    //        return new ShotgunSemiauto(_firePoint, weaponsValues[4]);

    //    }
    //    else if (whichType == 2)
    //    {
    //        return new ShotgunBolt(_firePoint, weaponsValues[5]);
    //    }

    //    throw new System.NotImplementedException();

    //}

    //private Gun ReturnGunType(int whichType, Transform _firePoint)
    //{
    //    if (whichType == 0)
    //    {
    //        return new GunAuto(_firePoint, weaponsValues[6]);
    //    }
    //    else if (whichType == 1)
    //    {
    //        return new GunSemiauto(_firePoint, weaponsValues[7]);

    //    }
    //    else if (whichType == 2)
    //    {
    //        return new GunBolt(_firePoint, weaponsValues[8]);
    //    }

    //    throw new System.NotImplementedException();

    //}



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
