using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player: MonoBehaviour // TODO : Make PlayerCharacter, give character setting
{
    public int money; //가진 돈
    public int charcterMoverange; //한번에 이동가능한 범위
    public int characterActionRange; //캐릭터 행동가능범위
    public int actionpoint;

    public GameObject playerCharcter;//Player Character
    public GameObject playerCharcteronScreen;//Player Character  

    public GameObject groundUI;
    public GameObject groundUIprefab;
    public GameObject ConstructUI;
    public GameObject ConstructUIprefab;

    public Color PlayerColor; //플레이어의 색상 
    public Color backtocolor; // 타일 라인 색갈을 실시간으로 바꿔준다

    public Vector3 charactorPos; //Player Character Position
    public Vector3 currentMousePos; //Cursor Postition 마우스 커서 위치

    public List<Tile> territory; //Player's territory(own land) 플레이어가 소유한 땅
    public List<Tile> path;
    public Tile WhereIsYourCharacter;

    public GameObject cursorlookingObject; // 플레이어의 마우스커서가 현재 가리키는 오브젝트가 뭔지 판별하기위해 사용
    public GameObject selectedObject;

    public bool isCharacterSelected = false; //캐릭터 선택 했을 때
    public bool isCharaterhere = false; //캐릭터가 있는가 없는가
    public bool gUI = false;
    public bool bUI = false;

    public GameObject statealarm;


    private void Start()
    {
        money = 10000;
        GameManager.instance.players.Add(this);
    }
    private void Update()
    {
        if (!isCharacterSelected) //플레이어의 케릭터가 선택되지 않았을 경우
        {
            AtNormal();
        }
        if (GameManager.instance.currentTrun == 0) //시작 지점 세팅용
        {
            if (this.territory.Count== 0)
            {
                SetStartPositon();
            }
        }
        if (GameManager.instance.currentTrun > 0 && GameManager.instance.playFSM == GameManager.GamePlayFSM.whileTurn)  //턴이 진행중일때의 상태를 표시한다.
        {
            WhileTurn();
        }
        if (isCharacterSelected) //플래이어의 케릭터가 선택되었을경우
        {
            CHaracterControl();
        }
    }

    private void SetStartPositon()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                currentMousePos = hit.point;
                if (hit.collider.gameObject.GetComponent<Tile>())
                {
                    if (hit.collider.gameObject.GetComponent<Tile>().owner == null && Input.GetMouseButton(0))
                    {
                        this.territory.Add(hit.collider.gameObject.GetComponent<Tile>());
                        hit.collider.gameObject.GetComponent<Tile>().owner = this;
                    }
                }
                else
                {
                    Debug.Log("do nothing");
                }
            }
        }
    }

    private void WhileTurn()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (Input.GetMouseButtonDown(0) && !isCharacterSelected) //캐릭터 선택이 되지 않았을때의 조작
                {
                    selectedObject = hit.collider.gameObject;
                    if (hit.collider.gameObject != selectedObject)
                    {
                        selectedObject = hit.collider.gameObject;
                    }
                    Controller(selectedObject);
                }
                else if (Input.GetMouseButtonDown(0) && isCharacterSelected) //캐릭터 선택이 되었을 때의 조작
                {
                    selectedObject = hit.collider.gameObject;
                    if (hit.collider.gameObject != selectedObject)
                    {
                        selectedObject = hit.collider.gameObject;
                    }
                }
                else if (Input.GetMouseButtonDown(1) && isCharacterSelected)
                {
                    isCharacterSelected = false;
                    UnShowMovableRange(WhereIsYourCharacter);
                }
            }
        }
    }

    private void AtNormal()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                currentMousePos = hit.point;
                if (hit.collider.gameObject.GetComponent<Tile>() && cursorlookingObject == null)
                {
                    cursorlookingObject = hit.collider.gameObject;
                    backtocolor = cursorlookingObject.GetComponent<Tile>().line.material.color;
                    hit.collider.gameObject.GetComponent<Tile>().line.material.color = Color.white;
                }
                else if (hit.collider.gameObject.GetComponent<Tile>() && cursorlookingObject != null)
                {
                    if (hit.collider.gameObject != cursorlookingObject)
                    {
                        cursorlookingObject.GetComponent<Tile>().line.material.color = backtocolor;
                        cursorlookingObject = null;
                    }
                }
            }
        }
    }

    private void CHaracterControl() //케릭터 선택시 주변에 갈 수 있는 범위 구하기
    {
        if (selectedObject.GetComponent<Tile>())
        {
            if (GetDIstance(WhereIsYourCharacter, selectedObject.GetComponent<Tile>()) <= charcterMoverange)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("이동");
                    UnShowMovableRange(WhereIsYourCharacter);
                    SetPath(WhereIsYourCharacter, selectedObject.GetComponent<Tile>());
                    //MoveTIleByTile(selectedObject.GetComponent<Tile>());
                   StartCoroutine(Delay(selectedObject.GetComponent<Tile>()));
                }
            }
            else if (GetDIstance(WhereIsYourCharacter, selectedObject.GetComponent<Tile>()) > charcterMoverange)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("이동불가");
                    isCharacterSelected = false;
                    UnShowMovableRange(WhereIsYourCharacter);
                }
            }
        }   
    }
    private void Controller(GameObject selected) //캐릭터 조작을 제외한 다른 행동들 
    {
        if (selected.tag == "Ground") //그라운드 태그
        {
            if (selected.GetComponent<Tile>().owner == this)
            {
                if (!bUI)
                {
                    if (gUI)
                    {
                        gUI = false;
                    }
                    Debug.Log("내땅"); // UI 조작 창(건설 Ui)
                    bUI = true;
                    ConstructUIprefab = Instantiate(ConstructUI);
                    ConstructUIprefab.GetComponent<ConstructUI>().buildTothis = selected.GetComponent<Tile>(); //타일 정보를 ui에 담는다.
                    ConstructUIprefab.GetComponent<ConstructUI>().transform.SetParent(this.gameObject.transform);
                }
            }
            else
            {
                if (GetDIstance(WhereIsYourCharacter, selected.GetComponent<Tile>()) <= characterActionRange)
                {
                    if (!gUI)
                    {
                        if (bUI)
                        {
                            bUI = false;
                        }
                        gUI = true;
                        groundUIprefab = Instantiate(groundUI);
                        groundUIprefab.GetComponent<GroundUI>().tile = selected.GetComponent<Tile>();
                        groundUIprefab.GetComponent<GroundUI>().price = selected.GetComponent<Tile>().Price;
                        groundUIprefab.GetComponent<GroundUI>().description = selected.GetComponent<Tile>().Description;
                        groundUIprefab.GetComponent<GroundUI>().transform.SetParent(this.gameObject.transform);
                    }
                } 
            }
        }
        else if (selected.tag == "Character") //케릭터 태그
        {
            if (selected.GetComponent<Character>().whoseCharacter == this)
            {
                isCharacterSelected = true;
                Debug.Log("내캐릭터"); //캐릭터 선택
                ShowMoveableRange(WhereIsYourCharacter);
            }
            else
            {
                Debug.Log("캐릭터"); //타 플레이어 정보UI
            }
        }
    }

    private int GetDIstance(Tile currentPos, Tile Cursor) //타일 거리 구하기(땅구매범위, 케릭터 이동 목적)
    {
        return ((Mathf.Abs(currentPos.x - Cursor.x)) + (Mathf.Abs(currentPos.y - Cursor.y)) + (Mathf.Abs(currentPos.z - Cursor.z)))/2;
    }

    #region Move
    private void UnShowMovableRange(Tile pos) //범위 색상 초기화
    {
        foreach (Tile tile in GameManager.instance.tiles)
        {
            if ((tile.x <= pos.x + charcterMoverange && tile.x >= pos.x - charcterMoverange) && (tile.y <= pos.y + charcterMoverange && tile.y >= pos.y - charcterMoverange) && (tile.z <= pos.z + charcterMoverange && tile.z >= pos.z - charcterMoverange))
            {
                if (tile.Kind_Of_This != Tile.TileKind.MOUNTAIN) //모든 플레이어들은 산타일은 오브젝트 색상변경에서 제외한다.
                {
                    tile.GetComponent<Renderer>().material.color = tile.tileColor;
                }
            }
        }
    }

    private void ShowMoveableRange(Tile pos) //움직일수 있는 범위 표시
    {
        foreach (Tile tile in GameManager.instance.tiles)
        {
            if ((tile.x <= pos.x + charcterMoverange && tile.x >= pos.x - charcterMoverange) && (tile.y <= pos.y + charcterMoverange && tile.y >= pos.y - charcterMoverange) && (tile.z <= pos.z + charcterMoverange && tile.z >= pos.z - charcterMoverange))
            {
                if (tile.Kind_Of_This != Tile.TileKind.MOUNTAIN) //모든 플레이어들은 산타일은 이동범위에서 제외한다.
                {
                    tile.GetComponent<Renderer>().material.color = Color.green;
                }
            }
        }
    }

    private void SetPath(Tile pathfind, Tile destin) //길찾기, 목적지 (유사 A*)
    {
        int distance = GetDIstance(pathfind, destin);
        if (GetDIstance(pathfind, destin) != 0) //거리가 0(목적지에 도착하지 않았을때) do recursives
        {
            Tile t = pathfind;
            foreach (Tile tile in pathfind.neighborTIle)
            {
                if (GetDIstance(tile, destin) < distance) //모든 이웃과 타일의 거리는 1로 동등하기 때문
                {
                    path.Add(tile);
                    t = tile;
                    break;
                }
            }
            SetPath(t, destin);
        }
    }

    #endregion
    
    IEnumerator Delay(Tile destin) //TODO : 액션포인트 소모, 포물선운동 추가
    {
        int i = 0;
        while (WhereIsYourCharacter != destin)
        {
            if (actionpoint > 0)
            {
                yield return new WaitForSeconds(1f);
                playerCharcteronScreen.transform.position = path[i].transform.position;
                WhereIsYourCharacter = path[i];
                i++;
                actionpoint--;
            }
            else
            {
                break;
            }
        }
        if (WhereIsYourCharacter == destin || actionpoint == 0)
        {
            path.Clear();
            isCharacterSelected = false;
            if (actionpoint == 0)
            { Debug.Log("이동 불가"); }
        }
        
    }
}

