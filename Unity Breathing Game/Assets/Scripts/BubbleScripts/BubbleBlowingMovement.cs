using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBlowingMovement : MonoBehaviour
{

    public GameObject bubble;

    private Vector3 scaleChange;

    private Vector3 positionChange;
    public bool moveIt;
    public bool release;
    public bool released;
    private float forward = 0.001f;
    private float randomX = 0;
    private float randomY = 0;

    Callibration calibrationScript;
    ButtonsManager buttonsManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        scaleChange = new Vector3(0.002f, 0.002f, 0.002f);
        positionChange = new Vector3(randomX, randomY, forward);
        calibrationScript = GameObject.FindObjectOfType<Callibration>();
        buttonsManagerScript = GameObject.FindObjectOfType<ButtonsManager>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (calibrationScript.quick || Input.GetKey(KeyCode.Q))
        {
            Expander(scaleChange);
            Debug.Log("Update fast men");
        }

        if (calibrationScript.slow || Input.GetKey(KeyCode.S))
        {
            // Debug.Log("Update men");
            // Expander(scaleChange * (0.5f - calibrationScript.normedAbd));
            Expander(scaleChange * 0.5f);
        }
        
        Floater();
    }
    


    public void Expander(Vector3 expandScale)
    {
        /* if (Input.GetKey(KeyCode.S))
         {
             // Expand the bubble
             bubble.transform.localScale += scaleChange;
             moveIt = false;            
         }*/
        bubble.transform.localScale += expandScale;
        release = true;
        released = false;

        /*if (buttonsManagerScript.finalInputsIncoming && !released)
        {
            if (calibrationScript.isExhaling && !calibrationScript.hold)
            {
                bubble.transform.localScale += expandScale;
                release = true;
            }
        }*/
    }

    public void Floater()
    {

       /* if (Input.GetKeyUp(KeyCode.S))
        {
            moveIt = true;
            // Release the bubble and make it move away            
        }*/

        if (release && (Input.GetKey(KeyCode.W)))// || !calibrationScript.isExhaling))
        {

            randomX = Random.Range(-0.003f, 0.003f);
            randomY = Random.Range(-0.001f, 0.001f);
            positionChange = new Vector3(randomX, randomY, forward);
            bubble.transform.position += positionChange;
            released = true;
        }
        
    }


}
