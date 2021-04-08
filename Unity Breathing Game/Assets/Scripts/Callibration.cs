using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

/// <summary>
/// All input comes from here
/// </summary>

public class Callibration : MonoBehaviour
{
    public List<float> abdList = new List<float>();
    private List<float> chList = new List<float>();
    private List<float> tList = new List<float>();

    public List<float> abdCallibrationList = new List<float>();
    private List<float> chCallibrationList = new List<float>();
    private List<float> timeList = new List<float>();

    private List<float> mov_aves_abd = new List<float>();
    private List<float> mov_aves_ch = new List<float>();

    public bool sendToNorm = false;

    private GetBreathPacket getBreathPacketScript;
    private ButtonsManager buttonsManagerScript;

    float sum_abd_std;
    float sum_ch_std;
    public float mean_abd;
    public float mean_ch;
    public float std_abd;
    public float std_ch;
    public float max_abd;
    public float max_ch;
    public float min_abd;
    public float min_ch;

    public int std_multiplier = 3;

    public int mov_avg_N = 4;
    float mov_avg_abd_val = 0;
    float mov_avg_ch_val = 0;


    private void Awake()
    {
        getBreathPacketScript = GameObject.FindObjectOfType<GetBreathPacket>();
        buttonsManagerScript = GameObject.FindObjectOfType<ButtonsManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float temp_time = 0;
    // Update is called once per frame
    void Update()
    {
        if(getBreathPacketScript.timer > temp_time)
        {
            if (buttonsManagerScript.startCallibration)
            {
                CallibrationStart();
            }

            if (buttonsManagerScript.doTheCallibration)
            {
                CallibrationStop();
                sendToNorm = true;
            }

            if (buttonsManagerScript.finalInputsIncoming)
            {
                FinalInputs();
            }
        }
        temp_time = getBreathPacketScript.timer;
    }


    // When the 'Start Calibration' UI button is clicked
    
    public void CallibrationStart()
    {    
        abdList.Add(getBreathPacketScript.abdBreathValue);
        chList.Add(getBreathPacketScript.chBreathValue);
        tList.Add(getBreathPacketScript.timer);
    }


    // When 'Stop' button gets clicked, perform the following operations
    public void CallibrationStop() {

        Debug.Log("stop flag");
        // Calculate Means and Standard Deviations
        abdCallibrationList = abdList;
        chCallibrationList = chList;
        timeList = tList;

        mean_abd = abdCallibrationList.Average();
        foreach (float value_abd in abdCallibrationList)
        {
            sum_abd_std += Mathf.Pow((value_abd - mean_abd), 2);
        }
        std_abd = Mathf.Sqrt(sum_abd_std / abdCallibrationList.Count);

        mean_ch = chCallibrationList.Average();
        foreach (float value_ch in chCallibrationList)
        {
            sum_ch_std += Mathf.Pow((value_ch - mean_ch), 2);
        }
        std_ch = Mathf.Sqrt(sum_ch_std / chCallibrationList.Count);


        // Detect outliers with Means and STDs
        foreach (float val in abdCallibrationList.ToList())
        {
            //Debug.Log(abdCallibrationList.IndexOf(val) + ":" + val);
            if (val > (mean_abd + (std_multiplier * std_abd)) || val < (mean_abd - (std_multiplier * std_abd)))
            {
                int inda = abdCallibrationList.IndexOf(val);
                abdCallibrationList.RemoveAt(inda);
                chCallibrationList.RemoveAt(inda);
                timeList.RemoveAt(inda);
            }
        }

        foreach (float val in chCallibrationList.ToList())
        {
            if (val > (mean_ch + (std_multiplier * std_ch)) || val < (mean_ch - (std_multiplier * std_ch)))
            {
                int indc = chCallibrationList.IndexOf(val);
                abdCallibrationList.RemoveAt(indc);
                chCallibrationList.RemoveAt(indc);
                timeList.RemoveAt(indc);
            }
        }

        // Perform Moving Average Smoothing

        float sum_abd;
        float sum_ch;

        for (int window_start_index = 0; window_start_index < (abdCallibrationList.Count - mov_avg_N); window_start_index++)
        {
            float temp_abd = 0;
            float temp_ch = 0;

            for (int i = 0; i < (mov_avg_N); i++)
            {
                sum_abd = abdCallibrationList[i + window_start_index] + temp_abd;
                temp_abd = sum_abd;
                sum_ch = chCallibrationList[i + window_start_index] + temp_ch;
                temp_ch = sum_ch;
            }
            mov_avg_abd_val = temp_abd / mov_avg_N;
            mov_avg_ch_val = temp_ch / mov_avg_N;
            mov_aves_abd.Add(mov_avg_abd_val);
            mov_aves_ch.Add(mov_avg_ch_val);
        }

        abdCallibrationList = mov_aves_abd;
        chCallibrationList = mov_aves_ch;

        int len_abd = abdCallibrationList.Count;
        int len_ch = chCallibrationList.Count;
        int len_t = timeList.Count;

        int diff = len_t - len_abd;

        for (int popper = 0; popper <= diff; popper++)
        {
            timeList.RemoveAt(0);
        }

        // Calculate Maximum, Minimum, Mean and Standard deviation for the final smoothed lists
        max_abd = abdCallibrationList.Max() + 75;
        max_ch = chCallibrationList.Max() + 75;
        min_abd = abdCallibrationList.Min() - 25;
        min_ch = chCallibrationList.Min() - 25;

        mean_abd = abdCallibrationList.Average();

        foreach (float value_abd in abdCallibrationList)
        {
            sum_abd_std += Mathf.Pow((value_abd - mean_abd), 2);
        }
        std_abd = Mathf.Sqrt(sum_abd_std / abdCallibrationList.Count);

        mean_ch = chCallibrationList.Average();
        foreach (float value_ch in chCallibrationList)
        {
            sum_ch_std += Mathf.Pow((value_ch - mean_ch), 2);
        }
        std_ch = Mathf.Sqrt(sum_ch_std / chCallibrationList.Count);

        Debug.Log("A mean ---------------------------- : " + mean_abd);

        buttonsManagerScript.doTheCallibration = false;
        
    }


    public float abdValFromGBP;
    public float chValFromGBP;
    public float tValFromGBP;

    private List<float> abdSmoothingWindow = new List<float>();
    private List<float> chSmoothingWindow = new List<float>();

    public float smoothedAbd = 0;
    public float smoothedCh = 0;

    public float normedAbd = 0;
    public float normedCh = 0;

    public bool isInhaling = false;
    public bool isExhaling = false;
    public bool isConstant = false;

    public void FinalInputs()
    {
        // Perform Outlier Removal with means and standard deviations and then get the cleaned input without outliers

        if (getBreathPacketScript.abdBreathValue > (mean_abd + (std_multiplier * std_abd)) || getBreathPacketScript.abdBreathValue < (mean_abd - (std_multiplier * std_abd)) || getBreathPacketScript.chBreathValue > (mean_ch + (std_multiplier * std_ch)) || getBreathPacketScript.chBreathValue < (mean_ch - (std_multiplier * std_ch)))
        {
            Debug.Log("Outlier Eliminated: ABD: " + getBreathPacketScript.abdBreathValue + "    CH: " + getBreathPacketScript.chBreathValue);
        }
        else
        {
            abdValFromGBP = getBreathPacketScript.abdBreathValue;
            chValFromGBP = getBreathPacketScript.chBreathValue;
            tValFromGBP = getBreathPacketScript.timer;
        }

        // Moving Average Smoothing

        if (abdSmoothingWindow.Count < 5)
        {
            abdSmoothingWindow.Add(abdValFromGBP);
            chSmoothingWindow.Add(chValFromGBP);
        }

        float sum_abd;
        float sum_ch;

        for (int window_start_index = 0; window_start_index < (abdSmoothingWindow.Count - mov_avg_N); window_start_index++)
        {
            float temp_abd = 0;
            float temp_ch = 0;

            for (int i = 0; i < (mov_avg_N); i++)
            {
                sum_abd = abdSmoothingWindow[i + window_start_index] + temp_abd;
                temp_abd = sum_abd;
                sum_ch = chSmoothingWindow[i + window_start_index] + temp_ch;
                temp_ch = sum_ch;
            }
            smoothedAbd = temp_abd / mov_avg_N;
            smoothedCh = temp_ch / mov_avg_N;

            float temp_prev_window_value = abdSmoothingWindow[0];
            int posCount = 0;
            int negCount = 0;
            foreach(float each_val in abdSmoothingWindow.GetRange(1, mov_avg_N-1))
            {
                if(each_val > temp_prev_window_value)
                {
                    posCount++;
                }
                else if(each_val < temp_prev_window_value)
                {
                    negCount++;
                }
                else
                {
           
                }

                temp_prev_window_value = each_val;
            }
            /*if(posCount > negCount)
            {
                Debug.Log("Decreasing");
            }
            else if(posCount < negCount)
            {
                Debug.Log("Increasing");
            }
            else
            {
                Debug.Log("Equal");
            }*/

           // CheckDirection(abdSmoothingWindow, "abdomen");
            CheckDirection(chSmoothingWindow, "chest");

            //ImmediateAccelerationCheck(abdSmoothingWindow, mean_abd);
            ImmediateAccelerationCheck(chSmoothingWindow, mean_ch);

            abdSmoothingWindow.RemoveAt(0);
            chSmoothingWindow.RemoveAt(0);
        }

        // Normalize values using Max, min

        normedAbd = 1 - ((smoothedAbd - min_abd) / (max_abd - min_abd));
        normedCh = 1 - ((smoothedCh - min_ch) / (max_ch - min_ch));

        if (normedAbd > 0.99)
        {
            normedAbd = 0.99f;
        }
        if (normedAbd < 0.01)
        {
            normedAbd = 0.01f;
        }

        if (normedCh > 0.99)
        {
            normedCh = 0.99f;
        }
        if (normedCh < 0.01)
        {
            normedCh = 0.01f;
        }

    }

    public float lowThreshold = 0.2f;
    public float highThreshold = 0.8f;
    public void CheckDirection(List<float> window, string whichPart )
    {
        float temp_prev_window_value = window[0];
        int posCount = 0;
        int negCount = 0;
        foreach (float each_val in window.GetRange(1, mov_avg_N - 1))
        {
            if (each_val > temp_prev_window_value)
            {
                posCount++;
            }
            else if (each_val < temp_prev_window_value)
            {
                negCount++;
            }
            else
            {

            }

            temp_prev_window_value = each_val;
        }
        if (posCount > negCount) 
        {
            isExhaling = true;
            isInhaling = false;
            isConstant = false;
            //Debug.Log(whichPart + "Decreasing");
        }
        else if (posCount < negCount)
        {
            isExhaling = false;
            isInhaling = true;
            isConstant = false;
            //Debug.Log(whichPart + "Increasing");
        }
        else
        {
            isExhaling = false;
            isInhaling = false;
            isConstant = true;
            //Debug.Log(whichPart + "Constant-ish");
        }
        
    }

    public float theVarianceDiff = 0;
    public bool quick = false;
    public bool slow = false;
    public bool hold = false;
    public void ImmediateAccelerationCheck(List<float> theWindow, float mean)
    {
        float diff = 0;
        float max = 0;
        float min = 0;
        //float variance = 0;

        max = theWindow.Max();
        min = theWindow.Min();
        diff = max - min;

        theVarianceDiff = diff;

        if (theVarianceDiff <= 7)
        {
            hold = true;
            slow = false;
            quick = false;
        }
        if((7 < theVarianceDiff) && (theVarianceDiff <= 30))
        {
            hold = false;
            slow = true;
            quick = false;
        }
        if (theVarianceDiff > 30)
        {
            hold = false;
            slow = false;
            quick = true;
        }
    }

}
