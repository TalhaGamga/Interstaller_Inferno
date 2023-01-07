using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AıShip : ShipBase
{
    public StateAı currentState;
    public StateAı fireState = new FireState(), fightState = new FightState(), followState = new FollowState();
    private Move move;

    public float radarForwardSize = 4, radarUpSize = 10;
    public float multiple = 10.0f;
    Vector3 planePos;
    private float rotationSpeed;
    public float RotationSpeed
    {
        get => rotationSpeed; set
        {
            if (value > 1) {
                rotationSpeed = 1;
            }
            if (value<=0)
            {
                rotationSpeed = 0;
            }
        }
    }

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
        if (Move.left == move)
        {
            RotationSpeed -= Time.deltaTime * 3f;
            if (RotationSpeed == 0)
            {
                move = Move.right;
            }
        }
        else
        {
            RotationSpeed += Time.deltaTime;
        }
        planePos = transform.position;
        planePos.x += multiple * Time.deltaTime;
        transform.position = planePos;
        transform.rotation = Quaternion.Euler(0, 0, -45*rotationSpeed);
    }
    public void MoveLeft()
    {
        if (Move.right==move)
        {
            RotationSpeed -= Time.deltaTime*3f;
            if (RotationSpeed==0)
            {
                move = Move.left;
            }
        }
        else
        {
            RotationSpeed += Time.deltaTime;
        }
        planePos = transform.position;
        planePos.x -= multiple * Time.deltaTime;
        transform.position = planePos;     
        transform.rotation = Quaternion.Euler(0, 0, 45*RotationSpeed);
    }


    public void MoveUp()
    {
        if (Move.down == move)
        {
            RotationSpeed -= Time.deltaTime * 3f;
            if (RotationSpeed == 0)
            {
                move = Move.up;
            }
        }
        else
        {
            RotationSpeed += Time.deltaTime;
        }
        planePos = transform.position;
        planePos.y += multiple * Time.deltaTime;
        transform.position = planePos;
        transform.rotation = Quaternion.Euler(-45* RotationSpeed, 0, 0);
    }

    public void MoveDown()
    {
        if (Move.up == move)
        {
            RotationSpeed -= Time.deltaTime * 3f;
            if (RotationSpeed == 0)
            {
                move = Move.down;
            }
        }
        else
        {
            RotationSpeed += Time.deltaTime;
        }
        planePos = transform.position;
        planePos.y -= multiple * Time.deltaTime;
        transform.position = planePos;
        transform.rotation = Quaternion.Euler(45* RotationSpeed, 0, 0);
    }
}

public abstract class StateAı
{
    public abstract void EnterState(AıShip aiShip);

    public abstract void UpdateState(AıShip aiShip);
}

public class FireState : StateAı
{
    public override void EnterState(AıShip aiShip)
    {

    }
    public override void UpdateState(AıShip aiShip)
    {

    }
}

public class FightState : StateAı
{
    public override void EnterState(AıShip aiShip)
    {

    }
    public override void UpdateState(AıShip aiShip)
    {

    }
}
public class FollowState : StateAı
{
    Transform buffTransform;
    Collider[] hits = new Collider[50];
    int hitSize;
    float maxSize = float.MaxValue, temp;
    public override void EnterState(AıShip aiShip)
    {

    }
    public override void UpdateState(AıShip aiShip)
    {
        Follow(aiShip, ClosestBuff(aiShip));
    }
    private Transform ClosestBuff(AıShip aiShip)
    {
        maxSize = float.MaxValue;
        hitSize = Physics.OverlapBoxNonAlloc(aiShip.transform.position, new Vector3(1, aiShip.radarUpSize, aiShip.radarForwardSize), hits, Quaternion.identity);
        if (hitSize == 0)
        {
            return null;
        }
        for (int i = 0; i < hitSize; i++)
        {
            temp = Vector3.Distance(aiShip.transform.position, hits[i].transform.position);
            if (maxSize < temp)
            {
                maxSize = temp;
                buffTransform = hits[i].transform;
            }
        }
        return buffTransform;
    }
    private void Follow(AıShip aiShip, Transform followTransform)
    {
        if (.1f > aiShip.transform.position.x - followTransform.position.x)
        {
            aiShip.MoveLeft();
        }else
        if (-.1f < aiShip.transform.position.x - followTransform.position.x)
        {
            aiShip.MoveRight();
        }
        else
        {
            aiShip.RotationSpeed = 0;
        }
        if (.1f > aiShip.transform.position.y - followTransform.position.y)
        {
            aiShip.MoveDown();
        }else
        if (-.1f < aiShip.transform.position.y - followTransform.position.y)
        {
            aiShip.MoveUp();
        }
        else
        {
            aiShip.RotationSpeed = 0;
        }
    }
}
