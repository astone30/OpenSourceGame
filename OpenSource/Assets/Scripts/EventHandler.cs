using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{

    public static EventHandler instance;

    public GameObject newspaper;

    public List<int> setevent;
    public List<Sprite> eventimages; //후에 추가

    public int eventnum;

    public enum Act { NOTHING, SHOW, READY } //이벤트 발생시키기, 준비하기

    public Act act;

    bool set = false;
    bool newspop = false;

    public enum Event
    {
        PEOPLECOMING,
        KILLER,
        CELABLEAVSHERE,
        FIRE,
        DISEASE,
        FLOAD,
    }

    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        EventSwitcher();
    }

    void EventSwitcher()
    {
        switch (act)
        {
            case Act.NOTHING:
                break;
            case Act.READY:
                SetEvent();
                break;
            case Act.SHOW:
                ShowingEvent();
                break;
        }
    }

    void SetEvent()
    {
        if (!set)
        {
            for (int i = 0; i < eventnum; i++)
            {
                setevent.Add(Random.Range(0, 5));
            }
            set = true;
            GameManager.instance.allEventSett = set;
        }
    }

    void ShowingEvent()
    {
        for (int i = 0; i < setevent.Count; i++)
        {
            ActivateEvent((Event)setevent[i]);
        }
    }

    void ActivateEvent(Event tt)
    {
        switch (tt)
        {
            case Event.PEOPLECOMING:
                GetMorePeople();
                break;
            case Event.KILLER:
                KillerPOP();
                break;
            case Event.CELABLEAVSHERE:
                CelabisComming();
                break;
            case Event.FIRE:
                FirePOP();
                break;
            case Event.DISEASE:
                DiseasePOP();
                break;
            case Event.FLOAD:
                FloadPOP();
                break;

        }
    }

    void GetMorePeople()
    {
        string content = "외딴섬, 거주민 유입...";
        ShowNewspaper(content);
    }

    void KillerPOP()
    {
        string content = "외딴 섬에서 살인사건 발생, 섬 주민들 공포에 떨다.";
        ShowNewspaper(content);
    }

    void CelabisComming()
    {
        string content = "유명 가수 ㅁㅁ, 외딴 섬으로 이주하다.";
        ShowNewspaper(content);
    }

    void FirePOP()
    {
        string content = "화재주의 경보, 산불발생!!";
        ShowNewspaper(content);
    }

    void DiseasePOP()
    {
        string content = "질병주의보, 알수없는 바이러스가 외딴 섬에서 발현되다!!";
        ShowNewspaper(content);
    }

    void FloadPOP()
    {
        string content = "태풍 경보, 주민들은 외출을 삼가시오.";
        ShowNewspaper(content);
    }

    void ShowNewspaper(string news)//화요일에 이미지 추가 image img, string news
    {
        if (!newspop)
        {
            Instantiate(newspaper);
            newspaper.GetComponent<EventUI>().newsLetter = news;
            newspop = true;
        }
        else if (newspop)
        {
            newspaper.GetComponent<Animator>().SetBool("EventPOP", true);
        }
    }
}
