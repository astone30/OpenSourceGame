using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killer : Event
{
    bool ismove = false;
    private void Awake()
    {
        EventHandler.instance.wanderingKiller = gameObject;
        int ran = Random.Range(0, GameManager.instance.tiles.Count);
        hereisEvent = GameManager.instance.tiles[ran];
        gameObject.transform.position = GameManager.instance.tiles[ran].transform.position;
    }

    private void Start()
    {
        StartCoroutine(Wandering());
    }

    private void Update()
    {
        if (hereisEvent == GameManager.instance.players[0].WhereIsYourCharacter)
        {
            Destroy(GameManager.instance.players[0]);
        }
        if (hereisEvent.rightNow != Tile.NatureEvent.NONE)
        {
            EventHandler.instance.wanderingKiller = null;
            Destroy(gameObject);
        }
    }

    IEnumerator Wandering()
    {
        while (GameManager.instance.playFSM == GameManager.GamePlayFSM.whileTurn)
        {
            yield return new WaitForSeconds(10f);
            int random = Random.Range(0, hereisEvent.neighborTIle.Count - 1);
            gameObject.transform.position = hereisEvent.neighborTIle[random].transform.position;
            hereisEvent = hereisEvent.neighborTIle[random];
            if (hereisEvent.construct != null)
            {
                if (hereisEvent.construct.GetComponent<Villa>())
                {
                    hereisEvent.construct.GetComponent<Villa>().populaiton -= 2;
                }
            }
        }
    }

}
