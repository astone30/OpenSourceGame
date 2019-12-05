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
        Debug.Log("구매요청");
        if (this.GetComponentInParent<Player>().money >= tile.Price && this.GetComponentInParent<Player>().actionpoint >0)
        {
            if (this.GetComponentInParent<Player>().actionpoint > 0)
            {
                if (!tile.WhoWantsToBuy.Contains(this.GetComponentInParent<Player>()))
                {
                    tile.WhoWantsToBuy.Add(this.GetComponentInParent<Player>()); //구매자 시도자 리스트
                }
                GameManager.instance.theywantTheseTiles.Add(tile);
                this.GetComponentInParent<Player>().actionpoint--;
                this.GetComponentInParent<Player>().gUI = false;
                Destroy(this.gameObject);
            }
        }
        else if (this.GetComponentInParent<Player>().money < tile.Price || this.GetComponentInParent<Player>().actionpoint <= 0)
        {
            this.GetComponentInParent<Player>().gUI = false;
            Destroy(this.gameObject);
            Debug.Log("구매 불가!! 자금이 부족합니다.");//todo : 팝업창
            if (this.GetComponentInParent<Player>().actionpoint <= 0)
            {
                Debug.Log("지금은 구매 할 수 없어");
            }
        }
    }

    void CancleOrder()
    {
        this.GetComponentInParent<Player>().gUI = false;
        Destroy(this.gameObject);
    }
}
