using UnityEngine;

public class Building : MonoBehaviour
{
    public Sprite image;

    public int price; //건물을 짓는데 드는 가격
    public int profit;//수익
    public int cost; //유지비
    public int turn_time; //완성되기까지 걸리는 턴
    public int set_time; //건물이 지어진 턴

    public bool isActive; //현재 활성화 중인지
    public bool isReady; //완공 되었는지

    public float health; //건물의 채력
    public float how_damaged; //데미지를 받은 정도 

    public string description; //설명UI창을 위해 두었다.
    public string name; //이름 UI창을 위해 두었다.

    public GameObject buildingParticle;
    public GameObject dust;
}
