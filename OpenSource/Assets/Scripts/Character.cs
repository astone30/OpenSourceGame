using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Player whoseCharacter;
    public int actionPoint = 6;
    public int movablerange = 2; //한번에 움직일 수 있는 범위
    public int actionRange = 2; // 움직임을 제외한 케릭터의 행동(땅 구매 요청, 아후 구현할 스킬)의 범위

    public int gotPeople = 0;

    public int health = 100;

}
