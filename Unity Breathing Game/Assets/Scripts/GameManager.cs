using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private List<float> abdCallibrationList;
    private List<float> chCallibrationList;
    private List<float> timeList;

    private GetBreathPacket getBreathPacketScript;

    float sum_abd_std;
    float sum_ch_std;
    float mean_abd;
    float mean_ch;
    float std_abd;
    float std_ch;
    float max_abd;
    float max_ch;
    float min_abd;
    float min_ch;

    public float normed_abd;
    public float normed_ch;

    public float std_multiplier = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        getBreathPacketScript = GetComponent<GetBreathPacket>();
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    // Call method after pressing 'Callibration' Button on start screen.
    // Get abd and ch int values from GetBreathPacket

   
   
}
