using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class ShipBase : MonoBehaviour
{
    public float speed;
    public float hp;
    public Vector3 scale;
    public float normalSpeed;
    public ShipBase SourceShip;
    public float speedMultier = 1;
    public bool isStun, isSlowing;

    public Queue<RocketBase> rocketBases = new Queue<RocketBase>();

    private void Update()
    {
        moveAction?.Invoke();
    }
   
    public virtual void Skill(CollectableSkillBase skillBase)
    {
        skillBase.Use(this);
    }

    #region Movement
    public Action moveAction => Movement;

    public virtual void Movement()
    {
        if (isStun)
        {
            return;
        }

    }
    #endregion

}