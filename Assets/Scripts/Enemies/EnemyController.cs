using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using System.Threading.Tasks;

public class EnemyController : MonoBehaviour
{
    public bool isDeath;
    private enum State
    {
        Chasing, Firing, Hit
    }
    private State state;

    public float enemyHealth;
    public float bulletSpeed;

    public float timeBetweenShots;
    public float timeStartedShootingCrazy;
    public float lastShoot;

    public Color originalColor;

    public Transform enemyFirePoint;
    public GameObject enemyBulletPrefab;

    public float speed;
    public float stoppingDistance;
    public float retreatDistance;

    public float enemyBulletDamage;    

    private Transform player;

    public bool canShoot;

    private Rigidbody2D rb;
    private Vector2 movement;
    public  SpriteRenderer sr;

    public AudioClip enemyShootSound;

    public bool canSeePlayer;
    public bool inRange;
    public float knockbackPower;

    public int angleOfCone;
    public int numberOfBursts;

    public AudioClip damageSound;
    public GameObject floorBlood;

    public HealthStateTypes actualHealthState;
    public float durationActualState;
    public float timeEnterLastState;
    public float lastTimeDamagedByHealthState ;
    public float timeBetweenDamagesByHealthState ;
    public float damageActualHealthStateApply ;
    public float timeAddedToHealthState = 0;

    public bool metralleta;
    void Start()
    {
        state = State.Chasing;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        sr = this.GetComponent<SpriteRenderer>();
        rb = this.GetComponent<Rigidbody2D>();
        lastShoot = 0;
        originalColor = sr.color;
        actualHealthState = HealthStateTypes.NORMAL;
    }

    //public void GetDamage(BulletHitInfo impactInfo)
    //{
    //    Debug.Log("In");
    //    AudioManager.Instance.PlaySound(damageSound);
    //    GameObject blood = GameObject.Instantiate(floorBlood, impactInfo.impactPosition, this.transform.rotation);
    //    blood.GetComponent<Transform>().localScale = transform.localScale*2;

    //    if (enemyHealth <= impactInfo.damage)
    //    {
    //        enemyHealth = 0;
    //        isDeath = true;
    //    }
    //    else
    //    {
    //        enemyHealth -= impactInfo.damage;
    //    }

    //    //Debug.Log(enemyHealth);
        
    //}
    public HealthStateTypes GetActualHealthState()
    {
        return actualHealthState;
    }

    public void GetDamage(Action damageAction)
    {
        damageAction();
        //Debug.Log("In");
        //AudioManager.Instance.PlaySound(damageSound);
        //GameObject blood = GameObject.Instantiate(floorBlood, impactInfo.impactPosition, this.transform.rotation);
        //blood.GetComponent<Transform>().localScale = transform.localScale * 2;

        //if (enemyHealth <= impactInfo.damage)
        //{
        //    enemyHealth = 0;
        //    isDeath = true;
        //}
        //else
        //{
        //    enemyHealth -= impactInfo.damage;
        //}
        //Debug.Log(enemyHealth);

    }
    //public void ChangeHealthState(float durationTimeNewState, float timeBetweenDamagingByState, float damagePerTimeValueHealthState,HealthStateTypes newHealthState)
    //{
    //    switch (newHealthState)
    //    {
    //        case HealthStateTypes.BURNED:                
    //            sr.color = Color.red;                
    //            break;
    //        case HealthStateTypes.FREEZE:
    //            sr.color = Color.blue;                
    //            break;
    //        case HealthStateTypes.PARALYZED:
    //            sr.color = Color.yellow;                
    //            break;            
    //        default:
    //            break;
    //    }

    //    actualHealthState = newHealthState;        
    //    durationActualState = durationTimeNewState;
    //    timeEnterLastState = Time.time;
    //    timeBetweenDamagesByHealthState = timeBetweenDamagingByState;
    //    damageActualHealthStateApply = damagePerTimeValueHealthState;

