using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public int x;
    public int y;
    public int z;

    public GameObject[] points = new GameObject[6]; //핵사곤 타일의 꼭지점 좌표 받기용
    public GameObject for_vertices; //빈 게임오브젝트 클래스에 등록하기용
    public Building construct;  //타일 위에있는 건축물(추후 스크립트 추가한다)

    public List <Tile> neighborTIle;

    public LineRenderer line; //라인 랜더러 타일 위에 랜더링 선 생성

    public int Price;

    public enum TileKind{BEACH, GRASS, MOUNTAIN, PAVED} //타일의 종류판별
    public TileKind Kind_Of_This; //타일 종류 스위쳐

    public enum NatureEvent {NONE, FIRE, VIRUS }
    public NatureEvent rightNow;

    public Player owner; //소유한 플레이어 식별용
    public List<Player> WhoWantsToBuy;

    public float radius = 1.5f; //타일 위 라인을 그릴기 위해 사용
    public float lineh; // 라인이 위치할 높이 설정

    public string Description = "타일 설명"; //타일 설명

    public Color neutralColor = Color.black;
    public Color tileColor;

    public bool buyable = true; // 타일을 둘러싼 이웃타일들이 모두 같은 플레이어의 소유면 구매신청 불가
    public bool mountainChange = false;
    public Material pave; //콘크리트
    public Tile tileforpave;

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
        line.material.color = neutralColor;
        tileColor = gameObject.GetComponent<Renderer>().material.color;
    }
    public void Update()
    {
        if (mountainChange)
        {
            MountainTileChange();
        }
        if (this.neighborTIle.Count == 0) //이웃한 타일들 정보 불러오기
        {
            for (int i = 0; i < GameManager.instance.tiles.Count; i++)
            {
                if (GetDIstance(this, GameManager.instance.tiles[i]) == 1)
                {
                    neighborTIle.Add(GameManager.instance.tiles[i]);
                }
            }
        }
        for (int i = 0; i < points.Length; i++) //todo: 수정할것 메서드로 만들기
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

        if (this.owner != null && !this.owner.territory.Contains(this))
        {
            owner.territory.Add(this);
        }
    }


    private int GetDIstance(Tile currentPos, Tile neighbor)
    {
        return ((Mathf.Abs(currentPos.x - neighbor.x)) + (Mathf.Abs(currentPos.y - neighbor.y)) + (Mathf.Abs(currentPos.z - neighbor.z))) / 2;
    }

    private void OnDrawGizmos() //기즈모는 없다고 생각해도됩니다.(시각적인 요소 디버그용이라고 보시면되요  )
    {
        for (int i = 0; i < points.Length; i++)
        {
            Gizmos.DrawSphere(points[i].transform.position, 0.1f);
        }
    }

    private void MountainTileChange()
    {
        gameObject.GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        gameObject.GetComponent<Transform>().localPosition = new Vector3(0, 1.502358f, 0);
        for (int i = 0; i < gameObject.GetComponent<Renderer>().materials.Length; i++)
        {
            gameObject.GetComponent<Renderer>().materials[i] = null;
        }
        gameObject.GetComponent<Renderer>().material.color = pave.color;

        Mesh mesh = tileforpave.GetComponent<MeshFilter>().sharedMesh;
        Mesh mesh2 = Instantiate(mesh);
        gameObject.GetComponent<MeshFilter>().sharedMesh = mesh2;

        tileColor = pave.color;

        Kind_Of_This = TileKind.PAVED;
        Price *= 2;
        Description = "포장된 지역, 더 많은 건물들을 건설 할 수있다.";

        lineh = 2;
        for (int i = 0; i < points.Length; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * (60 * i)) * radius;
            float z = Mathf.Cos(Mathf.Deg2Rad * (60 * i)) * radius;
            points[i] = Instantiate(for_vertices);
            points[i].transform.parent = gameObject.transform;
            points[i].transform.localPosition = new Vector3(x, lineh, z);
        }

        mountainChange = false;
    }
}
