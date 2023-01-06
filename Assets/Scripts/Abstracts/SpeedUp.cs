using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : SkillBase
{
    public override void Use(ShipBase ship)
    {
        StartCoroutine(IEChangeSpeed(ship));
    }

    IEnumerator IEChangeSpeed(ShipBase ship)
    {
        ship.currentSpeed*=2;
        yield return new WaitForSeconds(2);
        ship.currentSpeed = ship.speed;
    }
}
