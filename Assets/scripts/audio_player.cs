using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_player : MonoBehaviour
{
    // Start is called before the first frame update
    bool isplaying = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isplaying)
        {
            if (!this.GetComponent<AudioSource>().isPlaying)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void playSound()
    {
        isplaying = true;
        this.GetComponent<AudioSource>().Play();
    }
}
