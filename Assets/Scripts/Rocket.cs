using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    [SerializeField] float rcsThrust = 100f;    //Used for power of rotation of the rocket
    [SerializeField] float mainThrust = 100f;   //Used for power of main thrust of rocket

    [SerializeField] AudioClip mainEngineSound;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] AudioClip winningSound;

    [SerializeField] ParticleSystem engineParticles;
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] ParticleSystem winningParticles;

    [SerializeField] float loadLevelDelay = 1f;     //Determines how long in seconds to load next level
    
    Rigidbody rigidBody;
    AudioSource sound;
    Transform rotation;

    //Three states that the ship can possibly be in
    enum RocketStatus
    {
        Alive,
        Dead,
        Transcending
    }

    RocketStatus status = RocketStatus.Alive;

    enum RocketFlight
    {
        Flying,
        Still
    }

    RocketFlight state = RocketFlight.Still;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        sound = GetComponent<AudioSource>();
        rotation = GetComponent<Transform>();
        
        status = RocketStatus.Alive;
        getCurrentLevel();
	}

    //Gets the current level loaded
    private int getCurrentLevel()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (status == RocketStatus.Alive)
        {
            shouldRocketFly();
            Rotate();
        }

        if (state == RocketFlight.Flying)
        {
            flyRocket();
        }
    }

    //Moves the position of the rocket in the direction it's facing
    private void shouldRocketFly()
    {
        if (Input.GetKey(KeyCode.Space) && state == RocketFlight.Still)
        {
            state = RocketFlight.Flying;
        }
        else if (Input.GetKey(KeyCode.Space) && state == RocketFlight.Flying)
        {
            state = RocketFlight.Still;
            sound.Stop();
            engineParticles.Stop();
        }
    }

    //Keeps the rocket at a stable altitude
    private void flyRocket()
    {
        //Makes rocket thrust frame independent
        rigidBody.AddRelativeForce(Vector3.back * mainThrust * Time.deltaTime);

        if (transform.position.y > 40f)
        {
            rigidBody.AddRelativeForce(Vector3.forward * mainThrust * Time.deltaTime);
        }

        if (!sound.isPlaying)
        {
            sound.PlayOneShot(mainEngineSound);
            engineParticles.Play();
        }
    }

    //Changes the direction if the ship
    private void Rotate()
    {
        rigidBody.freezeRotation = true;

        float rotationSpeed = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A)) //Rotate ship to the left
        {
            rotation.Rotate(Vector3.forward * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D)) //Rotate ship to the right
        {
            rotation.Rotate(Vector3.back * rotationSpeed);
        }
        else if (Input.GetKey(KeyCode.W)) //Move forward
        {
            rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.S)) //Move back
        {
            rigidBody.AddRelativeForce(Vector3.down * mainThrust * Time.deltaTime);
        }

        rigidBody.freezeRotation = false;
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
            if (getCurrentLevel() == 3)
            {
                SceneManager.LoadScene(getCurrentLevel());
            }
            else
            {
                SceneManager.LoadScene(getCurrentLevel() + 1);
            }
            
        }
        else if (status == RocketStatus.Dead)
        {
            //Load current level
            SceneManager.LoadScene(getCurrentLevel());
        }
    }

    
}
