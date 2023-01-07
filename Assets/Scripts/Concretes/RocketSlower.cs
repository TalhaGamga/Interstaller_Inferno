using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSlower : RocketBase
{

    float interval = 1;

    public override void Use(ShipBase ship)
    {
        base.Use(ship);
        StartCoroutine(IESlowing(interval));
    }

    private IEnumerator IESlowing(float interval)
    {
        float temp = 1;

        temp *= .5f;
        yield return new WaitForSeconds(interval);
        temp *= 2f;
    }
}
