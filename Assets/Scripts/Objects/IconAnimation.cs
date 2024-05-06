using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconAnimation : MonoBehaviour
{
    public float speed = 1.0f;
    public float height = 1.0f;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        transform.localScale = Vector3.one * 4;
    }

    void Update()
    {
        
        float newY = startPos.y + Mathf.PingPong(Time.time * speed, height);
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

        



}
