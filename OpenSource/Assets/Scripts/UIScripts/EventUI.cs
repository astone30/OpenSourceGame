using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventUI : MonoBehaviour
{
    public Image eventImage;
    public Text news;

    public string newsLetter;
    bool isClicked = false;
    public float time = 6f;
    // Update is called once per frame
    private void Awake()
    {
        EventHandler.instance.newsDone = false;
        gameObject.GetComponent<Animator>().SetBool("EventPOP", true);
    }

    void Update()
    {
        time -= Time.deltaTime;   
        for (int i = 0; i < newsLetter.Length; i++)
        {
            news.text = newsLetter;
            if (gameObject.GetComponent<Animator>().GetBool("EventPOP"))
            {
                if (Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Space))
                {
                    news.text = newsLetter;
                    isClicked = true;
                    break;
                }
            }
        }
        if (time <3)
        {
             gameObject.GetComponent<Animator>().SetBool("EventDown", true);
             if (time < 0)
             {
                EventHandler.instance.newsDone = true;
                Destroy(gameObject);
             }
        }
    }
}
