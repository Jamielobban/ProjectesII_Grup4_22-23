using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePathManager : MonoBehaviour
{
    [SerializeField]
    GameObject slimeEnemy;
    [SerializeField]
    BoxCollider2D[] coliders;
    Vector2[] numPerColider = new Vector2[11];    

    // 1 - 9 a 15
    // 2 - 7 a 11
    // 3 - 4 a 7
    // 4 - 6 a 10
    // 5 - 8 a 13
    // 6 - 6 a 9
    // 7 - 2 a 4
    // 8 - 8 a 13
    // 9 - 4 a 6
    // 10 - 2 a 3
    // 11 - 4 a 6

    void Start()
    {
        numPerColider[0] = new Vector2(5, 10);
        numPerColider[1] = new Vector2(3, 6);
        numPerColider[2] = new Vector2(3, 5);
        numPerColider[3] = new Vector2(3, 7);
        numPerColider[4] = new Vector2(5, 9);
        numPerColider[5] = new Vector2(3, 6);
        numPerColider[6] = new Vector2(2, 4);
        numPerColider[7] = new Vector2(5, 9);
        numPerColider[8] = new Vector2(4, 6);
        numPerColider[9] = new Vector2(2, 3);
        numPerColider[10] = new Vector2(4, 6);

        for(int i = 0; i < 11; i++)
        {
            int number = Random.Range((int)(numPerColider[i].x), (int)(numPerColider[i].y + 1));
            for(int j = 0; j < number; j++)
            {
                GameObject slime = Instantiate(slimeEnemy, RandomPointInBounds(coliders[i].bounds), Quaternion.identity);
                slime.GetComponent<Enemy12>().pathManager = this;
                slime.GetComponent<Enemy12>().zoneID = i;
                slime.GetComponent<Enemy12>().actualDestination = RandomPointInBounds(coliders[i].bounds);
                
            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }

    Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            0
        );
    }

    public Vector3 GetNewDestiny(int id)
    {
        return RandomPointInBounds(coliders[id].bounds);
    }
}
