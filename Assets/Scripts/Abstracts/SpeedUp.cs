using DG.Tweening;
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
        ship.currentSpeed *= 2;
        yield return new WaitForSeconds(2);
        ship.currentSpeed = ship.speed;
    }
}

public class Stun : SkillBase
{

    public override void Use(ShipBase ship)
    {

        Shake(ship, 1, 20, 1, 1);

    }

    public void Shake(ShipBase ship, float duration, float strength, int vibrato, float randomness)
    {

        ship.transform.DOShakeRotation(duration, strength, vibrato, randomness);
        _ = new Slowly(ship);

    }

}

public class HealthDown : SkillBase
{

    public override void Use(ShipBase ship)
    {

        ship.hp -= 40;

    }

}

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

