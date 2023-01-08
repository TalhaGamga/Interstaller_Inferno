using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Port : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out AýShip aýShip))
        {
            if (aýShip.ports.Contains(transform))
                aýShip.ports.Remove(transform);
        }
    }
}
