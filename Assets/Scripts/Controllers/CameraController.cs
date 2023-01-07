using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    [SerializeField] float backOffSet;
    [SerializeField] float upOffSet;
    Vector3 posToGo;
    void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        posToGo = player.transform.position - transform.forward * backOffSet + Vector3.up * upOffSet;
        posToGo = Vector3.Lerp(transform.position, posToGo, 0.06f);

        transform.position = posToGo;

        transform.LookAt(player.transform.position + player.transform.forward * 250f);
    }
}
