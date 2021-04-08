using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour
{

    public bool startCallibration = false;
    public bool doTheCallibration = false;
    public bool finalInputsIncoming = false;

    Callibration callibrationScript;

    public GameObject startGame;
    public GameObject canvas;

    private void Awake()
    {
        callibrationScript = GameObject.FindObjectOfType<Callibration>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCalibrationButtonFunctionality()
    {
        startCallibration = true;
        doTheCallibration = false;
        finalInputsIncoming = false;
    }

    public void DoTheCallibrationButtonFunctionality()
    {
        doTheCallibration = true;
        startCallibration = false;
        finalInputsIncoming = false;
    }

    public void StartFinalGameInputButtonFunctionality()
    {
        doTheCallibration = false;
        startCallibration = false;
        finalInputsIncoming = true;
        startGame.SetActive(true);
        canvas.SetActive(false);
    }



}
