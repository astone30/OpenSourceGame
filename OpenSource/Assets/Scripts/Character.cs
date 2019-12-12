using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Player whoseCharacter;
    public GameObject people;
    public GameObject GameOverUI;
    public int actionPoint = 6;
    public int movablerange = 2; //한번에 움직일 수 있는 범위
    public int actionRange = 2; // 움직임을 제외한 케릭터의 행동(땅 구매 요청, 아후 구현할 스킬)의 범위

    public int gotPeople = 0;

    public float health = 100;

    public bool isdead = false;

    private void Update()
    {
        if (gotPeople > 0)
        {
            people.SetActive(true);
        }
        else if (gotPeople == 0)
        {
            people.SetActive(false);
        }


        if (whoseCharacter.WhereIsYourCharacter.rightNow == Tile.NatureEvent.FIRE)
        {
            if (health > 0)
            {
                health -= 0.1f;
            }
        }
        else if (whoseCharacter.WhereIsYourCharacter.rightNow == Tile.NatureEvent.VIRUS)
        {
            if (health > 0)
            {
                health -= 0.02f;
            }
        }

        if (health < 0)
        {
            isdead = true;
        }

        if(isdead)
        {
            GameOverUI.SetActive(true);
        }
    }

}
