using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAudios : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;

    private Renderer rend;

    Callibration calIbrationScript;

    GameObject[] plsDisappear;

    // Start is called before the first frame update
    void Start()
    {
        calIbrationScript = GameObject.FindObjectOfType<Callibration>();

        audioSource = GetComponent<AudioSource>();

        rend = GetComponent<Renderer>();
        rend.enabled = true;

        plsDisappear = GameObject.FindGameObjectsWithTag("Bubble");
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if(!(gameObject.tag == "Block") &&  other.tag == "Player")
 
        {
            //Debug.Log("Destroyer");
            audioSource.PlayOneShot(audioClip);
            
            rend.enabled = false;
            
            
            foreach(GameObject pls in plsDisappear)
            {
                Destroy(pls);
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(this.gameObject);
            audioSource.Stop();
        }
    }
    


}
