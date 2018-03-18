using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootUp : MonoBehaviour {

    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] [Range(0,1)] float movementSpeed;
    Vector3 startingPosition;

    enum Direction
    {
        Up,
        Down
    }

    Direction movement;

    // Use this for initialization
    void Start () {
        startingPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        checkDirection();


        if (movementFactor >= 1f)
        {
            moveObstacleDown();
        }
        else if (movementFactor <= 0f)
        {
            moveObstacleUp();
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
            Invoke("moveObstacleDown", 2f);

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
