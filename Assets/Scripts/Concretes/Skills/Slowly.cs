using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Slowly : CollectableSkillBase
{
    public override Tween Use(ShipBase ship)
    {
        StartCoroutine(IEChangeSpeed(ship));
        return base.Use(ship);
    }
    IEnumerator IEChangeSpeed(ShipBase ship)
    {
        ship.speed *= 0.5f;
        yield return new WaitForSeconds(2);
        ship.speed = ship.normalSpeed;
    }
}
