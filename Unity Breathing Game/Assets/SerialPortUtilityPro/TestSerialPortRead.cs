using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSerialPortRead : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    //Example Read Data : AAA,BBB,CCC,DDD<CR><LF>
    //for List data
    public void ReadComplateList(object data)
    {
        var text = data as List<string>;
        
        for (int i = 0; i < text.Count; ++i)
            Debug.Log(text[i]);


            
    }
}
