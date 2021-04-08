using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovements : MonoBehaviour
{
    Callibration calScript;

    private float xPos = 4.3f;
    private float xNeg = -1.3f;

    public float horMovSpeed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        calScript = GameObject.FindObjectOfType<Callibration>();

    }

    // Update is called once per frame
    void Update()
    {
        MoveCar();
    }

    void MoveCar()
    {
         
        if(transform.position.x > xNeg && transform.position.x < xPos)
        {
            //if inhaling, move car left until -xlim
            if ((calScript.isInhaling || Input.GetKey(KeyCode.A)) && transform.position.x > (xNeg + 0.3))
            {
                //Debug.Log("Left");
                transform.Translate(Vector3.left * horMovSpeed * Time.deltaTime);
            }

            // If exhaling, move car right until +xlim
            if ((calScript.isExhaling || Input.GetKey(KeyCode.D)) && transform.position.x < (xPos - 0.3))
            {
                //Debug.Log("Right");
                transform.Translate(Vector3.right * horMovSpeed * Time.deltaTime);
            }
        }

        
    }


}
