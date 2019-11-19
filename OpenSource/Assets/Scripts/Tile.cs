using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject[] points = new GameObject[6];
    public GameObject for_vertices;

    public LineRenderer line;

    public int Price;

    public enum TileKind{BEACH, GRASS, MOUNTAIN, PAVED}
    public TileKind Kind_Of_This;

    public Player owner;

    public float radius = 1.5f;
    public float lineh;

    public string Description = "타일 설명";

    public void Awake()
    {
        GameManager.instance.tiles.Add(this);
        for (int i = 0; i < points.Length; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad*(60 * i))*radius;
            float z = Mathf.Cos(Mathf.Deg2Rad*(60 * i))*radius;
            points[i] = Instantiate(for_vertices);
            points[i].transform.parent = gameObject.transform;
            points[i].transform.localPosition = new Vector3(x, lineh, z);
        }
        line.positionCount = points.Length+1;
    }
    public void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            line.SetPosition(i,points[i].transform.position);
            if (i == points.Length - 1)
            {
                line.SetPosition(i+1, points[0].transform.position);
                if (owner != null)
                {
                    line.material.color = owner.PlayerColor;
                }
            }
        }
    }


    private void OnDrawGizmos()
    {
        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.DrawSphere(points[i].transform.position, 0.1f);
        }
    }

}
