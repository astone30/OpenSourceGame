using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hospital : Building
{
    bool costmoney = true;
    private void Awake()
    {
        set_time = GameManager.instance.currentTrun;
        price = 1200;
        health = 200;
        cost = 200;
        turn_time = 3;

        isReady = false;
        isActive = false;

        how_damaged = 0;

        name = "병원";
        description = "이곳에서 회복 할 수 있다.";
    }


    private void Update()
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
            ICanHealYou();
            Destroy(dust);
            LoseCost();
        }
    }

    void ICanHealYou()
    {
        if (GameManager.instance.players[0].WhereIsYourCharacter == gameObject.GetComponentInParent<Tile>())
        {
            if (GameManager.instance.players[0].playerCharcteronScreen.GetComponent<Character>().health < 100)
            {
                GameManager.instance.players[0].playerCharcteronScreen.GetComponent<Character>().health = 100;
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
            if (costmoney)
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
            dust.transform.localPosition = new Vector3(0, 2, 0);
        }

        if (gameObject.transform.localPosition.y < 0)
        {
            gameObject.transform.localPosition += Vector3.up * 0.009f;
        }
    }

}
