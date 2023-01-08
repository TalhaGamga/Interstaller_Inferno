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
        Debug.Log("Sped Up");
        ship.speed *= 2;
        yield return new WaitForSeconds(5);
        ship.speed = ship.normalSpeed;
    }
}
