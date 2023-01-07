using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDown : SkillBase
{

    public override void Use(ShipBase ship)
    {

        ship.hp -= 40;

    }

}
