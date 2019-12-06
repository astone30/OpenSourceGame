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
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < newsLetter.Length; i++)
        {
            news.text += newsLetter[i];
            if (Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.Space))
            {
                news.text = newsLetter;
                isClicked = true;
                break;
            }
        }
        if (isClicked)
        {
            if (Input.GetMouseButtonDown(0))
            {
                gameObject.GetComponent<Animator>().SetBool("EventDown", true);
            }
        }
    }
}
