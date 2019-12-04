using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructUI : MonoBehaviour
{
    public Tile buildTothis;//이곳에 건물 건설

    public Button cancleButton;

    public Button pavingTile;//지면 포장
    public Button buildVilla;//빌라
    public Button buildStore;//상가
    public Button buildFireStation;//소방서
    public Button buildPoliceStation;//경찰서
    public Button buildSchool; //학교


    public GameObject buildUIprefab;
    public GameObject buildUI;

    public void Start()
    {
        cancleButton.onClick.AddListener(CancleOrder);
        pavingTile.onClick.AddListener(PavingTile);
    }

    void CancleOrder()
    {
        this.GetComponentInParent<Player>().bUI = false;
        Destroy(this.gameObject);
    }

    void PavingTile()
    {
        buildUIprefab = Instantiate(buildUI);
    }
}
