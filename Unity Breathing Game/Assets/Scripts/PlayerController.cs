using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float turnSpeed;
    public float vehicleSpeed = 5.0f;
    public Vector3 right = new Vector3(0, 1, 0);
    public float horizontalInput;
    public float forwardInput;
    public float abdVal;
    public float chVal;
    public int horInput;
    public float fwdInput;

    private float xPosInhale = -4.0f;
    private float xPosExhale = 4.0f;

    //Normalization normScript;
    Callibration caliScript;
    // Start is called before the first frame update
    void Start()
    {
        //normScript = GameObject.FindObjectOfType<Normalization>();
        caliScript = GameObject.FindObjectOfType<Callibration>();
    }

    // Update is called once per frame
    void Update()
    {
        // Player inputs
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        
        horInput = Control1(out float chVal);
        fwdInput = chVal;

        // Move the vehicle forward, backward, turning
        transform.Translate(Vector3.forward * Time.deltaTime * vehicleSpeed *    fwdInput);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horInput);

    }

    int dir;
    public int Control1(out float chVal)
    {
        abdVal = 0;
        chVal = 0;
        // isInhaling and !hold = turn left
        if(caliScript.isInhaling && !caliScript.hold)
        {
            abdVal = caliScript.normedAbd;
            chVal = caliScript.normedCh;
            if (chVal <= 0.8)
            {
                dir = -1;
            }
            if (chVal > 0.8)
            {
                dir = 0;
            }
            //return dir;
        }

        // isExhaling and !hold = turn right
        if(caliScript.isExhaling && !caliScript.hold)
        {
            //Debug.Log("Turn Right");
            abdVal = 1 - caliScript.normedAbd;
            chVal = 1 - caliScript.normedCh;
            
            if(chVal <= 0.7)
            {
                dir = 1;
            }
            if(chVal > 0.7)
            {
                dir = 0;
            }
            //return dir;
        }

        if (caliScript.hold)
        {
            abdVal = caliScript.normedAbd;
            chVal = caliScript.normedCh;
            if (chVal <= 0.8)
            {
                dir = 0;
            }
            if (chVal > 0.8)
            {
                dir = 0;
            }
            //return dir;

        }
        return dir;
        
    }

    public float inThreshold = 0.7f;
    public float outThreshold = 0.3f;
    public void Thresholder()
    {
        if(caliScript.isInhaling && !caliScript.hold)
        {
            if (abdVal > inThreshold)
            {
                Debug.Log("Upper threshold crossed");
            }
        }

        if(caliScript.isExhaling && !caliScript.hold)
        {
            if (abdVal < outThreshold)
            {
                Debug.Log("Lower threshold crossed");
            }
        } 
    }

}
