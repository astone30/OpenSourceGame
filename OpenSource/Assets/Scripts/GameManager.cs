using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour //TODO : Make Eventhandler,
{
    public static GameManager instance; //게임 매니저 싱글톤

    public List<Tile> tiles; //게임 시작시 생성되는 타일
    public List<Tile> theywantTheseTiles; // 각턴에 플레이어들이 구매하려하는 타일들

    public int limitTurn = 30; //전체 턴 수
    public int currentTrun;

    public float timeLimit = 60; //턴 시간제한

    public float turnTime = 30;

    public List<Player> players;


    public bool startanimaiotnend = false; //시작 전 애니메이션이 끝났는지

    public enum GamePlayFSM { //GameRule : TurnReady -> StartTrun -> TurnExit -> HalfTurn -> Eventset-> GoToNextTurn
        beforeTurn, 
        whileTurn, 
        afterTurn,
        gotoNextTurn 
    }

    public GamePlayFSM playFSM;

    public enum Event {
        KILLER,
        SERIALKILLER,
        CELABLEAVSHERE,
        FIRE,
        SUPERVIRUS,
        GETMOREPEOPLE,
        ZOMBIEPOPUP,
        TOURIST
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        GridSetter.instance.CreateMap();
        this.currentTrun = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TurnFSM();
    }

    private void TurnFSM()//턴 제어기{준비, 턴, 턴스위친}
    {
        switch (playFSM)
        {
            case GamePlayFSM.beforeTurn:
                TurnReady();
                break;
            case GamePlayFSM.whileTurn:
                WhileTurn();
                break;
            case GamePlayFSM.afterTurn:
                CheckSequence();
                break;
            case GamePlayFSM.gotoNextTurn:
                StartTurn();
                break;
        }
    }

    private void TurnReady()
    {
        if (currentTrun == 0) //0 turn(at start), setting start Position 0턴(맨 시작 턴)
        {
            SetPosition();
        }
        else if(currentTrun > 0)
        {
            playFSM = GamePlayFSM.gotoNextTurn;
        }
    }

    private void SetPosition()
    {
        //float time = 30;
        if (turnTime >= 0)
        {
            turnTime -= Time.deltaTime;
            //Debug.Log(timeforsetPosition);
            if (players.Count != 0)
            {
                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].territory.Count == 1 && players[i].charactorPos == Vector3.zero)
                    {
                        players[i].charactorPos = players[i].territory[0].transform.position; //Set character spawn area
                    }
                }
            }
        }
        else if (turnTime < 0) //After settime
        {
            for (int i = 0; i < players.Count; i++) //if player didn't choose starttile, give random tile 플레이어가 시작 타일을 선택하지 않았다면 아무도 소유하지 않은 타일을 랜덤하게 준다.
            {
                if (players[i].territory.Count < 1)
                {
                    int rantile = Random.Range(0, tiles.Count - 1);
                    if (tiles[rantile].owner == null)
                    {
                        tiles[rantile].owner = players[i];
                        players[i].territory.Add(tiles[rantile]);
                        players[i].charactorPos = players[i].territory[0].transform.position; //Set character spawn area
                    }
                }
            }
            LocateCharacters();
            currentTrun ++;
        }
    }

    private void LocateCharacters() //플레이어들의 케릭터를 위치시킨 후, 플레이어 오브젝트의 차일드 오브젝트로 배정한다.
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (!players[i].isCharaterhere)
            {
                GameObject character = Instantiate(players[i].playerCharcter);
                character.GetComponent<Character>().whoseCharacter = players[i];
                players[i].charcterMoverange = character.GetComponent<Character>().movablerange;
                players[i].characterActionRange = character.GetComponent<Character>().actionRange;
                players[i].actionpoint = character.GetComponent<Character>().actionPoint;
                character.transform.position = players[i].charactorPos;
                character.transform.parent = players[i].transform;
                players[i].WhereIsYourCharacter = players[i].territory[0];
                players[i].isCharaterhere = true;
                players[i].playerCharcteronScreen = character;
            }
        }
    }

    private void CheckSequence() //After turn, output of player's act //다른 부분은 실시간으로 하되, 타일을 사고 팔고, 토지 경매 목적
    {
        if (theywantTheseTiles.Count > 0)
        {
            GivingLand();
            theywantTheseTiles.Clear();
        }
        else if (theywantTheseTiles.Count == 0) //땅 소유자 판정이 끝났을때 턴을 다시 준비한다.
        {
            currentTrun += 1;
            TurnReady();
        }
    }


    private void WhileTurn()//턴 넘기기, 나중에 기능추가
    {
        if (turnTime > 0)
        {
            turnTime -= Time.deltaTime;
        }
        else if (turnTime <= 0)
        {
            playFSM = GamePlayFSM.afterTurn;
            //TurnReady(); 
        }
    }

    private void StartTurn() //Start Turn //시작하기전 이벤트 현재 턴에 나올 이벤트셋 턴 시간 리셋,모든 이벤트는 1턴 이후에 나온다.
    {
        turnTime = 30;
        if (currentTrun == 1)
        {
            playFSM = GamePlayFSM.whileTurn;
        }
        else if (currentTrun > 1)
        {
            EventSetter();
            playFSM = GamePlayFSM.whileTurn;
        }
    }

    private void EventSetter() //event셋
    {
    }

    private void GivingLand()
    {
        for (int i = 0; i < theywantTheseTiles.Count; i++)
        {
            if (theywantTheseTiles[i].WhoWantsToBuy.Count > 1) //구매자가 두명 이상 일 때 
            {
                //경매
            }
            else if (theywantTheseTiles[i].WhoWantsToBuy.Count == 1)
            {
                theywantTheseTiles[i].owner = theywantTheseTiles[i].WhoWantsToBuy[0];
                theywantTheseTiles[i].WhoWantsToBuy[0].money -= theywantTheseTiles[i].Price;
                theywantTheseTiles[i].WhoWantsToBuy.Clear();
            }
        }
    }


}
