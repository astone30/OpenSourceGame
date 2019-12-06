﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villa : Building
{
    public int populaiton = 0; //인구최대 10까지?

    bool gotmoney = false;
    bool costmoney = true;

    float speed = 0.5f;

    private void Awake()
    {
        set_time = GameManager.instance.currentTrun;
        price = 2500;
        health = 110;
        profit = 40; //인구 * 수익
        cost = 100;
        turn_time = 2;

        isReady = false;
        isActive = false;

        how_damaged = 0;

        name = "거주지";
        description = "주민들이 이곳에서 거주한다.";

    }

    // Update is called once per frame
    void Update()
    {
        if (!isReady) // 건물이 완공되지 않았을 때
        {
            OnBuild(); //set build animation
            if (GameManager.instance.currentTrun == set_time + turn_time)
            {
                isReady = true; // 준비 완료
                isActive = true; // 활성화
            }
        }
        else if (isReady)
        {
            Destroy(dust);
            GetProfit();
            LoseCost();
        }
    }

    void GetProfit() //이익
    {
        if (isActive)
        {
            if (GameManager.instance.playFSM == GameManager.GamePlayFSM.afterTurn)
            {
                if (!gotmoney)
                {
                    gameObject.GetComponentInParent<Tile>().owner.money += profit * populaiton;
                    gotmoney = true;
                }
            }
            else if (GameManager.instance.playFSM == GameManager.GamePlayFSM.whileTurn)
            {
                if (gotmoney)
                {
                    gotmoney = false;
                }
            }
        }

    }

    void LoseCost() //유지비
    {
        if (GameManager.instance.playFSM == GameManager.GamePlayFSM.afterTurn)
        {
            if (!costmoney)
            {
                gameObject.GetComponentInParent<Tile>().owner.money -= cost;
                costmoney = true;
            }
        }
        else if (GameManager.instance.playFSM == GameManager.GamePlayFSM.whileTurn)
        {
            if(costmoney)
            {
                costmoney = false;
            }
        }
    }

    void OnBuild()
    {
        if (dust == null && !isReady)
        {
            dust = Instantiate(buildingParticle);
            dust.transform.SetParent(this.gameObject.transform.parent);
            dust.transform.localScale = new Vector3(2, 0, 2);
            dust.transform.localPosition = new Vector3(0,2,0);
        }

        if (gameObject.transform.localPosition.y < 0)
        {
            gameObject.transform.localPosition += Vector3.up * 0.009f;
        }
    }
}