    //    StartCoroutine(ApplyHealtHStateDamageWhileItsActive(durationTimeNewState));
    //}

    public void SetLastEnter()
    {
        timeEnterLastState = Time.time;
    }

    //void ApplyHealtHStateDamageWhileItsActive(float durationTimeNewState, float timeBetweenDamagingByState, float damagePerTimeValueHealthState, HealthStateTypes newHealthState)
    //{
    //    switch (newHealthState)
    //    {
    //        case HealthStateTypes.BURNED:
    //            sr.color = Color.red;
    //            break;
    //        case HealthStateTypes.FREEZE:
    //            sr.color = Color.blue;
    //            break;
    //        case HealthStateTypes.PARALYZED:
    //            sr.color = Color.yellow;
    //            break;
    //        default:
    //            break;
    //    }

    //    actualHealthState = newHealthState;

    //}

    public float AddTimeToHealthState()
    {
        var aux = timeEnterLastState - Time.time;
        timeEnterLastState = Time.time;
        return aux;
    }

    public IEnumerator ApplyNewHealthStateConsequences(float durationTimeNewState, float timeBetweenDamagingByState, float damagePerTimeValueHealthState, HealthStateTypes newHealthState)
    {
        timeEnterLastState = Time.time;
        timeAddedToHealthState = 0;
        GameObject feedbackObject = null;

        switch (newHealthState)
        {
            case HealthStateTypes.BURNED:
                feedbackObject = (GameObject)Resources.Load("Prefab/FireDamage");
                sr.color = Color.red;
                break;
            case HealthStateTypes.FREEZE:
                feedbackObject = (GameObject)Resources.Load("Prefab/FreezeDamage");
                sr.color = Color.blue;
                break;
            case HealthStateTypes.PARALYZED:
                feedbackObject = (GameObject)Resources.Load("Prefab/ParalyzedDamage");
                sr.color = Color.yellow;
                break;
            default:
                break;
        }

        actualHealthState = newHealthState;

        var timeEnd = durationTimeNewState + Time.time;

        Debug.Log(timeAddedToHealthState);

        while (Time.time < timeEnd + timeAddedToHealthState) 
        {
            if (enemyHealth <= damageActualHealthStateApply)
            {
                enemyHealth = 0;
                isDeath = true;
                yield break;
            }
            else
            {
                enemyHealth -= damageActualHealthStateApply;
                AudioManager.Instance.PlaySound(damageSound, this.gameObject.transform);
                if (feedbackObject != null)
                {
                    GameObject.Instantiate(feedbackObject, this.transform, false);
                    feedbackObject.transform.localScale = new Vector3(sr.gameObject.transform.localScale.x * 3, sr.gameObject.transform.localScale.y * 3, 1);
                }
            }

            yield return new WaitForSeconds(timeBetweenDamagingByState);

        }

        if(!isDeath)
            BackToNormalState();
    }


    //public IEnumerator ApplyHealtHStateDamageWhileItsActive(float durationTimeNewState, float timeBetweenDamagingByState, float damagePerTimeValueHealthState, HealthStateTypes newHealthState)
    //{
    //    switch (newHealthState)
    //    {
    //        case HealthStateTypes.BURNED:
    //            sr.color = Color.red;
    //            break;
    //        case HealthStateTypes.FREEZE:
    //            sr.color = Color.blue;
    //            break;
    //        case HealthStateTypes.PARALYZED:
    //            sr.color = Color.yellow;
    //            break;
    //        default:
    //            break;
    //    }

    //    actualHealthState = newHealthState;

    //    var end = durationTimeNewState + Time.time;
    //    var end2 = timeBetweenDamagesByHealthState + Time.time;

    //    //if (enemyHealth <= damageActualHealthStateApply)
    //    //{
    //    //    enemyHealth = 0;
    //    //    isDeath = true;
    //    //}
    //    //else
    //    //{
    //    //    enemyHealth -= damageActualHealthStateApply;
    //    //    AudioManager.Instance.PlaySound(damageSound);
    //    //}

