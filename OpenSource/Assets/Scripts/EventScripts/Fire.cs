using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Event
{
    int popTurn;
    public List<GameObject> OnFire;
    public bool firespread = false;
    public List<Tile> everyfirepath;
    public Tile firepath;
    private void Awake()
    {
        popTurn = GameManager.instance.currentTrun;
    }
    private void Update()
    {
        if (GameManager.instance.currentTrun < popTurn + 4)
        {
            if (hereisEvent == null)
            {
                FindMountainTile();
            }
            else if (hereisEvent != null)
            {
                if (GameManager.instance.playFSM == GameManager.GamePlayFSM.whileTurn)
                {
                    if (firepath != null)
                    {
                        if (!firespread)
                        {
                            FireGoes(firepath);
                        }
                    }
                }
                else if (GameManager.instance.playFSM == GameManager.GamePlayFSM.afterTurn)
                {
                    firespread = false;
                }
            }
            for (int i = 0; i < everyfirepath.Count; i++)
            {
                if (everyfirepath[i].rightNow == Tile.NatureEvent.NONE)
                {
                    Destroy(OnFire[i]);
                    everyfirepath.Remove(everyfirepath[i]);
                }
            }
        }
        else if (GameManager.instance.currentTrun == popTurn + 4)
        {
            for (int i = 0; i < OnFire.Count; i++)
            {
                Destroy(OnFire[i]);
            }
            for (int i = 0; i < everyfirepath.Count; i++)
            {
                everyfirepath[i].rightNow = Tile.NatureEvent.NONE;
            }
            Destroy(gameObject);
        }
    }

    void FindMountainTile()
    {
        while (hereisEvent == null)
        {
            int ran = Random.Range(0, GameManager.instance.tiles.Count);
            if (GameManager.instance.tiles[ran].Kind_Of_This == Tile.TileKind.MOUNTAIN)
            {
                hereisEvent = GameManager.instance.tiles[ran];
                hereisEvent.rightNow = Tile.NatureEvent.FIRE;
                everyfirepath.Add(hereisEvent);
                OnFire.Add(eventeffect);
                firepath = hereisEvent; //진원지
                gameObject.transform.position = hereisEvent.transform.position;
                break;
            }
        }
    }

    void FireGoes(Tile path)
    {
        
            for (int i = 0; i < path.neighborTIle.Count; i++)
            {
                if (path.neighborTIle[i].Kind_Of_This == Tile.TileKind.MOUNTAIN)
                {
                    path.neighborTIle[i].rightNow = Tile.NatureEvent.FIRE;
                    everyfirepath.Add(path.neighborTIle[i]);
                    firepath = path.neighborTIle[i];
                    OnFire.Add(Instantiate(eventeffect, path.neighborTIle[i].transform));
                    firespread = true;
                    break;
                }
            }
    }
}
