using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSlower : RocketBase
{

    float interval;

    
    private IEnumerator IESlowing(float interval)
    {
        float temp = 1;

        temp *= .5f;
        yield return new WaitForSeconds(interval);
        temp *= 2f;
    }
}
