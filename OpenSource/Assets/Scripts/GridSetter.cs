using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSetter : MonoBehaviour
{
    static GridSetter inst = null; //그리드 싱글톤

    public static GridSetter instance
    {
        get { return inst; }
    }
    public GameObject hex; //육각 그리드
    public GameObject surfacehex; //해안가 타일 생성용
    public GameObject MapBunch; //생성한 맵을 오브젝트"map"에 위치시키기 위해 선언함

    public float hexW;
    public float hexH;

    public int mapSizeX;
    public int mapSizeY;
    public int mapSizeZ;

    public Hex[][][] Map;

    private void Awake()
    {
        inst = this;
        SetHexSize();
    }

    public void SetHexSize() //그리드 사이즈 설정
    {
        hexW = hex.transform.GetComponent<Renderer>().bounds.size.x;
        hexH = hex.transform.GetComponent<Renderer>().bounds.size.z;
    }

    public Vector3 GetWorldPos(int x, int y, int z)
    {
        float X,Z;
        X = x * hexW + (z + hexW + 0.5f);
        Z = (-z) * hexH * 0.75f;
        return new Vector3(X, 0, Z);
    }

    public void CreateMap()
    {
        Map = new Hex[mapSizeX * 2 + 1][][];
        for (int x = -mapSizeX; x <= mapSizeX; x++)
        {
            Map[x+mapSizeX] = new Hex[mapSizeY*2+1][];
            for (int y = -mapSizeY; y <= mapSizeY; y++)
            {
                Map[x+mapSizeX][y+mapSizeY] = new Hex[mapSizeZ * 2 + 1];
                for (int z = -mapSizeZ; z<= mapSizeZ; z++)
                {
                    if (x + y + z == 0)
                    {
                        if(x == -mapSizeX || x == mapSizeX || y == -mapSizeY || y == mapSizeY || z == -mapSizeZ || z == mapSizeZ) //섬 외곽
                        {
                            Map[x + mapSizeX][y + mapSizeY][z + mapSizeZ] = ((GameObject)Instantiate(surfacehex)).GetComponent<Hex>();
                            Vector3 pos = GetWorldPos(x, y, z);
                            Map[x + mapSizeX][y + mapSizeY][z + mapSizeZ].x = x;
                            Map[x + mapSizeX][y + mapSizeY][z + mapSizeZ].y = y;
                            Map[x + mapSizeX][y + mapSizeY][z + mapSizeZ].z = z;
                            Map[x + mapSizeX][y + mapSizeY][z + mapSizeZ].transform.position = pos + new Vector3(0, -0.05f, 0);
                            Map[x + mapSizeX][y + mapSizeY][z + mapSizeZ].transform.parent = MapBunch.transform;
                        }
                        else
                        {
                            Map[x + mapSizeX][y + mapSizeY][z + mapSizeZ] = ((GameObject)Instantiate(hex)).GetComponent<Hex>();
                            Vector3 pos = GetWorldPos(x, y, z);
                            Map[x + mapSizeX][y + mapSizeY][z + mapSizeZ].x = x;
                            Map[x + mapSizeX][y + mapSizeY][z + mapSizeZ].y = y;
                            Map[x + mapSizeX][y + mapSizeY][z + mapSizeZ].z = z;
                            Map[x + mapSizeX][y + mapSizeY][z + mapSizeZ].transform.position = pos;
                            Map[x + mapSizeX][y + mapSizeY][z + mapSizeZ].transform.parent = MapBunch.transform;
                        }
                    }
                }
            }
        }
    }
}
