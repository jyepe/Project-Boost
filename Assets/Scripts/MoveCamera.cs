using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] [Range(0,1)] float movementSpeed;
    Vector3 startingPosition;

    // Use this for initialization
    void Start () {
        startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        cameraMovement();
	}

    private void cameraMovement()
    {
        movementFactor += movementSpeed;
        Vector3 offset = movementVector * movementFactor;
        transform.position = offset + startingPosition;
    }
}
