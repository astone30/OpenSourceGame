using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public Text money;
    public Text land;
    public Text currentTurn;
    public Text left_time;

    public Button exit;

    public Button toCharacter;
    public Button toyourland;
    public Button financial;
    public Button currentSituation;
    public Button turnend;

    private Player theone;


    private float time = 0;
    private float speed = 0.009f;
    private float score;

    private void Awake()
    {
        theone = this.gameObject.GetComponent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTurn.text = GameManager.instance.currentTrun.ToString("00");
        left_time.text = GameManager.instance.turnTime.ToString("00");
        money.text = theone.money.ToString("0000000000") +"$";
    }

    // Update is called once per frame
    void Update()
    {
        currentTurn.text = GameManager.instance.currentTrun.ToString("00");
        left_time.text = GameManager.instance.turnTime.ToString("00");
        if (theone.money.ToString("0000000000") + "$" != money.text)
        {
            time += Time.deltaTime * speed;

            score = Mathf.Lerp(score, theone.money, time);
            money.text = score.ToString("0000000000") + "$";
        }
    }



}
