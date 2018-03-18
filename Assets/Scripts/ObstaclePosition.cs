using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstaclePosition : MonoBehaviour {

    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] [Range(0,1)] float movementSpeed;
    Boolean moveObstacles;
    Vector3 startingPosition;

    enum Direction
    {
        Up,
        Down
    }

    Direction movement;

	// Use this for initialization
	void Start ()
    {
        startingPosition = transform.position;
        movement = Direction.Up;
        moveObstacles = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (moveObstacles)
        {
            checkDirection();
        }
    }

    private void checkDirection()
    {
        if (movement == Direction.Up)
        {
            moveObstacleUp();

            if (movementFactor >= 1f)
            {
                movement = Direction.Down;
            }
        }
        else
        {
            moveObstacleDown();

            if (movementFactor <= 0f)
            {
                movement = Direction.Up;
            }
        }
    }

    private void moveObstacleDown()
    {
        movementFactor += -movementSpeed;
        Vector3 offset = movementVector * movementFactor;
        transform.position = offset + startingPosition;
    }

    private void moveObstacleUp()
    {
        movementFactor += movementSpeed;
        Vector3 offset = movementVector * movementFactor;
        transform.position = offset + startingPosition;
    }

}
