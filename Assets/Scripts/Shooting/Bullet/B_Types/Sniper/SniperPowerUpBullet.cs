using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperPowerUpBullet : MonoBehaviour
{
    GameObject[] enemies;
    Transform enemyPosition;
    public GameObject marcador;


    bool canShoot;
    GameObject marc;

    private void Start()
    {
        canShoot = false;
        StartCoroutine(findEnemy(0.05f));    
    }

    private IEnumerator endPowerUp(float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("afwf");
        Time.timeScale = 1;
        canShoot = false;
        Destroy(marc);
    }
    private IEnumerator findEnemy(float time)
    {

        yield return new WaitForSeconds(time);
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            int enemy = Random.Range(0, enemies.Length-1);
            enemyPosition = enemies[enemy].transform;


            Time.timeScale = 0.05f;
            canShoot = true;
            StartCoroutine(endPowerUp(0.5f * Time.timeScale));
        }



    }
    private void Update()
    {
        if(canShoot && Input.GetButtonDown("Shoot"))
        {
            this.transform.parent.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            //Vector3 relativePos = enemyPosition.position - transform.position;

            //float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg - rotation;

            //Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            Vector3 from = transform.up;
            Vector3 to = enemyPosition.position - transform.position;

            float angle = Vector3.SignedAngle(from, to, transform.forward);
            this.transform.parent.Rotate(0.0f, 0.0f, angle);


            this.transform.parent.GetComponent<Rigidbody2D>().AddForce(this.transform.parent.up * this.transform.parent.GetComponent<Bullet>().GetSpeed(), ForceMode2D.Impulse);


            StartCoroutine(endPowerUp(0));

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(endPowerUp(0));

            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if(enemies.Length >1)
            {
            StartCoroutine(findEnemy(0));

            }


        }
        else if (collision.gameObject.tag == "MapLimit")
        {
            StartCoroutine(endPowerUp(0.01f));

        }

    }
}
