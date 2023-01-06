using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipCollisions : MonoBehaviour
{
    [SerializeField] ShipBase ship;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<CollectableBase>(out CollectableBase collectableBase))
        {
            collectableBase.Use(ship);
        }
    }
}
