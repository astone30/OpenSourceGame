using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour //TODO : Make Eventhandler,
{
    public static GameManager instance;

    public List<Tile> tiles;

    public int limitTurn = 30; //전체 턴 수
    public int currentTrun;

    public float timeLimit = 60;

    public float timeforsetPosition;

    public List<Player> players;

    public bool startanimaiotnend = false;

    public enum GamePlayFSM { //GameRule : TurnReady -> StartTrun -> TurnExit -> HalfTurn -> Eventset-> GoToNextTurn
        beforeTurn, 
        whileTurn, 
        gotoNextTurn 
    }

    public GamePlayFSM playFSM;

    private void Awake()
    {
        instance = this;
        timeforsetPosition = 30;
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

    private void TurnFSM()
    {
        switch (playFSM)
        {
            case GamePlayFSM.beforeTurn:
                TurnReady();
                break;
            case GamePlayFSM.whileTurn:
                WhileTurn();
                break;
            case GamePlayFSM.gotoNextTurn:
                StartTurn();
                break;
        }
    }

    private void TurnReady()
    {
        if (currentTrun == 0) //0 turn(at start), setting start Position
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
        if (timeforsetPosition >= 0)
        {
            timeforsetPosition -= Time.deltaTime;
            Debug.Log(timeforsetPosition);
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
        else if (timeforsetPosition < 0) //After settime
        {
            for (int i = 0; i < players.Count; i++) //if player didn't choose starttile, give random tile
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

    private void LocateCharacters()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (!players[i].isCharaterhere)
            {
                GameObject character = Instantiate(players[i].playerCharcter);
                character.transform.position = players[i].charactorPos;
                character.transform.parent = players[i].transform;
                players[i].isCharaterhere = true;
            }
        }
    }

    private void CheckSequence() //After turn, output of player's act
    {

    }


    private void WhileTurn()
    {

    }

    private void StartTurn() //Start Turn
    {

    }

    private void EventHandler() //event
    {

    }
}
