using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFirable
{
    void Fire(ShipBase Source);
    void Move(ShipBase Source);
}