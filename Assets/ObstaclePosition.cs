using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstaclePosition : MonoBehaviour {

    [SerializeField] Vector3 movementVector;
    [Range(0,1)] [SerializeField] float movementFactor;
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
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            startingPosition = transform.position;
            movement = Direction.Up;
            moveObstacles = true;
        }
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
        movementFactor += -0.01f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = offset + startingPosition;
    }

    private void moveObstacleUp()
    {
        movementFactor += 0.01f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = offset + startingPosition;
    }

}
