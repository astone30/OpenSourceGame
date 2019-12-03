using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    public Tile[] grounds; //땅 랜덤생성용
    Tile ground;
    public int percent_of_Plain;
    int ground_kind;
    public int x;
    public int y;
    public int z;

    private void Awake()
    {
        ground_kind = Random.Range(0, 10);
        if (grounds.Length > 1)
        {
            if (ground_kind > percent_of_Plain)
            {
                ground = Instantiate(grounds[0]);
            }
            else if (ground_kind <= percent_of_Plain)
            {
                ground = Instantiate(grounds[1]);
            }
        }
        else
        {
            ground = Instantiate(grounds[0]);
        }

        ground.transform.parent = gameObject.transform;
        ground.transform.position += new Vector3(0, 0.1f, 0);
    }
    private void Start()
    {
        ground.x = x;
        ground.y = y;
        ground.z = z;
    }
}
