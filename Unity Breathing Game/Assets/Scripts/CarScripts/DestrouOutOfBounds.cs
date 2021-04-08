using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestrouOutOfBounds : MonoBehaviour
{
    public float bound = -7;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyGameObject();
    }

    void DestroyGameObject()
    {
        if(transform.position.z < bound)
        {
            //Debug.Log("Destruction?");
            Destroy(gameObject);
        }
    }

}
