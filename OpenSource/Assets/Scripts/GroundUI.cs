using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroundUI : MonoBehaviour
{
    public Tile tile;
    public Player player;

    public Image mugshot;
    public string description;
    public int price;

    public Button buybutton;
    public Button canclebutton;

    public Text pricetxt;
    public Text descriptiontxt;
    public Image mug;


    // Start is called before the first frame update
    void Start()
    {
        buybutton.onClick.AddListener(BuyOrder);
        canclebutton.onClick.AddListener(CancleOrder);

        pricetxt.text = price.ToString();
        descriptiontxt.text = description;

    }

    void BuyOrder() //GAMEMANAGER에 구매요청(구매자가 누구인가, 무엇을 구매하려하는가)
    {
        tile.WhoWantsToBuy.Add(this.GetComponentInParent<Player>()); //구매자 시도자 리스트
        GameManager.instance.theywantTheseTiles.Add(tile);
        Debug.Log("구매요청");
        this.GetComponentInParent<Player>().gUI = false;
        Destroy(this.gameObject);
    }

    void CancleOrder()
    {
        this.GetComponentInParent<Player>().gUI = false;
        Destroy(this.gameObject);
    }
}
