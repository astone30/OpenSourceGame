﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuildUI2 : MonoBehaviour //건물 건설을 위한 UI
{
    public Tile hereHere;
    public Building tobuild;

    public Button build;
    public Button cancle;

    public Text price;
    public Text description;

    public Sprite mug;

    public int pricenum;
    public string Description;

    // Start is called before the first frame update
    void Start()
    {
        cancle.onClick.AddListener(Nevermind);
        build.onClick.AddListener(Build);
    }

    // Update is called once per frame
    void Update()
    {
        if (price.text != pricenum.ToString())
        {
            price.text = "Price : " + pricenum.ToString("0000");
        }
        if (description.text != Description)
        {
            description.text = Description;
        }
    }

    void Build()
    {
        if (this.gameObject.GetComponentInParent<Player>().money >= pricenum)//건설
        {
            if (hereHere.Kind_Of_This != Tile.TileKind.MOUNTAIN)
            {
                this.gameObject.GetComponentInParent<Player>().money -= pricenum;
                hereHere.Price += 500;
                hereHere.construct = Instantiate(tobuild);
                hereHere.construct.transform.parent = hereHere.gameObject.transform;
                hereHere.construct.transform.localPosition = new Vector3(0, -10, 0);
            }
            else if (hereHere.Kind_Of_This == Tile.TileKind.MOUNTAIN)
            {
                Debug.Log("산에는 건설 불가!!");
            }
        }
        else if (this.gameObject.GetComponentInParent<Player>().money < pricenum)
        {
            Debug.Log("자금 부족!!");
        }
        this.gameObject.GetComponentInParent<Player>().bUI = false;
        Destroy(this.gameObject);
    }

    void Nevermind()
    {
        Destroy(this.gameObject);
    }
}
