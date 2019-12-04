using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConstructUI : MonoBehaviour
{
    public Tile buildTothis;//이곳에 건물 건설

    public Button cancleButton;

    public void Start()
    {
        cancleButton.onClick.AddListener(CancleOrder);
    }

    void CancleOrder()
    {
        this.GetComponentInParent<Player>().bUI = false;
        Destroy(this.gameObject);
    }
}
