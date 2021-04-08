using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InhalationScript : MonoBehaviour
{
    ExhalationSound exhalationSoundScript;

    public int countIn = 0;
    public int countEx = 0;

    public AudioSource breathSource;
    public AudioClip breathClip;

    private AudioSource song;

    public bool inhaleNow = false;

    Callibration callibrationScript;
    // Start is called before the first frame update
    void Start()
    {
        exhalationSoundScript = GameObject.FindObjectOfType<ExhalationSound>();
        breathSource = GetComponent<AudioSource>();

        callibrationScript = GameObject.FindObjectOfType<Callibration>();
    }

    // Update is called once per frame
    void Update()
    {
        countEx = exhalationSoundScript.countEx;
    }

    public int score = 0;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Low")
        {
            countIn++;

            if (countIn == countEx + 1)
            {
                //Debug.Log("Inhalation sound here");
                breathSource.PlayOneShot(breathClip);
                inhaleNow = true;
                exhalationSoundScript.exhaleNow = false;
            }

            if ((breathSource.volume > 0) && (Input.GetKey(KeyCode.W) || callibrationScript.isInhaling))
            {
                score++;
                breathSource.volume = breathSource.volume - (0.0005f * score);

            }
            if ((breathSource.volume > 0) && !(Input.GetKey(KeyCode.W) || !callibrationScript.isInhaling))
            {
                breathSource.volume = breathSource.volume + (0.0001f * score);
                score--;
            }
        }
               
    }

}
