using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    bool final;
    public GameObject portal;
    // Start is called before the first frame update
    void Start()
    {
        final = false;
        if (PlayerPrefs.GetInt("Final1", 0) == 1 && PlayerPrefs.GetInt("Final2", 0) == 1)
        {
            final = true;
            portal.SetActive(true);

        }
        else
        {
            portal.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
            if (PlayerPrefs.GetInt("Final1", 0) == 1 && PlayerPrefs.GetInt("Final2", 0) == 1&&!final)
            {
                final = true;
                portal.SetActive(true);

            }
        }
}
