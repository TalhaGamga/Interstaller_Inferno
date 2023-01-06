using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSlower : RocketBase
{
    public override void Fire(ShipBase ship)
    {
        base.Fire(ship);
        ship.Slow(1f);
    }
}
