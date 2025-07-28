using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementTest : MonoBehaviour
{
    
    private float _speed = 0.02f;
    
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
    

    private void FixedUpdate()
    {
        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = 0.1f;
        }
        else
        {
            _speed = 0.02f;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * _speed);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * _speed);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * _speed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * _speed);
        }
        

    }
}
