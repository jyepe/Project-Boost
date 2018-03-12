using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour {

    Transform rotation;

	// Use this for initialization
	void Start () {
        rotation = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        rotation.Rotate(Vector3.forward);
	}
}
