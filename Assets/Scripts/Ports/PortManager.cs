using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortManager : MonoBehaviour
{
    public List<Transform> ports;
    public static PortManager instance;
    private void Awake()
    {
        instance = this;
    }
}