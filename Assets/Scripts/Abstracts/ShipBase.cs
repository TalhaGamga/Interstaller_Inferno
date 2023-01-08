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
    public float speedMultier = 1;
    public bool isStunned = false, isSlowing;

    RocketLauncher rocketLauncher;
    private void Awake()
    {
        rocketLauncher = GetComponent<RocketLauncher>();
        normalSpeed = speed;
    }
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
        if (isStunned)
        {
            return;
        }
    }
    #endregion

    public abstract void Roll();

}