using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormProjectile : MonoBehaviour
{
    GameObject[] pelletsOnBullet = new GameObject[8];
    public GameObject bigBall;
    private PlayerMovement player;
    Vector3 direction;
    float angle;

    //private Rigidbody2D thisisWow;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();

        direction = player.transform.position - this.transform.position;
        this.GetComponent<Rigidbody2D>().AddForce(direction.normalized * 10,ForceMode2D.Impulse);

        StartCoroutine(WaitForShoot());
        
    }


    private IEnumerator WaitForShoot()
    {
        yield return new WaitForSeconds(1);
        direction = player.transform.position - this.transform.position;
        angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) + 90;
        for (int i = 0, grados = -15; i < pelletsOnBullet.Length; i++, grados += 3)
        {
            pelletsOnBullet[i] = GameObject.Instantiate(bigBall, transform.position, transform.rotation);
            pelletsOnBullet[i].GetComponent<Rigidbody2D>().AddForce(transform.up * 4, ForceMode2D.Impulse);
            pelletsOnBullet[i].transform.Rotate(0, 0, angle + Random.Range(-15, 15));


        }
        Destroy(this.gameObject);
    }
}
