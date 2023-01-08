using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFirable
{
    void Fire();
    void GiveDamage(ShipBase Source);
    void Move(ShipBase Source);
}
public enum MoveX
{
    left,
    right,
}
public enum MoveY
{
    up,
    down
}
public interface IObstacle
{

}