using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AıShip : ShipBase
{
    public StateAı currentState;
    public StateAı fireState = new FireState(), obstacleState = new ObstacleState(), followState = new FollowState();
    private Move move;

    public float radarForwardSize = 6, radarUpSize =15;
    public float multiple = 10.0f;
    Vector3 planePos;
    private float rotationSpeed;
    public float RotationSpeed
    {
        get => rotationSpeed; set
        {
            if (value > 1)
            {
                rotationSpeed = 1;
            }
            if (value <= 0)
            {
                rotationSpeed = 0;
            }
        }
    }

    public LayerMask obstacleLayer, skillLayer;
    public float skillRadarSize = 50f;
    private void Start()
    {
        currentState = followState;
    }
    private void Update()
    {
        currentState.UpdateState(this);
        GoForward();
    }
    private void GoForward()
    {

    }
    private void SwitchState(StateAı switchAı)
    {
        currentState = switchAı;
        currentState.EnterState(this);
    }
    public void MoveRight()
    {
        Debug.Log("right");
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
        transform.rotation = Quaternion.Euler(0, 0, -45 * rotationSpeed);
    }
    public void MoveLeft()
    {
        Debug.Log("left");
        if (Move.right == move)
        {
            RotationSpeed -= Time.deltaTime * 3f;
            if (RotationSpeed == 0)
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
        transform.rotation = Quaternion.Euler(0, 0, 45 * RotationSpeed);
    }


    public void MoveUp()
    {
        Debug.Log("up");
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
        transform.rotation = Quaternion.Euler(-45 * RotationSpeed, 0, 0);
    }

    public void MoveDown()
    {
        Debug.Log("down");
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
        transform.rotation = Quaternion.Euler(45 * RotationSpeed, 0, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
     
        if (other.gameObject.TryGetComponent(out IObstacle obstacle))
        {
           
            SwitchState(obstacleState);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IObstacle obstacle))
        {
         
            SwitchState(followState);
            
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(1, radarUpSize, radarForwardSize));
       // Physics.OverlapBoxNonAlloc(aiShip.transform.position, new Vector3(1, aiShip.radarUpSize, aiShip.radarForwardSize), hits, Quaternion.identity);
    }

    public override void Roll()
    {
        throw new System.NotImplementedException();
    }
}

public abstract class StateAı
{
    public abstract void EnterState(AıShip aiShip);
    public abstract void UpdateState(AıShip aiShip);
}

public class FireState : StateAı//tartışmalı
{
    public override void EnterState(AıShip aiShipSource)
    {
        aiShipSource.shipManager.onFireAction.Invoke(aiShipSource);
    }
    public override void UpdateState(AıShip aiShip)
    {

    }
}

public class ObstacleState : StateAı
{
    Transform buffTransform;
    Collider[] hits = new Collider[50];
    int hitSize;
    float maxSize = float.MaxValue, temp;
    bool isLeft, isRight, isUp, isDown;
    public override void EnterState(AıShip aiShip)
    {
        aiShip.RotationSpeed = 0;
    }
    public override void UpdateState(AıShip aiShip)
    {
        ClosestBuff(aiShip);
    }
    private void ClosestBuff(AıShip aiShip)
    {
       
        isLeft = Physics.Raycast(aiShip.transform.position - aiShip.transform.right, aiShip.transform.forward, 50f, aiShip.obstacleLayer);//obstacleLayer
        isRight = Physics.Raycast(aiShip.transform.position + aiShip.transform.right, aiShip.transform.forward, 50f, aiShip.obstacleLayer);
        isUp = Physics.Raycast(aiShip.transform.position + aiShip.transform.up, aiShip.transform.forward, 50f, aiShip.obstacleLayer);
        isDown = Physics.Raycast(aiShip.transform.position - aiShip.transform.up, aiShip.transform.forward, 50f, aiShip.obstacleLayer);
        if (!isLeft && !isRight && !isUp && !isDown)
        {
          
            aiShip.RotationSpeed = 0;// state değiştirmeyi colliderden yap
            return;
        }
        Follow(aiShip);
    }
    private void Follow(AıShip aiShip)
    {
        Debug.Log("obs"+ isLeft +isRight);
        if (isLeft)
        {
            aiShip.MoveRight();
        }else
        if (isRight)
        {
            aiShip.MoveLeft();
        }
        if (isDown)
        {
            aiShip.MoveUp();
        }else
        if (isRight)
        {
            aiShip.MoveDown();
        }
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
        aiShip.RotationSpeed = 0;
    }
    public override void UpdateState(AıShip aiShip)
    {
        Debug.Log("follow update");
        Follow(aiShip, ClosestBuff(aiShip));
    }
    
    private Transform ClosestBuff(AıShip aiShip)
    {
        maxSize = float.MaxValue;
        hitSize = Physics.OverlapBoxNonAlloc(aiShip.transform.position, new Vector3(1, aiShip.radarUpSize, aiShip.radarForwardSize), hits, Quaternion.identity, aiShip.skillLayer);
        if (hitSize == 0)
        {
            return null;//en öndeki objeye doğru git
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
       
        if (.1f > aiShip.transform.position.x - followTransform?.position.x)
        {
            Debug.Log("left");
            aiShip.MoveLeft();
        }
        else
        if (-.1f < aiShip.transform.position.x - followTransform?.position.x)
        {
            aiShip.MoveRight();
        }
        else
        {
            aiShip.RotationSpeed = 0;
        }
        if (.1f > aiShip.transform.position.y - followTransform?.position.y)
        {
            aiShip.MoveDown();
        }
        else
        if (-.1f < aiShip.transform.position.y - followTransform?.position.y)
        {
            aiShip.MoveUp();
        }
        else
        {
            aiShip.RotationSpeed = 0;
        }
    }
}
