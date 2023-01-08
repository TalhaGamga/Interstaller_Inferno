using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    Queue<RocketBase> rockets = new Queue<RocketBase>();
    public void Add(RocketBase rocket)
    {
        rockets.Enqueue(rocket);
    }

    public void Fire(ShipBase ship)
    {
        if (rockets.Count>0)
        {
            RocketBase rocket = rockets.Dequeue();
        }
    }


}
