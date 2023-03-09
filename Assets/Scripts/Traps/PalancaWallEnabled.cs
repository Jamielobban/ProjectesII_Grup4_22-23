using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PalancaWallEnabled : Palanca
{
    public GameObject wall;
    public GameObject ruinas;

    public GameObject a;
    public GameObject b;

    public MovingPlatform plat;
    // Start is called before the first frame update
    void Awake()
    {
        wall.SetActive(true);
        ruinas.SetActive(false);

    }
    public override void Action()
    {
        wall.SetActive(false);
        ruinas.SetActive(true);
        plat.points.Add(a);
        plat.points.Add(b);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
