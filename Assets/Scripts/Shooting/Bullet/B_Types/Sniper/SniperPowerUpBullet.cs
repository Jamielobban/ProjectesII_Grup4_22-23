using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SniperPowerUpBullet : MonoBehaviour
{
    public GameObject[] enemies;
    GameObject enemyPosition;
    public GameObject marcador;

    int maxRebotes;
    bool canShoot;
    GameObject marc;

    private void Start()
    {
        maxRebotes = 0;
        enemies = new GameObject[0];
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
        if (enemies.Length > 0)
        {
            clearArray();
        }

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length > 0)
        {
            int enemy = Random.Range(0, enemies.Length - 1);
            enemyPosition = enemies[enemy];


            Time.timeScale = 0.1f;
            canShoot = true;
            StartCoroutine(endPowerUp(5f * Time.timeScale));
        }



    }
    private void Update()
    {
        if (canShoot && Input.GetButtonDown("Shoot"))
        {
            this.transform.parent.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

            //Vector3 relativePos = enemyPosition.position - transform.position;

            //float angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg - rotation;

            //Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            Vector3 from = transform.up;
            Vector3 to = enemyPosition.transform.position - transform.position;

            float angle = Vector3.SignedAngle(from, to, transform.forward);
            this.transform.parent.Rotate(0.0f, 0.0f, angle);


            this.transform.parent.GetComponent<Rigidbody2D>().AddForce(this.transform.parent.up * this.transform.parent.GetComponent<Bullet>().GetSpeed(), ForceMode2D.Impulse);


            StartCoroutine(endPowerUp(0));

        }
    }
    void clearArray()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i] = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            StartCoroutine(endPowerUp(0));


            clearArray();
            enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (enemies.Length > 1)
            {
                StartCoroutine(findEnemy(0));

            }
            else if (enemies.Length == 1 && collision.gameObject != null)
            {

                this.transform.parent.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                this.transform.parent.GetComponent<Rigidbody2D>().AddForce(this.transform.parent.up * -this.transform.parent.GetComponent<Bullet>().GetSpeed(), ForceMode2D.Impulse);
                StartCoroutine(findEnemy(0.05f));


            }






        }
        else if (collision.gameObject.tag == "MapLimit")
        {
            Resources.Load<FloatValue>("ScriptableObjects/Weapons/Snipers/SniperBolt/SnB_FloatValues/SnB_MaxTimePowerup").RuntimeValue = 0.01f;
            StartCoroutine(endPowerUp(0.01f));

        }

    }
}