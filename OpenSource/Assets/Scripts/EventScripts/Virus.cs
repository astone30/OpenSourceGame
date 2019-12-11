using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : Event
{
    int popTurn;
    public List<GameObject> neigborvirus;
    private void Awake()
    {
        popTurn = GameManager.instance.currentTrun;
        int ran = Random.Range(0, GameManager.instance.tiles.Count);
        hereisEvent = GameManager.instance.tiles[ran];
        gameObject.transform.position = hereisEvent.transform.position;
    }
    private void Start()
    {
        hereisEvent.rightNow = Tile.NatureEvent.VIRUS;
        for (int i = 0; i < hereisEvent.neighborTIle.Count; i++)
        {
            hereisEvent.neighborTIle[i].rightNow = Tile.NatureEvent.VIRUS;
            neigborvirus.Add(Instantiate(eventeffect, hereisEvent.neighborTIle[i].transform));
        }
    }

    private void Update()
    {
        if (GameManager.instance.currentTrun < popTurn + 2)
        {
            for (int i = 0; i < hereisEvent.neighborTIle.Count; i++)
            {
                if (hereisEvent.neighborTIle[i].rightNow != Tile.NatureEvent.VIRUS)
                {
                    Destroy(neigborvirus[i]);
                }
            }
        }
        else if (GameManager.instance.currentTrun == popTurn + 2)
        {
            hereisEvent.rightNow = Tile.NatureEvent.NONE;
            for (int i = 0; i < hereisEvent.neighborTIle.Count; i++)
            {
                hereisEvent.neighborTIle[i].rightNow = Tile.NatureEvent.NONE;
                Destroy(neigborvirus[i]);
            }
            Destroy(gameObject);
        }
    }

}
