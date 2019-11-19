using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    public GameObject[] grounds;
    GameObject ground;
    int ground_kind;


    private void Awake()
    {
        ground_kind = Random.Range(0, grounds.Length);
        ground = Instantiate(grounds[ground_kind]);
        ground.transform.parent = gameObject.transform;
        ground.transform.position += new Vector3(0, 0.1f, 0);
    }
}
