using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSlower : RocketBase
{
    public override void Use(ShipBase ship)
    {
        base.Use(ship);
        ship.Slowing();
    }
}
