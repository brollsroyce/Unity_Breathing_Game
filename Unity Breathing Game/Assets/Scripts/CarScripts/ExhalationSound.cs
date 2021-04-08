using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExhalationSound : MonoBehaviour
{
    InhalationScript inhalationScript;

    public int countIn = 0;
    public int countEx = 0;

    public AudioSource breathSource;
    public AudioClip breathClip;

    Callibration callibrationScript;

    public bool exhaleNow = false;
    StopSignals stopSignalsScript;
    // Start is called before the first frame update
    void Start()
    {
        stopSignalsScript = GameObject.FindObjectOfType<StopSignals>();
        inhalationScript = GameObject.FindObjectOfType<InhalationScript>();
        callibrationScript = GameObject.FindObjectOfType<Callibration>();
    }

    // Update is called once per frame
    void Update()
    {
        countIn = inhalationScript.countIn;

    }   

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "High")
        {
            //Debug.Log("Exhalationo colidion");
            countEx++;

            if (countEx == countIn - 3)
            {
                //Debug.Log(countEx + "Exhalation sound here" + countIn);
                breathSource.PlayOneShot(breathClip);
                /*if (stopSignalsScript.redLightOn)
                {
                    exhaleNow = false;
                    inhalationScript.inhaleNow = false;
                }*/
                exhaleNow = true;
                inhalationScript.inhaleNow = false;


            }

            if (breathSource.volume > 0 && (Input.GetKey(KeyCode.E) || callibrationScript.isExhaling))
            {
                inhalationScript.score++;
                breathSource.volume = breathSource.volume - (0.0007f * inhalationScript.score);
            }
            if (breathSource.volume > 0 && !(Input.GetKey(KeyCode.E) || !callibrationScript.isExhaling))
            {
                inhalationScript.score--;
                breathSource.volume = breathSource.volume + (0.0001f * inhalationScript.score);
            }
        }

    }
}
