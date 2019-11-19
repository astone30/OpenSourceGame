using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player: MonoBehaviour // TODO : Make PlayerCharacter, give character setting
{
    public int money; //가진 돈

    public GameObject playerCharcter;//Player Character

    public Color PlayerColor;
    public Color backtocolor;

    public Vector3 charactorPos; //Player Character Position
    public Vector3 currentMousePos; //Cursor Postition

    public List<Tile> territory; //Player's territory(own land)

    public GameObject cursorlookingObject;

    public bool isCharaterhere = false;

    private void Start()
    {
        money = 10000;
        GameManager.instance.players.Add(this);
    }
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            currentMousePos = hit.point;
            if (hit.collider.gameObject.GetComponent<Tile>() && cursorlookingObject == null) //
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
        if (GameManager.instance.currentTrun == 0 && this.territory.Count <1)
        {
            SetStartPositon();
        }
    }

    private void SetStartPositon()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
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
        }
    }
}
