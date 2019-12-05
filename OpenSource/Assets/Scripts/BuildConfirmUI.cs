using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildConfirmUI : MonoBehaviour
{
    public Tile hereHere;

    public Button build;
    public Button cancle;

    public Text price;
    public Text description;

    public Sprite mug;

    public int pricenum;
    public string Description;
    
    // Start is called before the first frame update
    void Start()
    {
        cancle.onClick.AddListener(Nevermind);
        build.onClick.AddListener(Build);
    }

    // Update is called once per frame
    void Update()
    {
        if (price.text != pricenum.ToString())
        {
            price.text = "Price : " + pricenum.ToString("0000");
        }
        if (description.text != Description)
        {
            description.text = Description;
        }
    }

    void Build()
    {
        if (this.gameObject.GetComponentInParent<Player>().money >= pricenum)
        {
            this.gameObject.GetComponentInParent<Player>().money -= pricenum;
            if (hereHere.Kind_Of_This != Tile.TileKind.MOUNTAIN)
            {
                hereHere.Kind_Of_This = Tile.TileKind.PAVED;
                hereHere.Price *= 2;
                hereHere.GetComponent<MeshRenderer>().material = hereHere.GetComponent<Tile>().pave;
                hereHere.tileColor = hereHere.GetComponent<Tile>().pave.color;
            }
            else if (hereHere.Kind_Of_This == Tile.TileKind.MOUNTAIN)
            {
                hereHere.mountainChange = true;
            }
        }
        else if (this.gameObject.GetComponentInParent<Player>().money < pricenum)
        {
            Debug.Log("자금 부족!!");
        }
        this.gameObject.GetComponentInParent<Player>().bUI = false;
        Destroy(this.gameObject);
    }

    void Nevermind()
    {
        Destroy(this.gameObject);
    }
}