    //    while (Time.time < end)
    //    {
    //        if (enemyHealth <= damageActualHealthStateApply)
    //        {
    //            enemyHealth = 0;
    //            isDeath = true;
    //        }
    //        else
    //        {
    //            enemyHealth -= damageActualHealthStateApply;
    //            AudioManager.Instance.PlaySound(damageSound);
    //        }

        
    //        yield return new WaitWhile(() => Time.time > end2);

    //        end2 = timeBetweenDamagesByHealthState + Time.time;




    //    }

    //    BackToNormalState();        
    //}

    void BackToNormalState()
    {
        timeEnterLastState = 0;
        actualHealthState = HealthStateTypes.NORMAL;
        sr.color = originalColor;
        damageActualHealthStateApply = 0;
        timeBetweenDamagesByHealthState = 0;
        durationActualState = 0;
        timeAddedToHealthState = 0;
    }
    void Update()
    {
        //if (enemyHealth <= 0)
        //{
        //    DeathAnimation();
        //}


        //Debug.Log(actualHealthState);
        //if(actualHealthState != HealthStateTypes.NORMAL)
        //{
        //    if(Time.time - timeEnterLastState >= durationActualState)
        //    {
        //        Debug.Log("a");
        //        actualHealthState = HealthStateTypes.NORMAL;
        //        sr.color = originalColor;
        //        lastTimeDamagedByHealthState = 0;
        //        timeBetweenDamagesByHealthState = 0;
        //        damageActualHealthStateApply = 0;
        //    }
        //    else if(actualHealthState == HealthStateTypes.BURNED)
        //    {
                

        //        if (Time.time - lastTimeDamagedByHealthState >= timeBetweenDamagesByHealthState)
        //        {
        //            if (enemyHealth <= damageActualHealthStateApply)
        //            {
        //                enemyHealth = 0;
        //                isDeath = true;
        //            }
        //            else
        //            {
        //                enemyHealth -= damageActualHealthStateApply;
        //                AudioManager.Instance.PlaySound(damageSound);
        //            }
        //        }
        //    }
        //}

        if (isDeath)
        {
            GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>().enemiesInRoom.Remove(this.gameObject);
            if (GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>().enemiesInRoom.Count == 0)
            {
                GameObject.FindGameObjectWithTag("RoomManager").GetComponent<RoomManager>().spawnRound();
            }
            Destroy(this.gameObject);
        }

        switch (state)
        {
            case State.Chasing:

                if (inRange)
                {
                    state = State.Firing;
                }
                break;
            case State.Firing:                
                if (!inRange)
                {
                    state = State.Chasing;
                }
                else if ((Time.time - lastShoot >= timeBetweenShots))
                {
                    if (metralleta)
                    {

                        MachineGunShoot();
                    }
                    else
                    {
                        Shoot();
                    }
                    lastShoot = Time.time;
                }
                break;
            case State.Hit:
                if (enemyHealth <= 0)
                {
                    DeathAnimation();
                }
                sr.DOColor(originalColor, 0.6f);
                //rb.AddForce(transform.right * -knockbackPower, ForceMode2D.Impulse);
                lastShoot = Time.time;
                //state = State.Chasing;
                break;
            default:
                break;
        }
    }

    private void FixedUpdate()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;

