using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class ShipBase : MonoBehaviour
{
    public float speed;
    public float hp;
    public Vector3 scale;
    public float currentSpeed;
    public ShipBase SourceShip;
    public float speedMultier = 1;
    public bool isStun, isSlowing;
    public Queue<RocketBase> rocketBases = new Queue<RocketBase>();

    private void Update()
    {
        moveAction?.Invoke();
    }
    public virtual void TakeDamage(float Damage)
    {

    }
    public virtual void Slow(float Timer)
    {
        StartCoroutine(IESlowing(Timer));
    }
    private IEnumerator IESlowing(float timer)
    {
        speedMultier *= .5f;
        yield return new WaitForSeconds(timer);
        speedMultier *= 2f;
    }
    public virtual void Stun()
    {

    }
    public virtual void Skill(SkillBase skillBase)
    {
        skillBase.Use(this);
    }
    public Action moveAction => Movement;
    public virtual void Movement()
    {
        if (isStun)
        {
            return;
        }

    }

}