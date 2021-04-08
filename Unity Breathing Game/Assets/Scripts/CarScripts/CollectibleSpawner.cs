using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Did not use this script

public class CollectibleSpawner : MonoBehaviour
{
    public GameObject[] collectibles;
    public Transform[] points;

    public float beat = 60 / 72;
    private float timer;
    //private int whichPoint;

    // Counts on which to inhale or exhale. More or less no. of counts can be added, for eg: {1,1} or {1,1,1,1,1,1,1,1}
    private int[] out4Counts = { 1, 1, 1, 1 };
    private int[] in4Counts = { 0, 0, 0, 0 };
    //private int[] in4Counts = { 1 };

    private int[] nullBassNullBass = { 0, 1, 0, 1 };
    private int[] bassNullBassnull = { 1, 0, 1, 0 };
    private int[] bassBassNullNull = { 1, 1, 0, 0 };
    private int[] nullNullBassBass = { 0, 0, 1, 1 };

    private int[] nullHiNullHi = { 0, 2, 0, 2 };
    private int[] hiNullHinull = { 2, 0, 2, 0 };
    private int[] hiHiNullNull = { 2, 2, 0, 0 };
    private int[] nullNullHiHi = { 0, 0, 2, 2 };

    private int[] videoBreak = new int[114];

    public List<int> spawnPoints = new List<int>();     // This is where the whole exercise is created
    public int listIndex;

    // Chord sound codes
    private int noSound = 0;
    private int bassDrum = 1;
    private int hiHat = 2;

    // Chords need to be added to this list
    public List<int> chords = new List<int>();

    private void Awake()
    {
        for (int i = 0; i < 114; i++)
        {
            videoBreak[i] = 0;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        

        timer = beat;
        #region Expand to set the exercise

        // Set the breathing exercise pattern here
        AddCountsToList(in4Counts, nullNullBassBass);
        AddCountsToList(out4Counts, nullNullHiHi);
        AddCountsToList(in4Counts, nullNullBassBass);
        AddCountsToList(out4Counts, nullNullHiHi);
        AddCountsToList(in4Counts, nullNullBassBass);
        AddCountsToList(out4Counts, nullNullHiHi);
        AddCountsToList(in4Counts, nullNullBassBass);
        AddCountsToList(out4Counts, nullNullHiHi);
        AddCountsToList(videoBreak, videoBreak);
        AddCountsToList(in4Counts, nullNullBassBass);
        AddCountsToList(out4Counts, nullNullHiHi);
        AddCountsToList(out4Counts, nullNullHiHi);
        AddCountsToList(in4Counts, nullNullBassBass);
        AddCountsToList(out4Counts, nullNullHiHi);
        AddCountsToList(in4Counts, nullNullBassBass);
        AddCountsToList(out4Counts, nullNullHiHi);
        AddCountsToList(out4Counts, nullNullHiHi);
        /*AddCountsToList(in4Counts, nullNullBassBass);
        AddCountsToList(out4Counts, nullNullHiHi);
        AddCountsToList(in4Counts, nullNullBassBass);
        AddCountsToList(out4Counts, nullNullHiHi);
        AddCountsToList(in4Counts, nullNullBassBass);
        AddCountsToList(out4Counts, nullNullHiHi);
        AddCountsToList(in4Counts, nullNullBassBass);
        AddCountsToList(out4Counts, nullNullHiHi);
        AddCountsToList(in4Counts, nullNullBassBass);
        AddCountsToList(out4Counts, nullNullHiHi);
        AddCountsToList(in4Counts, nullNullBassBass);
        AddCountsToList(out4Counts, nullNullHiHi);*/




        /*AddCountsToList(in4Counts, fLow);
        AddCountsToList(out4Counts, cHigh);
        AddCountsToList(in4Counts, c7Low);
        AddCountsToList(out4Counts, fHigh);
        AddCountsToList(in4Counts, bbLow);
        AddCountsToList(out4Counts, fHigh);
        AddCountsToList(in4Counts, cLow);
        AddCountsToList(out4Counts, fHigh);
        AddCountsToList(in4Counts, fLow);
        AddCountsToList(out4Counts, cHigh);
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
            GameObject cubeSpawn = Instantiate(collectibles[chords[listIndex]], points[spawnPoints[listIndex]]);

            timer -= beat;

            listIndex++;
        }

        timer += Time.deltaTime;
    }

    void AddCountsToList(int[] addCounts, int[] chord)
    {

        for (int i = 0; i < addCounts.Length; i++)
        {
            spawnPoints.Add(addCounts[i]);
            chords.Add(chord[i]);
        }
    }
}
