using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StopSignals : MonoBehaviour
{
    InhalationScript inhalationScript;
    ExhalationSound exhalationScript;
    MoveBackground moveBackgroundScript;

    public TextMeshPro scoreText;

    public int inCount;
    public int exCount;

    public GameObject signalPrefab;
    public Transform signalSpawnPos;

    private GameObject videoPlayer;

    private Light redLight;
    private Light greenLight;
    private Light yellowLight;

    private GameObject breatheInText;
    private GameObject breatheOutText;
    private GameObject note1;
    private GameObject note2;
    private GameObject note3;
    private GameObject note4;
    private GameObject note5;
    private GameObject note6;
    private GameObject note7;
    private GameObject note8;

    public GameObject signBoard;

    public bool redLightOn = false;
    private AudioSource theSong;
    public GameObject theCar;
    private float songVolumeAtSignal;
    private MoveBackground terrainStopper;
    // Start is called before the first frame update
    void Start()
    {
        redLight = GameObject.Find("RedLight").GetComponent<Light>();
        greenLight = GameObject.Find("Green Light").GetComponent<Light>();
        yellowLight = GameObject.Find("Yellow Light").GetComponent<Light>();
        videoPlayer = GameObject.Find("Video Player");
        videoPlayer.SetActive(false);

        inhalationScript = GameObject.FindObjectOfType<InhalationScript>();
        exhalationScript = GameObject.FindObjectOfType<ExhalationSound>();
        moveBackgroundScript = GameObject.FindObjectOfType<MoveBackground>();
        terrainStopper = GameObject.Find("Terrain").GetComponent<MoveBackground>();

        breatheInText = GameObject.Find("BreatheInText");
        breatheOutText = GameObject.Find("BreatheOutText");
        note1 = GameObject.Find("Note1");
        note2 = GameObject.Find("Note2");
        note3 = GameObject.Find("Note3");
        note4 = GameObject.Find("Note4");
        note5 = GameObject.Find("Note5");
        note6 = GameObject.Find("Note6");
        note7 = GameObject.Find("Note7");
        note8 = GameObject.Find("Note8");

        breatheInText.SetActive(false);
        breatheOutText.SetActive(false);
        note1.SetActive(false);
        note2.SetActive(false);
        note3.SetActive(false);
        note4.SetActive(false);
        note5.SetActive(false);
        note6.SetActive(false);
        note7.SetActive(false);
        note8.SetActive(false);

        theSong = GameObject.Find("Hey Jude Audio").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        inCount = inhalationScript.countIn;
        exCount = inhalationScript.countEx;

        StopSignalAfterXXX(inCount, exCount);
        SignBoard(inCount, exCount);
        UpdateScore(inhalationScript.score);
        SongController();
        MoveSignBoard();
    }

    private void MoveSignBoard()
    {
        if (moveSignboard)
        {
            if (signBoard.transform.position.z > 23)
            {
                signBoard.transform.Translate(Vector3.forward * Time.deltaTime * 7);
            }
            if(signBoard.transform.position.z == 23)
            {
                moveSignboard = false;
            }
        }
        if (moveSignboardAgain)
        {            
            signBoard.transform.Translate(Vector3.forward * Time.deltaTime * 7);
            if (signBoard.transform.position.z < -2)
            {
                Destroy(signBoard);
            }
            
        }

    }
    private bool moveSignboard = false;
    private bool moveSignboardAgain = false;
    public void StopSignalAfterXXX(int countInFromInScript, int countExFromInScript)
    {
        if(countInFromInScript == 16 && countExFromInScript == 12)
        {
            yellowLight.intensity = 17;
            greenLight.intensity = 2;
            redLight.intensity = 2;
            moveBackgroundScript.speed = 5;
            songVolumeAtSignal = theSong.volume;
            signBoard.SetActive(true);
            moveSignboard = true;
            
            
        }
        if(countInFromInScript==16 && countExFromInScript == 15)
        {
            //Debug.Log("boutesh kumar");
            redLightOn = true;
            redLight.intensity = 25;
            yellowLight.intensity = 2;
            greenLight.intensity = 2;
                     
            //theSong.Pause();
        }
    }

    void SongController()
    {

        if (redLightOn)
        {
            theSong.volume = 0.1f;
            moveBackgroundScript.speed = 0;
            terrainStopper.speed = 0;
            videoPlayer.SetActive(true);

            breatheOutText.SetActive(false);
            breatheInText.SetActive(false);
        }
        else
        {
            moveBackgroundScript.speed = 30;
            terrainStopper.speed = 30;
        }

        if(inhalationScript.countIn > 16)
        {
            redLightOn = false;
            videoPlayer.SetActive(false);
            theSong.volume = songVolumeAtSignal;
            moveSignboardAgain = true;
        }
    }

    void SignBoard(int countInFromInScript, int countExFromInScript)
    {
        if(countInFromInScript == countExFromInScript + 1)
        {
            note1.SetActive(true);
            note2.SetActive(false);
            note3.SetActive(false);
            note4.SetActive(false);
            note5.SetActive(false);
            note6.SetActive(false);
            note7.SetActive(false);
            note8.SetActive(false);
        }

        if(countInFromInScript == countExFromInScript + 2)
        {            
            note1.SetActive(true);
            note2.SetActive(true);
            note3.SetActive(false);
            note4.SetActive(false);
            note5.SetActive(false);
            note6.SetActive(false);
            note7.SetActive(false);
            note8.SetActive(false);
        }

        if (countInFromInScript == countExFromInScript + 3)
        {            
            note1.SetActive(true);
            note2.SetActive(true);
            note3.SetActive(true);
            note4.SetActive(false);
            note5.SetActive(false);
            note6.SetActive(false);
            note7.SetActive(false);
            note8.SetActive(false);
        }

        if (countInFromInScript == countExFromInScript + 4)
        {
            note1.SetActive(true);
            note2.SetActive(true);
            note3.SetActive(true);
            note4.SetActive(true);
            note5.SetActive(false);
            note6.SetActive(false);
            note7.SetActive(false);
            note8.SetActive(false);
        }

        if (countInFromInScript == countExFromInScript)
        {
            note1.SetActive(false);
            note2.SetActive(false);
            note3.SetActive(false);
            note4.SetActive(false);
            note5.SetActive(false);
            note6.SetActive(false);
            note7.SetActive(false);
            note8.SetActive(false);
        }

        if (countInFromInScript == countExFromInScript - 1)
        {
            note1.SetActive(true);
            note2.SetActive(true);
            note3.SetActive(true);
            note4.SetActive(true);
            note5.SetActive(true);
            note6.SetActive(false);
            note7.SetActive(false);
            note8.SetActive(false);
        }

        if (countInFromInScript == countExFromInScript - 2)
        {
            note1.SetActive(true);
            note2.SetActive(true);
            note3.SetActive(true);
            note4.SetActive(true);
            note5.SetActive(true);
            note6.SetActive(true);
            note7.SetActive(false);
            note8.SetActive(false);
        }

        if (countInFromInScript == countExFromInScript - 3)
        {
            note1.SetActive(true);
            note2.SetActive(true);
            note3.SetActive(true);
            note4.SetActive(true);
            note5.SetActive(true);
            note6.SetActive(true);
            note7.SetActive(true);
            note8.SetActive(false);
        }

        if (countInFromInScript == countExFromInScript - 4)
        {
            note1.SetActive(true);
            note2.SetActive(true);
            note3.SetActive(true);
            note4.SetActive(true);
            note5.SetActive(true);
            note6.SetActive(true);
            note7.SetActive(true);
            note8.SetActive(true);
        }


        if (inhalationScript.inhaleNow)
        {
            breatheInText.SetActive(true);
            breatheOutText.SetActive(false);
        }

        if (exhalationScript.exhaleNow)
        {

                breatheInText.SetActive(false);
                breatheOutText.SetActive(true);

            
        }

    }

    public void UpdateScore(int scoreToAdd)
    {
        scoreText.text = "Score: " + scoreToAdd;
    }
}
