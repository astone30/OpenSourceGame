using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    public Tile hereisEvent; //이벤트가 시작되는 타일들
    public GameObject eventeffect; //이벤트 파티클
    public bool isOnScreen = false;
    public string description;
}
