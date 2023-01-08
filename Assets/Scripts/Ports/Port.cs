using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out A�Ship a�Ship))
        {
            if (a�Ship.ports.Contains(transform))
                a�Ship.ports.Remove(transform);
        }
    }
}
