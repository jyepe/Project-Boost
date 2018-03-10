using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Swing : MonoBehaviour {
    
    int currentAngle;
    [SerializeField] float rotationSpeed = 0;

    enum Swinging
    {
        Right,
        Left,
        None
    }

    Swinging direction;

	// Use this for initialization
	void Start () {
        direction = Swinging.None;
    }
	
	// Update is called once per frame
	void Update ()
    {
        currentAngle = Convert.ToInt32(transform.eulerAngles.z);
        directionToSwing();
    }

    private void directionToSwing()
    {
        if (direction == Swinging.None)
        {
            direction = Swinging.Right;
        }
        else if (direction == Swinging.Right)
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed));

            if (currentAngle == 30)
            {
                direction = Swinging.Left;
            }
        }
        else if (direction == Swinging.Left)
        {
            transform.Rotate(new Vector3(0, 0, -rotationSpeed));

            if (currentAngle == 330)
            {
                direction = Swinging.Right;
            }
        }
    }
}
