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
        bulletDamage = 25 * _damageMultiplier;
        bulletRangeInMetres = 150;
        bulletSpeedMetresPerSec = 25;
        bulletRadius = 0.23f;

        this.transform.Rotate(0,0, this.transform.rotation.z + Random.RandomRange(-5, 5));

        this.GetComponent<Rigidbody2D>().AddForce(-this.transform.up * bulletSpeedMetresPerSec, ForceMode2D.Impulse);
        GameObject explosion = Instantiate(muzzle, this.transform.position, this.transform.rotation);
        Destroy(explosion, 0.2f);

        StartCoroutine(teledirigir(0.4f));
    }

    private IEnumerator teledirigir(float time)
    {

        yield return new WaitForSeconds(time);

        GameObject explosion = Instantiate(explosionTel, this.transform.position, this.transform.rotation);
        explosion.GetComponent<Bullet>().ApplyMultiplierToDamage(_damageMultiplier);

        GameObject misil1 = Instantiate(teledirigido, this.transform.position, this.transform.rotation);
        misil1.GetComponent<Bullet>().ApplyMultiplierToDamage(_damageMultiplier);

        GameObject misil2 = Instantiate(teledirigido, this.transform.position, this.transform.rotation);
        misil2.GetComponent<Bullet>().ApplyMultiplierToDamage(_damageMultiplier);

        GameObject misil3 = Instantiate(teledirigido, this.transform.position, this.transform.rotation);
        misil3.GetComponent<Bullet>().ApplyMultiplierToDamage(_damageMultiplier);

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
            bulletInfo.damage = bulletDamage;
            bulletInfo.impactPosition = transform.position;
            collision.gameObject.SendMessage("GetDamage", bulletInfo);
        }
    }

}
