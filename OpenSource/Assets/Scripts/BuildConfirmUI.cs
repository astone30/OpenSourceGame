using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildConfirmUI : MonoBehaviour
{
    public Button build;
    public Button cancle;
    
    // Start is called before the first frame update
    void Start()
    {
        cancle.onClick.AddListener(nevermind);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void nevermind()
    {
        //this.GetComponentInParent<Player>().bUI = false;
        Destroy(this.gameObject);
    }
}
