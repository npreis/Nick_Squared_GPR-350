﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float mSpeed = 3.0f;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        var d = Input.GetAxis("Mouse ScrollWheel");
        var zoom = 0.1f;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(new Vector3(mSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(new Vector3(-mSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.Translate(new Vector3(0, mSpeed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.Translate(new Vector3(0, -mSpeed * Time.deltaTime, 0));
        }
        if(d > 0)
        {
            Camera.main.orthographicSize -= zoom;
            if(Camera.main.orthographicSize <= 1.0)
            {
                Camera.main.orthographicSize = 1.0f;
            }
        }
        if(d < 0)
        {
            Camera.main.orthographicSize += zoom;
        }
    }
}
