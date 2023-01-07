using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketStunner : RocketBase
{
    public override void Use(ShipBase ship)
    {
        base.Use(ship);
        Shake(ship, 1, 20, 1, 1);
    }

    public void Shake(ShipBase ship, float duration, float strength, int vibrato, float randomness)
    {

        ship.transform.DOShakeRotation(duration, strength, vibrato, randomness);
        _ = new Slowly(ship);

    }
}
