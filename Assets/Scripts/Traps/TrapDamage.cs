using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    bool check;

    public bool directionUp;

    GameObject[] enemies;

    public bool noEmpujar;
    // Start is called before the first frame update
    void Start()
    {
        check = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Delay(float time)
    {
        yield return new WaitForSeconds(time);
        check = true;

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && check)
        {

            collision.gameObject.SendMessage("GetDamage", 1);
            check = false;
            StartCoroutine(Delay(0.5f));
            if (!noEmpujar)
            {
                if (directionUp)
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(this.transform.up*4000, ForceMode2D.Force);

                }
                else
                {
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(-this.transform.up * 4000, ForceMode2D.Force);

                }
            }
        }
        //else if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    for (int i = 0; i < enemies.Length; i++)
        //    {
        //        if (!GameObject.ReferenceEquals(collision.gameObject, enemies[i]))
        //        {
        //            if (directionUp)
        //            {
        //                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 3000, 0), ForceMode2D.Force);

        //            }
        //            else
        //            {
        //                collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, -3000, 0), ForceMode2D.Force);

        //            }
        //        }
        //    }

        //    if (enemies.Length == 0)
        //    {
        //        if (directionUp)
        //        {
        //            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, 3000, 0), ForceMode2D.Force);

        //        }
        //        else
        //        {
        //            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, -3000, 0), ForceMode2D.Force);

        //        }
        //    }

        //}
    }




}
