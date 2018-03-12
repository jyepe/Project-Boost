using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootUp : MonoBehaviour {

    [SerializeField] Vector3 movementVector;
    float movementFactor;
    Vector3 startingPosition;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        startingPosition = transform.position;

        if (movementFactor == 1f)
        {
            moveObstacleDown();
        }
        else
        {
            moveObstacleUp();
        }
        
    }

    private void moveObstacleDown()
    {
        movementFactor += -0.01f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = offset + startingPosition;
    }

    private void moveObstacleUp()
    {
        movementFactor += 0.011f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = offset + startingPosition;
    }
}
