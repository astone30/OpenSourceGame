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

    public Building villa;
    public Building store;
    public Building filestation;
    public Building policestation;
    public Building school;



    public GameObject buildUIprefab;
    public GameObject buildUI;

    public GameObject buildUI2prefab;
    public GameObject buildUI2;

    public Sprite[] mugimages; //이미지들

    public void Start()
    {
        cancleButton.onClick.AddListener(CancleOrder);
        pavingTile.onClick.AddListener(PavingTile);
        buildVilla.onClick.AddListener(BuildVilla);
    }

    public void Update()
    {
        TileJudger();
        if (!this.GetComponentInParent<Player>().bUI)
        {
            Destroy(this.gameObject);
        }
    }

    void TileJudger()
    {
    }


    void CancleOrder()
    {
        this.GetComponentInParent<Player>().bUI = false;
        Destroy(this.gameObject);
    }

    void PavingTile()
    {
        buildUIprefab = Instantiate(buildUI);
        buildUIprefab.GetComponent<BuildConfirmUI>().hereHere = buildTothis;
        buildUIprefab.transform.SetParent(this.GetComponentInParent<Player>().transform);
        buildUIprefab.GetComponent<BuildConfirmUI>().Description = "땅을 포장한다.";
        switch (buildTothis.Kind_Of_This)
        {
            case Tile.TileKind.MOUNTAIN:
                buildUIprefab.GetComponent<BuildConfirmUI>().pricenum = 1800;
                buildUIprefab.GetComponent<BuildConfirmUI>().Description += "산지를 개간 하려면 추가비용이 든다.";
                break;
            case Tile.TileKind.GRASS: // 타일 종류가 평지이면 포장 가격 평균
                buildUIprefab.GetComponent<BuildConfirmUI>().pricenum = 1500;
                break;
            case Tile.TileKind.BEACH: // 타일 종류가 해변 가일 경우
                buildUIprefab.GetComponent<BuildConfirmUI>().pricenum = 1200;
                buildUIprefab.GetComponent<BuildConfirmUI>().Description += "해안가에 건물을 건설 하겠다고? 어떻게 되도 모른다??";
                break;
        }
    }

    void BuildVilla()
    {
        buildUI2prefab = Instantiate(buildUI2);
        buildUI2prefab.GetComponent<BuildUI2>().hereHere = buildTothis;
        buildUI2prefab.transform.SetParent(this.GetComponentInParent<Player>().transform);
        buildUI2prefab.GetComponent<BuildUI2>().pricenum = villa.price;
        buildUI2prefab.GetComponent<BuildUI2>().Description = villa.description;
        buildUI2prefab.GetComponent<BuildUI2>().tobuild = villa;
    }
}
