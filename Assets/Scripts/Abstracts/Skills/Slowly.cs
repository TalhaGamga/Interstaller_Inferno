using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Slowly : SkillBase
{
    public Slowly(ShipBase ship)
    {
        Use(ship);
    }


    public override void Use(ShipBase ship)
    {
        StartCoroutine(IEChangeSpeed(ship));
    }
    IEnumerator IEChangeSpeed(ShipBase ship)
    {
        ship.currentSpeed *= 0.5f;
        yield return new WaitForSeconds(2);
        ship.currentSpeed = ship.speed;
    }

}