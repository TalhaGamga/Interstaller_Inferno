using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target; // the transform of the object to follow
    public float smoothTime = 0.3f; // the time it takes for the camera to catch up with the target
    public Vector3 offset; // the distance between the camera and the target

    private Vector3 velocity = Vector3.zero;

    void LateUpdate()
    { 
        transform.position =    Vector3.SmoothDamp(transform.position, target.transform.position-target.transform.forward*30 + target.transform.up*3, ref velocity, smoothTime);
        transform.LookAt(target.position + target.transform.forward * 200);
    }
}