using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 20f;

    public Vector2 panLimit;

    bool startSet = false;
    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if (startSet)
        {
            if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
            {
                pos.z += panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
            {
                pos.z -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
            {
                pos.x -= panSpeed * Time.deltaTime;
            }
            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
            {
                pos.x += panSpeed * Time.deltaTime;
            }
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

        pos.y = Mathf.Clamp(pos.y, 5f, 15f);
        pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        pos.z = Mathf.Clamp(pos.z, -panLimit.y, 2f);


        if (gameObject.GetComponentInParent<Player>().isCharaterhere == true)
        {
            if (!startSet)
            {
                pos = Vector3.Lerp(pos, gameObject.GetComponentInParent<Player>().charactorPos, Time.deltaTime * 2f);
                if (pos.y <= 5)
                {
                    pos.y = 5f;
                    startSet = true;
                }
            }
        }

        transform.position = pos;
    }
}
