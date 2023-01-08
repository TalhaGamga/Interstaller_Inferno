using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HpBoostCS : CollectableSkillBase
{

    public override Tween Use(ShipBase ship)
    {
        ship.hp += 40;//todo
        return base.Use(ship);

    }

}
