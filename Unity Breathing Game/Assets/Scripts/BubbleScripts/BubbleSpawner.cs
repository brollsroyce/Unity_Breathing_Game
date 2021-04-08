using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawner : MonoBehaviour
{
    BubbleBlowingMovement bubbleBlowingMovementScript;
    Callibration calScript;
    // Start is called before the first frame update
    void Start()
    {
        bubbleBlowingMovementScript = GameObject.FindObjectOfType<BubbleBlowingMovement>();
        calScript = GameObject.FindObjectOfType<Callibration>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void SpawnBubbles()
    {
        // Everytime isInhaling becomes true after exhaling, spawn new bubble
    }

    public void DestroyBubble()
    {

    }
}
