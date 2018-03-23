using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour {


    [SerializeField] Transform target;  //The rocket

    [SerializeField] Vector3 offset;    //Will be used to determine where the camera is positioned

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 desiredPosition = target.transform.position + offset;
        transform.position = desiredPosition;
        transform.LookAt(target.transform.position);
    }
}
