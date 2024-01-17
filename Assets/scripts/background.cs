using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 topLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 bottomRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        Transform cam = Camera.main.transform;
        Debug.Log("Top-left point in world space: " + topLeft);
        Debug.Log("Bottom-right point in world space: " + bottomRight);
        this.transform.position = new Vector2(cam.position.x, cam.position.y);
        this.transform.localScale = new Vector3(bottomRight.x - topLeft.x, topLeft.y - bottomRight.y, 10);
    }
}
