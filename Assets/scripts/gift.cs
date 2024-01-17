﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gift : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(8, 8);
        topLeft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        bottomRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
    public int speed = 1;
    Vector2 topLeft, bottomRight;
    public void click()
    {
        Instantiate(GameObject.Find("pop"), null).GetComponent<audio_player>().playSound();
        start.points += 10 * speed;
        if (this.name.Contains("(Clone)"))
        {
            Destroy(this.gameObject);
        }

    }
    // Update is called once per frame
    void Update()
    {
        this.transform.eulerAngles = new Vector3(
            this.transform.eulerAngles.x,
            this.transform.eulerAngles.y,
            this.transform.eulerAngles.z + -360 * Time.deltaTime
       );

        if (this.name.Contains("(Clone)") && !start.paused)
        {
            if (this.transform.position.y < topLeft.y - 1)
            {
                Instantiate(GameObject.Find("hit"), null).GetComponent<audio_player>().playSound();
                Destroy(this.gameObject);
                start.hp -= 1;
            }
            else
            {
                this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - speed * Time.deltaTime);
            }
        }
    }
}
