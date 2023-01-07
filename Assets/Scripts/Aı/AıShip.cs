using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AıShip : ShipBase
{
    public StateAı currentState;
    public StateAı fireState=new FireState(), fightState=new FightState(), followState=new FollowState();

    
    private void Update()
    {
        currentState.UpdateState(this);
    }
    private void SwitchState(StateAı switchAı)
    {
        currentState = switchAı;
    }
    public void MoveRight()
    {
        
    }
    public void MoveLeft()
    {

    }
    public void MoveUp()
    {

    }
    public void MoveDown()
    {

    }
}
public abstract class StateAı
{
    public abstract void EnterState(AıShip aıShip);

    public abstract void UpdateState(AıShip aıShip);
}
public class FireState : StateAı
{
    public override void EnterState(AıShip aıShip)
    {
        
    }
    public override void UpdateState(AıShip aıShip)
    {
        
    }
}
public class FightState : StateAı
{
    public override void EnterState(AıShip aıShip)
    {
        
    }
    public override void UpdateState(AıShip aıShip)
    {
        
    }
}
public class FollowState : StateAı
{
    Collider[] collider;
    int x;

    public override void EnterState(AıShip aıShip)
    {
        
    }
    public override void UpdateState(AıShip aiShip)
    {
         x= Physics.OverlapSphereNonAlloc(aiShip.transform.position, 3f, collider, 1);
        for (int i = 0; i <x; i++)
        {

        }
    }
}
