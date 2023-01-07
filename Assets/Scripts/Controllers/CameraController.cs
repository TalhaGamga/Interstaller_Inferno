using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //public GameObject player;
    //[SerializeField] float backOffSet;
    //[SerializeField] float upOffSet;
    //Vector3 posToGo;
    //void LateUpdate()
    //{
    //    FollowPlayer();
    //}

    //void FollowPlayer()
    //{
    //    posToGo = player.transform.position - transform.forward * backOffSet + Vector3.up * upOffSet;
    //    posToGo = Vector3.Lerp(transform.position, posToGo, 0.1f);

    //    //transform.position += Vector3.Lerp(transform.position, player.transform.position + transform.forward, 0.1f);

    //    transform.position = posToGo;

    //    transform.LookAt(player.transform.position + player.transform.forward * 250f);
    //}
    public Transform target; // the transform of the object to follow
    public float smoothTime = 0.3f; // the time it takes for the camera to catch up with the target
    public Vector3 offset; // the distance between the camera and the target

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        // move the camera towards the target by a certain amount each frame
        transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, smoothTime);
        transform.LookAt(target);   
    }
}