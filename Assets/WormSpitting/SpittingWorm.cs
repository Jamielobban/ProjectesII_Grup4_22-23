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
        
        StartCoroutine(waitForAppear());        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator waitForShot()
    {
        //hasShot = false;
        Instantiate(wormProjectile, this.transform.position, Quaternion.identity); //dispara
        yield return new WaitForSeconds(1f);                                       //espera 1s
        this.GetComponent<SpriteRenderer>().DOFade(0, 1f);                         //Triga 1s en estar amagat
        //this.GetComponent<SpriteRenderer>().DOColor(Color.white, 2.8f);
        yield return new WaitForSeconds(1f);                                       //Espera 1s amagat
        StartCoroutine(waitForAppear());                                           //Tria nou punt d'aparicio
                                                                                   //Triga 2.8 segons a apareixer del tot
                                                                                   //Espera 3 segons abans de tornar a disparar
    }

    private IEnumerator waitForAppear() {        

        this.transform.position = spawnlocations[(Random.Range(0, spawnlocations.Length))].transform.position;
        this.GetComponent<SpriteRenderer>().DOColor(Color.red, 2.8f);
        yield return new WaitForSeconds(3f);
        StartCoroutine(waitForShot());
    }
}


