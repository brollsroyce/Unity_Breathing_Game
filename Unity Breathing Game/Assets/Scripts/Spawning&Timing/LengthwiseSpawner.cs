using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
/// 
/// /// used in the breath slasher game 
///
public class LengthwiseSpawner : MonoBehaviour
{
    public GameObject[] cubes;
    public Transform[] points;
    public GameObject[] noteTypes;
    
    public float speed;

    public float speedOfCubeSizing = 100f;

    public Rigidbody rb;

    /// <summary>
    /// /
    /// </summary>
    private float beat = (60 / 72);
    private float timer;
    private int whichPoint;

    // Counts on which to inhale or exhale. More or less no. of counts can be added, for eg: {1,1} or {1,1,1,1,1,1,1,1}
    private int[] in4Counts = { 1, 1, 1, 1 };
    private int[] out4Counts = { 0, 0, 0, 0 };
    private int[] videoBreak = new int[36];
    public List<int> spawnPoints = new List<int>();     // This is where the whole exercise is created
    public int listIndex = 0;

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

    public GameObject videoPlayer;

    // Chords need to be added to this list
    private List<int> chords = new List<int>();
    public List<float> counts = new List<float>();

    private float beatUpdater;
    public GameObject song;
    private Vector3 sevenBeatSpawnPos;
    private void Awake()
    {
        for (int i = 0; i < 36; i++)
        {
            videoBreak[i] = 1;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        beatUpdater = beat;
        //videoPlayer = GameObject.Find("VideoPlayer");
        videoPlayer.SetActive(false);

        rb = GetComponent<Rigidbody>();

        //setting the exercise below is hard coded here
        #region Set the Exercise
        // Set the breathing exercise pattern here
        //Hey Jude
        AddCountsToList(1, bbLow, 1);
        AddCountsToList(0, bbHigh, 0.5f);
        AddCountsToList(0, cHigh, 0.5f);
        AddCountsToList(0, cHigh, 0.5f);
        AddCountsToList(0, bbHigh, 0.5f);
        AddCountsToList(0, cHigh, 0.5f);
        AddCountsToList(0, cHigh, 0.5f);
        // Don't make it bad
        AddCountsToList(1, bbLow, 1);
        AddCountsToList(0, cHigh, 0.5f);
        AddCountsToList(0, cHigh, 0.5f);
        AddCountsToList(0, bbHigh, 0.5f);
        AddCountsToList(0, cHigh, 0.5f);
        AddCountsToList(0, cHigh, 0.5f);
        AddCountsToList(0, bbHigh, 0.5f);
        // Take a sad song and
        AddCountsToList(1, bbLow, 1);
        AddCountsToList(0, cHigh, 0.5f);
        AddCountsToList(0, bbHigh, 0.5f);
        AddCountsToList(0, bbHigh, 0.5f);
        AddCountsToList(0, cHigh, 0.5f);
        AddCountsToList(0, cHigh, 0.5f);
        AddCountsToList(0, bbHigh, 0.5f);
        // Make it better
        AddCountsToList(1, bbLow, 1);
        AddCountsToList(0, bbHigh, 0.5f);
        AddCountsToList(0, bbHigh, 0.5f);
        AddCountsToList(0, cHigh, 0.5f);
        AddCountsToList(0, cHigh, 0.5f);
        AddCountsToList(0, bbHigh, 0.5f);
        AddCountsToList(0, bbHigh, 0.5f);
        // Remember to let her into
        AddCountsToList(1, bbLow, 1);
        AddCountsToList(0, bbHigh, 0.5f);
        AddCountsToList(0, bbHigh, 0.5f);
        AddCountsToList(0, cHigh, 0.5f);
        AddCountsToList(0, bbHigh, 0.5f);
        AddCountsToList(0, bbHigh, 0.5f);
        AddCountsToList(0, bbHigh, 0.5f);
        // Your heart then
        AddCountsToList(1, bbLow, 1);
        AddCountsToList(0, cHigh, 0.5f);
        AddCountsToList(0, cHigh, 0.5f);
        AddCountsToList(0, bbHigh, 0.5f);
        AddCountsToList(0, bbHigh, 0.5f);
        AddCountsToList(0, cHigh, 0.5f);
        AddCountsToList(0, cHigh, 0.5f);
        // you can start C7
        AddCountsToList(1, bbLow, 1);
        AddCountsToList(0, bbHigh, 0.5f);
        AddCountsToList(0, bbHigh, 0.5f);
        AddCountsToList(0, cHigh, 0.5f);
        AddCountsToList(0, cHigh, 0.5f);
        AddCountsToList(0, bbHigh, 0.5f);
        AddCountsToList(0, cHigh, 0.5f);
        // to make it better
        AddCountsToList(1, bbLow, 1);
        AddCountsToList(0, bbHigh, 0.5f);
        AddCountsToList(0, bbHigh, 0.5f);
        AddCountsToList(0, cHigh, 0.5f);
        AddCountsToList(0, cHigh, 0.5f);
        AddCountsToList(0, bbHigh, 0.5f);
        AddCountsToList(0, cHigh, 0.5f);
        // Hey Jude dont be afraid, you were made to, go out and get her , the minute, you let her under your skin, then you begin, to make it better
        VideoBreak(videoBreak, 10);
        // Pain hey jude refrain
        AddCountsToList(1, bbLow, 1);
        AddCountsToList(0, bbHigh, 7);
        AddCountsToList(1, bbLow, 1);
        AddCountsToList(0, cHigh, 7);



        #endregion 
        timer = beat;

    }

    private int noteToSpawn;
    // Update is called once per frame
    void Update()
    {
        VideoPlayer();
        // Logic to instantiate cubes at the right beat and position
        if (timer > beatUpdater)
        {
            /*Debug.Log("listIndex: " + (listIndex));
            Debug.Log("counts[listIndex]: " + counts[listIndex]);
            Debug.Log("Timer: " + timer);
            Debug.Log("Beat: " + beat);*/         
            
            ResizeOn(cubes[chords[listIndex]], counts[listIndex]);
            GameObject cubeSpawn = Instantiate(cubes[chords[listIndex]], points[spawnPoints[listIndex]] );
            Debug.Log(points[spawnPoints[listIndex]].GetType()); 
            if (counts[listIndex] == 1)
            {
                noteToSpawn = 0;

            }
            if(counts[listIndex] == 0.5f)
            {
                noteToSpawn = 1;
            }
            //GameObject noteSpawn = Instantiate(noteTypes[noteToSpawn], points[spawnPoints[listIndex]]);
            if (counts[listIndex] == 7)
            {
                noteToSpawn = 2;
                //GameObject noteSpawn = Instantiate(noteTypes[noteToSpawn], points[3]);
            }
            //GameObject noteSpawn = Instantiate(noteTypes[noteToSpawn], points[spawnPoints[listIndex]]);

            //GameObject noteSpawn = Instantiate(noteTypes[noteToSpawn], )

            timer -= beatUpdater;
            beatUpdater = beat * counts[listIndex];
            Debug.Log("LATER BEAT: " + beatUpdater);

            listIndex++;
        }
        timer += Time.deltaTime;
    }

    void AddCountsToList(int addCounts, int chord, float howManyCounts)
    {        
        spawnPoints.Add(addCounts);
        chords.Add(chord);
        counts.Add(howManyCounts);        
    }

    void VideoBreak(int[] addCounts, int chord)
    {

        for (int i = 0; i < addCounts.Length; i++)
        {
            spawnPoints.Add(addCounts[i]);
            chords.Add(chord);
            counts.Add(1);
        }
    }


    private void ResizeOn(GameObject whatToResize, float howManyCounts)
    {
        whatToResize.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * howManyCounts);
    }

    private void VideoPlayer()
    {
        if (listIndex >58 && listIndex < 92)
        {
            videoPlayer.SetActive(true);
            song.GetComponent<AudioSource>().volume = 0.1f;
        }
        else
        {
            song.GetComponent<AudioSource>().volume = 0.6f;
            videoPlayer.SetActive(false);
        }
    }

}

