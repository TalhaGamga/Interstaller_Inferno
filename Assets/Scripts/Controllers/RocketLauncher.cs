using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public List<Transform> slots;
    [SerializeField]
    ShipManager shipManager;
    private void OnEnable()
    {
        shipManager.onFireAction += Fire;
        
    }
    private void OnDisable()
    {
        shipManager.onFireAction -= Fire;
    }
    Queue<RocketBase> rockets = new Queue<RocketBase>();
    public void Add(RocketBase rocket)
    {
        if (!rocket.isAdding)
        {
            rockets.Enqueue(rocket);
        }
     
    }

    public void Fire(ShipBase sourceShip)
    {
        if (rockets.Count>0)
        {
            RocketBase rocket = rockets.Dequeue();
            rocket.Fire(sourceShip);
        }
    }


}
