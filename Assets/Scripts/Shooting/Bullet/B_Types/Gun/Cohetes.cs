using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cohetes : Bullet
{
    public GameObject muzzle;
    public GameObject teledirigido;
    public GameObject explosionTel;

    // Start is called before the first frame update
    void Start()
    {
        //bulletDamage = 25 * _damageMultiplier;
        //bulletRangeInMetres = 150;
        //bulletSpeedMetresPerSec = 25;
        //bulletRadius = 0.23f;
        bulletData.bulletDamage *= bulletData._damageMultiplier;


        //this.transform.Rotate(0,0, this.transform.rotation.z + Random.RandomRange(-5, 5));

        //this.GetComponent<Rigidbody2D>().AddForce(-this.transform.up * bulletSpeedMetresPerSec, ForceMode2D.Impulse);
        GameObject explosion = Instantiate(muzzle, this.transform.position, this.transform.rotation);
        Destroy(explosion, 0.2f);

        StartCoroutine(teledirigir(0.4f));
    }

    private IEnumerator teledirigir(float time)
    {

        yield return new WaitForSeconds(time);

        GameObject explosion = Instantiate(explosionTel, this.transform.position, this.transform.rotation);
        explosion.GetComponent<Bullet>().ApplyMultiplierToDamage(bulletData._damageMultiplier);

        GameObject misil1 = Instantiate(teledirigido, this.transform.position, this.transform.rotation);
        misil1.GetComponent<Bullet>().FireProjectile();

        misil1.transform.Rotate(0f, 0f, misil1.transform.rotation.z + 0);

        misil1.GetComponent<Rigidbody2D>().AddForce(misil1.transform.up * -misil1.GetComponent<Bullet>().bulletData.bulletSpeedMetresPerSec, ForceMode2D.Impulse);
        misil1.GetComponent<Bullet>().ApplyMultiplierToDamage(bulletData._damageMultiplier);

        GameObject misil2 = Instantiate(teledirigido, this.transform.position, this.transform.rotation);
        misil2.GetComponent<Bullet>().FireProjectile();

        misil2.transform.Rotate(0f, 0f, misil2.transform.rotation.z + 6);

        misil2.GetComponent<Rigidbody2D>().AddForce(misil2.transform.up * -misil2.GetComponent<Bullet>().bulletData.bulletSpeedMetresPerSec, ForceMode2D.Impulse);
        misil2.GetComponent<Bullet>().ApplyMultiplierToDamage(bulletData._damageMultiplier);

        GameObject misil3 = Instantiate(teledirigido, this.transform.position, this.transform.rotation);
        misil3.GetComponent<Bullet>().FireProjectile();

        misil3.transform.Rotate(0f, 0f, misil3.transform.rotation.z - 6);

        misil3.GetComponent<Rigidbody2D>().AddForce(misil3.transform.up * -misil3.GetComponent<Bullet>().bulletData.bulletSpeedMetresPerSec, ForceMode2D.Impulse);
        misil3.GetComponent<Bullet>().ApplyMultiplierToDamage(bulletData._damageMultiplier);

        Destroy(this.gameObject);

    }


    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("MapLimit"))
        {
            base.ImpactWall();
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            base.ImpactBody();
            collision.gameObject.GetComponent<Entity>().GetDamage(bulletData.bulletDamage, HealthStateTypes.NORMAL, 0, this.transform.position);

        }
    }

}
