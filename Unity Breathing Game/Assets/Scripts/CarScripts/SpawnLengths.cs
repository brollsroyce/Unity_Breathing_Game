using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLengths : MonoBehaviour
{
    public float maxScale = -10f;
    public float speed = 100f;


    public Rigidbody rb;


    private Vector3 v3OrgPos;
    private float orgScale;
    private float endScale;

    void Awake()
    {
        v3OrgPos = transform.position - transform.forward;
        orgScale = transform.localScale.z;
        endScale = orgScale;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        transform.position += Time.deltaTime * transform.forward * 2;
        speed = rb.velocity.magnitude;
        ResizeOn();
    }

    private void ResizeOn()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Mathf.MoveTowards(transform.localScale.z, endScale, Time.deltaTime * speed));
        transform.position = v3OrgPos + (transform.forward) * (transform.localScale.z / 2.0f + orgScale / 2.0f);
        endScale = maxScale;
        /*if (Input.GetKeyDown(KeyCode.S))
        {
            endScale = maxScale;
        }*/
        /*else if (Input.GetKeyDown(KeyCode.R))
        {
            endScale = orgScale;
        }*/
    }
}