        switch (state)
        {
            case State.Chasing:


                if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.fixedDeltaTime);
                    inRange = false;
                }
                else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
                {
                    transform.position = this.transform.position;
                    inRange = true;
                }
                break;
            case State.Firing:
                if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
                {
                    inRange = false;
                }
                else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
                {
                    transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.fixedDeltaTime);
                    inRange = true;
                }
                break;
            case State.Hit:
                break;
            default:
                break;
        }
    }

    void Shoot()
    {
        for (int i = 0; i < numberOfBursts; i++)
        {
            StartCoroutine(Load(0.2f * i));
        }

        //reloading = true;
        //enemyState = States.MOVEMENT;
        //StartCoroutine(reload(reloadTime));

    }

    int a = 0;
    bool bAverOn = false;

    void MachineGunShoot()
    {
        //float tempDistance = 0;
        //if (timeStartedShootingCrazy - Time.time >= 0)
        //{
        // tempDistance = stoppingDistance;
        //stoppingDistance = 30;
        //random machine gun
        GameObject instance = Instantiate(enemyBulletPrefab, enemyFirePoint.transform.position, enemyFirePoint.rotation);
        instance.GetComponent<EnemyProjectile>().bulletData.damage = enemyBulletDamage;
        AudioManager.Instance.PlaySound(enemyShootSound, enemyFirePoint.transform.position);
        //Metralleta
        instance.transform.Rotate(0, 0, instance.transform.rotation.z + UnityEngine.Random.Range(-25,25));


            //banda a banda
            //instance.transform.Rotate(0, 0, instance.transform.rotation.z + a);

            instance.GetComponent<Rigidbody2D>().AddForce(instance.transform.right * bulletSpeed, ForceMode2D.Impulse);
            Destroy(instance, 3);

            //banda a banda
            if (bAverOn)
            {
                a += 10;
                if (a == 30)
                {
                    bAverOn = false;
                }
            }
            else
            {
                a -= 10;
                if (a == -30)
                {
                    bAverOn = true;

                }
            }
        //}
        //else
        //{
        //    //stoppingDistance = tempDistance;
        //    state = State.Chasing;
        //}
    }
    private IEnumerator Load(float waitTime)
    {
        if (inRange)
        {
            //GameObject enemyBullet = Instantiate(enemyBulletPrefab, enemyFirePoint.position, enemyFirePoint.rotation);
            //Rigidbody2D rbBullet = enemyBullet.GetComponent<Rigidbody2D>();
            //rbBullet.AddForce(enemyFirePoint.right * bulletSpeed, ForceMode2D.Impulse);
            //rb.AddForce(transform.right * -knockbackPower, ForceMode2D.Impulse);



            yield return new WaitForSeconds(waitTime);
            int par = 0;
            if (angleOfCone % 2 == 0)
                par = 5;


            for (int i = 0, grados = ((angleOfCone / 2) * -10) + par; i < angleOfCone; i++, grados += 10)
            {
                GameObject instance = Instantiate(enemyBulletPrefab, enemyFirePoint.transform.position, enemyFirePoint.rotation);
                instance.GetComponent<EnemyProjectile>().bulletData.damage = enemyBulletDamage;
                AudioManager.Instance.PlaySound(enemyShootSound, enemyFirePoint.transform.position);
                instance.transform.Rotate(0, 0, instance.transform.rotation.z + grados);

                instance.GetComponent<Rigidbody2D>().AddForce(instance.transform.right * bulletSpeed, ForceMode2D.Impulse);
                //Destroy(instance, 3);
            }
        }
    }

    void DeathAnimation()
    {
        //rb.AddForce(transform.right * -knockbackPower, ForceMode2D.Impulse);
        Invoke("DestroyGameObject", 1f);
        isDeath = true;
        sr.DOColor(Color.clear, 1f);
    }

    

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Bullet"))
    //    {
    //        Debug.Log(("ive been hit"));
    //        enemyHealth -= 20;
    //        sr.DOColor(Color.red, 0.0f);
    //        state = State.Hit;
    //        StartCoroutine(hitEnemy());
    //    }
    //}

    //private void DestroyGameObject()
    //{
    //    Destroy(this.gameObject);
    //}
    //private IEnumerator hitEnemy()
    //{
    //    state = State.Hit;
    //    rb.AddForce(transform.right * -knockbackPower, ForceMode2D.Impulse);
    //    yield return new WaitForSeconds(2);
    //    enemyHealth -= 20;
    //    sr.DOColor(Color.red, 0.0f);
    //    state = State.Chasing;
    //}
}
