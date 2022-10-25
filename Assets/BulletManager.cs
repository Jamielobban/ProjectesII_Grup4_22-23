using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject bulletToDestroy;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(bulletToDestroy);
    }
}
