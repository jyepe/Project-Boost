using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstaclePosition : MonoBehaviour {

    Rigidbody body;
    int currentLevel;

    enum Moving
    {
        Up,
        Down,
        Still
    }
    
    Moving direction;

	// Use this for initialization
	void Start ()
    {
        body = GetComponent<Rigidbody>();
        getCurrentLevel();
        Direction();
    }

    private void Direction()
    {
        if (currentLevel == 0)
        {
            direction = Moving.Still;
        }
        else
        {
            body.useGravity = false;
            direction = Moving.Up;
        }
    }

    private void getCurrentLevel()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }
	
	// Update is called once per frame
	void Update () {
        if (currentLevel == 1)
        {
            body.AddRelativeForce(new Vector3(0, 1, 0));
        }
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (currentLevel == 1)
        {
            if (direction == Moving.Up)
            {
                direction = Moving.Down;
                body.useGravity = true;
            }
            else
            {
                direction = Moving.Up;
                body.useGravity = false;
            }
        }
        
    }
}
