using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructUI : MonoBehaviour
{
    public Tile buildTothis;//이곳에 건물 건설

    public Button cancleButton;

    public Button[] constructbuttons;

    public GameObject buildUIprefab;
    public GameObject buildUI;

    public void Start()
    {
        cancleButton.onClick.AddListener(CancleOrder);
        for (int i = 0; i < constructbuttons.Length; i++)
        {
            constructbuttons[i].onClick.AddListener(BuildPOPUP);
        }
    }

    void CancleOrder()
    {
        this.GetComponentInParent<Player>().bUI = false;
        Destroy(this.gameObject);
    }

    void BuildPOPUP()
    {
        buildUIprefab = Instantiate(buildUI);

    }
}
