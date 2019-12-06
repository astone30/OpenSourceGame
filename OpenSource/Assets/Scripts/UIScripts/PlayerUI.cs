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

    public Button toCharacter; //케릭터 있는 위치로 화면을 이동 시킨다.
    public Button toyourland; 
    public Button financial;
    public Button currentSituation;
    public Button turnend;

    private Player theone;


    private float time = 0;
    private float speed = 0.1f;
    private float score;

    private void Awake()
    {
        theone = gameObject.GetComponentInParent<Player>();
    }

    // Start is called before the first frame update
    void Start()
    {
        currentTurn.text = GameManager.instance.currentTrun.ToString("00");
        left_time.text = GameManager.instance.turnTime.ToString("00");
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

        if (gameObject.GetComponentInParent<Player>().isCharaterhere)
        {
            gameObject.GetComponent<Animator>().SetBool("AllSet", true);
        }
    }



}
