using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;
public class SpittingWorm : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform[] spawnlocations;
    public GameObject wormEnemy;
    private PlayerMovement player;

    [SerializeField]
    private GameObject wormProjectile;

    GameObject wormSpawn;
    bool hasShot;
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        hasShot= false;
        //spawnlocations = new List<Transform> ;
        //Debug.Log(spawnlocations.Length + "Why is it doing it twice");
        //int randomSpawn = Random.Range(0, spawnlocations.Length);
        //Transform thisSpawn = spawnlocations[randomSpawn];
        //Debug.Log(spawnlocations[randomSpawn]);

        //Instantiate(wormEnemy,this.transform.position,Quaternion.identity);
        //wormSpawn = Instantiate(wormEnemy, spawnlocations[(Random.Range(0,spawnlocations.Length))].transform.position,Quaternion.identity);
        StartCoroutine(waitForAppear());
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator waitForShot()
    {
        //hasShot = false;
        Instantiate(wormProjectile, this.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        this.GetComponent<SpriteRenderer>().DOFade(0, 1f);
        //this.GetComponent<SpriteRenderer>().DOColor(Color.white, 2.8f);
        yield return new WaitForSeconds(1f);
        StartCoroutine(waitForAppear());
        //hasShot= true;
        
        //wormProjectile.GetComponent<Rigidbody2D>().AddForce(transform.up * Random.Range(-4, 4), ForceMode2D.Impulse);
    }

    private IEnumerator waitForAppear() {
        //this.GetComponent<SpriteRenderer>().DOFade(0.5f, 2.5f);
        //Do the animation time it with shot

        this.transform.position = spawnlocations[(Random.Range(0, spawnlocations.Length))].transform.position;
        this.GetComponent<SpriteRenderer>().DOColor(Color.red, 2.8f);
        yield return new WaitForSeconds(3f);
        StartCoroutine(waitForShot());
    }
}


//31. Lifting Animation