using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {

    [SerializeField] Transform rocket;
    [SerializeField] float relativeZ;
    [SerializeField] float relativeY;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 rocketPosition = rocket.transform.position;
        transform.position = new Vector3(rocketPosition.x, rocketPosition.y + relativeY, rocketPosition.z - relativeZ);
    }
}
