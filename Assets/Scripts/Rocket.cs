using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    [SerializeField] float rcsThrust = 100f;    //Used for power of rotation of the rocket
    [SerializeField] float mainThrust = 100f;   //Used for power of main thrust of rocket
    [SerializeField] GameObject rotatingObstacle;   //The rotating obstacle in the scene

    [SerializeField] AudioClip mainEngineSound;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip winningSound;

    [SerializeField] ParticleSystem engineParticles;
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] ParticleSystem winningParticles;

    [SerializeField] float loadLevelDelay = 1f;     //Determines how long in seconds to load next level

    Vector3 rotatingObstacleVector;     //The initial popsition of the rotating obstacle in the scene
    Rigidbody rigidBody;
    AudioSource sound;
    Transform rotation;
    int currentLevel;

    //Two states that the ship can possibly be in
    enum RocketStatus
    {
        Alive,
        Dead,
        Transcending
    }

    RocketStatus status = RocketStatus.Alive;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
        rotation = GetComponent<Transform>();

        //Saving initial position of rotating obstacle
        rotatingObstacleVector = rotatingObstacle.transform.position;
        status = RocketStatus.Alive;
        getCurrentLevel();
	}

    //Gets the current level loaded
    private void getCurrentLevel()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (status == RocketStatus.Alive)
        {
            Thrust();
            Rotate();
        }
        
        obstaclePosition();
    }

    //Moves the position of the rocket in the direction it's facing
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            //Makes rocket thrust frame independent
            rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

            if (!sound.isPlaying)
            {
                sound.PlayOneShot(mainEngineSound);
                engineParticles.Play();
            }
        }
        else
        {
            sound.Stop();
            engineParticles.Stop();
        }
    }

    //Changes the direction if the ship
    private void Rotate()
    {
        rigidBody.freezeRotation = true;

        float rotationSpeed = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow)) //Rotate ship to the left
        {
            rotation.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.RightArrow)) //Rotate ship to the right
        {
            rotation.Rotate(Vector3.back * rotationSpeed);
        }

        rigidBody.freezeRotation = false;
    }

    /// <summary>
    /// Changes the rotating obstacle position depending on the location of the ship
    /// </summary>
    private void obstaclePosition()
    {
        //If the rocket goes higher than 12.16 on the y coordinate and beyond -12.16 on the x coordinate
        if (transform.position.y >= 12.16f && transform.position.x >= -12.16f)
        {
            //Stop the rotation
            rotatingObstacle.transform.rotation = new Quaternion(0, 0, 0, 0);
            //Move the obstacle
            rotatingObstacle.transform.position = new Vector3(rotatingObstacle.transform.position.x, 16.31f, 0);    
        }
        else
        {
            //Move the obstacle to starting position
            rotatingObstacle.transform.position = rotatingObstacleVector;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.ToLower() != "friendly")
        {
            if (collision.gameObject.tag.ToLower() == "finish") //If ship reached landing pad
            {
                sound.Stop();
                status = RocketStatus.Transcending;
                sound.PlayOneShot(winningSound);
                winningParticles.Play();
                Invoke("changeLevel", loadLevelDelay);
            }
            else // If ship touched an obstacle
            {
                sound.Stop();
                status = RocketStatus.Dead;
                sound.PlayOneShot(explosionSound);
                explosionParticles.Play();
                Invoke("changeLevel", loadLevelDelay);
            }
        }
    }

    //Changes level depending on whether player died or passed
    private void changeLevel()
    {
        if (status == RocketStatus.Transcending)
        {
            //Load next level
            if (currentLevel == 2)
            {

            }
            else
            {
                SceneManager.LoadScene(currentLevel + 1);
            }
            
        }
        else if (status == RocketStatus.Dead)
        {
            //Load level 1
            SceneManager.LoadScene(0);
        }
    }

    
}
