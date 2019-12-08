using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleComing : Event
{
    bool ismove = false;
    private void Awake()
    {
        EventHandler.instance.wanderingpeople.Add(gameObject);
        int ran = Random.Range(0, GameManager.instance.tiles.Count);
        hereisEvent = GameManager.instance.tiles[ran];
        gameObject.transform.position = GameManager.instance.tiles[ran].transform.position;
    }

    private void Start()
    {
        StartCoroutine(Wandering());
    }

    IEnumerator Wandering()
    {
        while (GameManager.instance.playFSM == GameManager.GamePlayFSM.whileTurn)
        {
            yield return new WaitForSeconds(5f);
            int random = Random.Range(0, hereisEvent.neighborTIle.Count - 1);
            gameObject.transform.position = hereisEvent.neighborTIle[random].transform.position;
            hereisEvent = hereisEvent.neighborTIle[random];
            if (hereisEvent.construct != null)
            {
                if (hereisEvent.construct.GetComponent<Villa>())
                {
                    hereisEvent.construct.GetComponent<Villa>().populaiton += 5;
                    Destroy(gameObject);
                }
            }
        }
    }
}
