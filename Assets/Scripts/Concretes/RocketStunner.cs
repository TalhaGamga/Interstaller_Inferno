using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketStunner : RocketBase
{
    [SerializeField] Camera main;
    public override void Use(ShipBase ship)
    {
        base.Use(ship);
        Shake(ship, 1, 100, 10, 1);
    }

    public void Shake(ShipBase ship, float duration, float strength, int vibrato, float randomness)
    {
        main.transform.DOShakePosition(duration, strength, vibrato, randomness);
        StartCoroutine(Stun(ship));
    }

    IEnumerator Stun(ShipBase ship)
    {
        ship.isStunned = true;
        ship.speed = 0;
        yield return new WaitForSeconds(3f);
        ship.speed = ship.normalSpeed;
    }
}
