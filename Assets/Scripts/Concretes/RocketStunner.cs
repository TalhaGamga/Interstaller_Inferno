using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketStunner : RocketBase
{ 
    public override void Use(ShipBase ship)
    {
        base.Use(ship);
        ship.Stun();
    }
}
