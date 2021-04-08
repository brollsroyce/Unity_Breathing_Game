using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnScript : MonoBehaviour
{
    public GameObject[] cubes;
    public Transform[] points;

    public float beat = (60 / 72);
    private float timer;

    // Counts on which to inhale or exhale. More or less no. of counts can be added, for eg: {1,1} or {1,1,1,1,1,1,1,1}
    private int[] in4Counts = { 1, 1, 1, 1 };
    private int[] out4Counts = { 0, 0, 0, 0 };

    private int[] videoBreak = new int[114];

    public List<int> spawnPoints = new List<int>();     // This is where the whole exercise is created
    public int listIndex;

    // Chord sound codes
    private int fLow = 0;
    private int fHigh = 1;
    private int cLow = 2;
    private int cHigh = 3;
    private int c7Low = 4;
    private int c7High = 5;
    private int bbLow = 6;
    private int bbHigh = 7;
    private int gLow = 8;
    private int gHigh = 9;
    private int nullChord = 10; 

    // Chords need to be added to this list
    public List<int> chords = new List<int>();

    private void Awake()
    {
        for(int i= 0; i < 114; i++)
        {
            videoBreak[i] = 1;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        
        timer = beat;
        #region Expand to set the exercise

        // Set the breathing exercise pattern here
        AddCountsToList(in4Counts, fLow);
        AddCountsToList(out4Counts, cHigh);
        AddCountsToList(in4Counts, c7Low);
        AddCountsToList(out4Counts, fHigh);
        AddCountsToList(in4Counts, bbLow);
        AddCountsToList(out4Counts, fHigh);
        AddCountsToList(in4Counts, cLow);
        AddCountsToList(out4Counts, fHigh);
        AddCountsToList(videoBreak, nullChord);
        AddCountsToList(in4Counts, gLow);
        AddCountsToList(out4Counts, gHigh);
        AddCountsToList(out4Counts, gHigh);
        AddCountsToList(in4Counts, gLow);
        AddCountsToList(out4Counts, gHigh);
        AddCountsToList(in4Counts, gLow);
        AddCountsToList(out4Counts, gHigh);
        AddCountsToList(out4Counts, gHigh);
        //AddCountsToList(in4Counts, fLow);
        /*AddCountsToList(out4Counts, cHigh);
        AddCountsToList(in4Counts, c7Low);
        AddCountsToList(out4Counts, fHigh);
        AddCountsToList(in4Counts, bbLow);
        AddCountsToList(out4Counts, fHigh);
        AddCountsToList(in4Counts, cLow);
        AddCountsToList(out4Counts, fHigh);*/
        /*AddCountsToList(in4Counts);
        AddCountsToList(out4Counts);
        AddCountsToList(in4Counts);
        AddCountsToList(out4Counts);
        AddCountsToList(in4Counts);
        AddCountsToList(out4Counts);
        AddCountsToList(in4Counts);
        AddCountsToList(out4Counts);
        AddCountsToList(in4Counts);
        AddCountsToList(out4Counts);
        AddCountsToList(in4Counts);
        AddCountsToList(out4Counts);
        AddCountsToList(in4Counts);
        AddCountsToList(out4Counts);
        AddCountsToList(in4Counts);
        AddCountsToList(out4Counts);
        AddCountsToList(in4Counts);
        AddCountsToList(out4Counts);
        AddCountsToList(in4Counts);
        AddCountsToList(out4Counts);
        AddCountsToList(in4Counts);
        AddCountsToList(out4Counts);
        AddCountsToList(in4Counts);
        AddCountsToList(out4Counts);*/
        #endregion


    }


    // Update is called once per frame
    void Update()
    {
        // Logic to instantiate cubes at the right beat and position
        if (timer > beat)
        {
            GameObject cubeSpawn = Instantiate(cubes[chords[listIndex]], points[spawnPoints[listIndex]]);

            timer -= beat;

            listIndex++;
        }

        timer += Time.deltaTime;
    }

    void AddCountsToList(int[] addCounts, int chord)
    {

        for (int i = 0; i < addCounts.Length; i++)
        {
            spawnPoints.Add(addCounts[i]);
            chords.Add(chord);
        }
    }

}
