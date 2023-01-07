using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpCS : CollectableSkillBase
{
    public override void Use(ShipBase ship)
    {
        StartCoroutine(IEChangeSpeed(ship));
    }

    IEnumerator IEChangeSpeed(ShipBase ship)
    {
        ship.normalSpeed *= 2;
        yield return new WaitForSeconds(2);
        ship.normalSpeed = ship.speed;
    }
}
