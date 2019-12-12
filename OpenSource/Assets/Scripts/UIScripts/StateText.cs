using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StateText : MonoBehaviour
{
    public Text state;
    public Text guide;
    public Text health;

    public GameObject panel;

    public int count=0;

    public bool readySet = false;
    public bool ready = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetPosition());
    }
    private void Update()
    {
        if (gameObject.GetComponentInParent<Player>().playerCharcteronScreen != null)
        {
            state.text = "ActionPoint Left : " + gameObject.GetComponentInParent<Player>().actionpoint.ToString();
            health.text = "CharacterHealth : " + gameObject.GetComponentInParent<Player>().playerCharcteronScreen.GetComponent<Character>().health.ToString("000");
        }
        if (GameManager.instance.currentTrun != 0)
        {
            StopCoroutine(SetPosition());
            panel.SetActive(false);
        }
    }


    IEnumerator SetPosition()
    {
        while(GameManager.instance.currentTrun ==0 && gameObject.GetComponentInParent<Player>().territory.Count < 1)
        {
            guide.text = "Choose Starting Point";
            yield return new WaitForSeconds(1f);
            guide.text = "Choose Starting Point.";
            yield return new WaitForSeconds(1f);
            guide.text = "Choose Starting Point..";
            yield return new WaitForSeconds(1f);
            guide.text = "Choose Starting Point...";
            yield return new WaitForSeconds(1f);
        }
        while (GameManager.instance.currentTrun == 0 && gameObject.GetComponentInParent<Player>().territory.Count == 1)
        {
            guide.text = "Waiting Other Player";
            yield return new WaitForSeconds(1f);
            guide.text = "Waiting Other Player.";
            yield return new WaitForSeconds(1f);
            guide.text = "Waiting Other Player..";
            yield return new WaitForSeconds(1f);
            guide.text = "Waiting Other Player...";
            yield return new WaitForSeconds(1f);
        }
    }


}
