using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongVolumeAdjustments : MonoBehaviour
{
    InhalationScript inhalationScript;
    public AudioSource songSource;
    public AudioClip songClip;
    // Start is called before the first frame update
    void Start()
    {
        inhalationScript = GameObject.FindObjectOfType<InhalationScript>();
        songSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(songSource.tag == "Main Song")
        {
            if (songSource.volume < 1)
            {
                songSource.volume = 0.015f * inhalationScript.score;
            }
        }

        if(songSource.tag == "Metronome")
        {
            if (songSource.volume > 0)
            {
                songSource.volume = songSource.volume - (0.0005f * inhalationScript.score);
            }
        }
        
    }
}
