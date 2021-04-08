using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlasherScript : MonoBehaviour
{
    Callibration calIbrationScript;

    public GameObject leftGlower;
    public GameObject rightGlower;
   
    // Start is called before the first frame update
    void Start()
    {
        calIbrationScript = GameObject.FindObjectOfType<Callibration>();

        leftGlower.SetActive(false);
        rightGlower.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        SlasherGlower();
    }

    void SlasherGlower()
    {
        if (Input.GetKey(KeyCode.A) || calIbrationScript.isInhaling)
        {
            leftGlower.SetActive(true);
            //Debug.Log("Bout men");
        }
        else
        {
            leftGlower.SetActive(false);
        }

        if(Input.GetKey(KeyCode.D) || calIbrationScript.isExhaling)
        {
            rightGlower.SetActive(true);
        }
        else
        {
            rightGlower.SetActive(false);
        }
    }

}
