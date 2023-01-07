using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBoostCS : CollectableSkillBase
{

    public override void Use(ShipBase ship)
    {

        ship.hp += 40;//todo

    }

}
