using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{

    public static EventHandler instance;

    public GameObject newspaper;
    public List<Sprite> eventimages; //후에 추가

    public bool set = false;
    public bool newsDone = false;
    bool newspop = false;

    public List<GameObject> wanderingpeople;
    public GameObject wanderingKiller;

    public GameObject KillerEvent;
    public GameObject FireEvent;
    public GameObject FloadEvent;
    public GameObject VirusEvent;
    public GameObject PeopleEvent;

    public int a;
    public int index;

    public string newsdebug;

    public int eventhandle;

    public int[] scenario = {4,5,2,1,0,3,4,2,4,3};
    private void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.playFSM == GameManager.GamePlayFSM.gotoNextTurn) //이벤트 종류 결정
        {
            set = false;
            if (!set)
            {
                a = scenario[index];
                EventSet(a);
                index++;
            }
        }
    }

    void EventSet(int index)
    {
        if (index < 4)
        {
            switch (index)
            {
                case 0:
                    if (wanderingKiller != null)
                    {
                        a = Random.Range(0, 7);
                        EventSet(a);
                    }
                    else if (wanderingKiller == null)
                    {
                        DecideEvent(KillerEvent, KillerEvent.GetComponent<Killer>().description);
                        set = true;
                    }
                    break;
                case 1:
                    DecideEvent(FireEvent, FireEvent.GetComponent<Fire>().description);
                    set = true;
                    break;
                case 2:
                    DecideEvent(FloadEvent, FloadEvent.GetComponent<Fload>().description);
                    set = true;
                    break;
                case 3:
                    DecideEvent(VirusEvent, VirusEvent.GetComponent<Virus>().description);
                    set = true;
                    break;
            }
        }
        else if (index >= 4)
        {
            if (wanderingpeople.Count < 3)
            {
                //주민 이벤트로 결정
                DecideEvent(PeopleEvent, PeopleEvent.GetComponent<PeopleComing>().description);
                set = true;
            }
            else if (wanderingpeople.Count == 3)
            {
                a = Random.Range(0, 7);
                EventSet(a);
            }
        }
    }

    void DecideEvent(GameObject thisevent, string newsdescription)
    {
        newspop = false;
        newsdebug = newsdescription;
        ShowNewspaper(newsdescription);
        LocateEvent(thisevent);
    }

    void LocateEvent(GameObject thisevent)
    {
        GameObject eventpref = null;
        if (thisevent.GetComponent<Killer>())
        {
            eventpref = Instantiate(thisevent);
        }
        else if (thisevent.GetComponent<Fire>())
        {
            eventpref = Instantiate(thisevent);
        }
        else if (thisevent.GetComponent<Fload>())
        {

        }
        else if (thisevent.GetComponent<Virus>())
        {
            eventpref = Instantiate(thisevent);
        }
        else if (thisevent.GetComponent<PeopleComing>())
        {
            if (eventpref == null)
            {
                eventpref = Instantiate(thisevent);
            }

        }
    }

    void ShowNewspaper(string news)//화요일에 이미지 추가 image img, string news
    {
        if (!newspop)
        {
            GameObject pref = Instantiate(newspaper);
            pref.GetComponent<EventUI>().newsLetter = news;
            newspop = true;
        }
    }

}
