using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AıShip : ShipBase
{
    public StateAı currentState;
    public StateAı fireState = new FireState(), obstacleState = new ObstacleState(), followState = new FollowState();
    private MoveX moveX;
    private MoveY moveY;
    public List<Transform> ports = new List<Transform>();
    public float radarForwardSize = 6, radarUpSize = 15;
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
    private float rotationSpeedY;
    public float RotationSpeedY
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
        for (int i = 0; i < PortManager.instance.ports.Count; i++)
        {
            ports.Add(PortManager.instance.ports[i]);
        }
        currentState = followState;
    }
    private void Update()
    {
        if (isStunned || isForce)
        {
            return;
        }
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
    public void SetRotate()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
    public void MoveRightRotate()
    {
        transform.rotation = Quaternion.Euler(0, 0, -45);
    }

    public void MoveLeftRotate()
    {
        transform.rotation = Quaternion.Euler(0, 0, 45);
    }
    public void MoveRight()
    {

        transform.position += transform.right * multiple * Time.deltaTime;
    }
    public void MoveLeft()
    {
        transform.position -= transform.right * multiple * Time.deltaTime;
    }
    public void MoveUp()
    {
        transform.position += transform.up * multiple * Time.deltaTime;
    }
    public void MoveDown()
    {
        transform.position -= transform.up * multiple * Time.deltaTime;
    }
    public void LookAt(Transform source, Transform target)
    {
        Vector3 _direction = target.position - source.transform.position;
        Quaternion _lookRotation = Quaternion.LookRotation(_direction);
        source.transform.rotation = Quaternion.Slerp(source.transform.rotation, _lookRotation, multiple*.3f * Time.deltaTime);
    }
    public void LookAt(Transform source, Vector3 target)
    {
        Vector3 _direction = target - source.transform.position;
        Quaternion _lookRotation = Quaternion.LookRotation(_direction);
        source.transform.rotation = Quaternion.Slerp(source.transform.rotation, _lookRotation, multiple * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Port port))
        {
            ports.Remove(port.transform);
        }
        if (other.gameObject.TryGetComponent(out IObstacle obstacle))
        {
            MoveActions(transform.position, other.transform);
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
    public List<Action> moveActions = new List<Action>();
    public List<Action> MoveActions(Vector3 ship, Transform target)
    {
        Vector3 yon = new Vector3(0, 0, 0);
        Debug.Log("moveActions");
        moveActions.Clear();
        float left, right, up, down;
        left = Vector3.Distance(transform.position, target.position - target.right);
        right = Vector3.Distance(transform.position, target.position + target.right);
        down = Vector3.Distance(transform.position, target.position - target.up);
        up = Vector3.Distance(transform.position, target.position + target.up);
        if (left < right)
        {
            moveActions.Add(MoveLeft);
            yon += (target.position - target.right * 3);
        }
        else
        {
            moveActions.Add(MoveRight);
            yon += (target.position + target.right * 3);
        }
        if (down < up)
        {
            moveActions.Add(MoveDown);
        }
        else
        {
            moveActions.Add(MoveUp);
        }
        return moveActions;
    }
    IEnumerator IELooking(Vector3 vector3)
    {
        float timer = 2f;
        Vector3 vector31 = (vector3 - transform.position).normalized;
        while (timer > 0)
        {
            transform.LookAt(transform.position + vector31);
            timer -= Time.deltaTime;
            yield return null;
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
    float temp,maxSize=float.MaxValue;
    Collider[] hits = new Collider[50];
    float atackTimer = 1f;
    int hitSize;
    ShipBase sourceShip;
    public override void EnterState(AıShip aiShipSource)
    {
     //   aiShipSource.shipManager.onFireAction.Invoke(aiShipSource);
    }
    public override void UpdateState(AıShip aiShip)
    {
        if (atackTimer>0)
        {
            atackTimer -= Time.deltaTime;
        }
        else
        {
            atackTimer = 1f;
            hitSize = Physics.OverlapBoxNonAlloc(aiShip.transform.position + aiShip.transform.forward * 50, new Vector3(200, 200, 300), hits, Quaternion.identity, aiShip.skillLayer);
            if (hitSize>0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    temp = Vector3.Distance(aiShip.transform.position, hits[i].transform.position);
                    if (temp < maxSize)
                    {
                        maxSize = temp;
                        if (hits[i].TryGetComponent(out ShipBase shipBase))
                        {
                            sourceShip = shipBase;
                        }
                    }
                }
                aiShip.shipManager.onFireAction.Invoke(sourceShip);
            }
        }

    }
}

public class ObstacleState : StateAı
{
    public override void EnterState(AıShip aiShip)
    {
    }
    public override void UpdateState(AıShip aiShip)
    {
        aiShip.transform.position += aiShip.transform.forward * Time.deltaTime * aiShip.multiple * aiShip.speed * .3f;
        foreach (var action in aiShip.moveActions)
        {
            action.Invoke();
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
        aiShip.transform.position += aiShip.transform.forward * Time.deltaTime * aiShip.multiple * aiShip.speed;
        Follow(aiShip, ClosestBuff(aiShip));
    }

    private Transform ClosestBuff(AıShip aiShip)
    {
        maxSize = float.MaxValue;
        hitSize = Physics.OverlapBoxNonAlloc(aiShip.transform.position + aiShip.transform.forward * 50, new Vector3(200, 200, 300), hits, Quaternion.identity, aiShip.skillLayer);
        if (hitSize == 0)
        {
            if (aiShip.ports.Count == 0)
            {
                return null;//finish
            }
            return aiShip.ports[0];
        }
        for (int i = 0; i < hitSize; i++)
        {
            temp = Vector3.Distance(aiShip.transform.position, hits[i].transform.position);
            if (temp < maxSize)
            {
                maxSize = temp;
                buffTransform = hits[i].transform;
            }
        }
        return buffTransform;
    }
    private void Follow(AıShip aiShip, Transform followTransform)
    {
        Debug.Log(followTransform);
        if (Physics.Raycast(aiShip.transform.position, aiShip.transform.forward, out RaycastHit hit))
        {
            if (hit.transform == followTransform)
            {
                // aiShip.SetRotate();
                Debug.Log("kitlendi");
                return;
            }
        }
        // aiShip.transform.LookAt(followTransform);
        aiShip.LookAt(aiShip.transform, followTransform);
        if (.1f > aiShip.transform.position.x - followTransform?.position.x)
        {

            aiShip.MoveRight();
        }
        else
        if (-.1f < aiShip.transform.position.x - followTransform?.position.x)
        {
            aiShip.MoveLeft();
        }


    }
}
