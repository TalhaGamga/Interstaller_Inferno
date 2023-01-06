using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketStunner : RocketBase
{
    public override void Fire(ShipBase ship)
    {
        base.Fire(ship);
        ship.Stun();
    }
}
