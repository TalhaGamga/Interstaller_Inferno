using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class ShipBase : MonoBehaviour
{

    public ShipManager shipManager;
    public float speed;
    public float hp;
    public Vector3 scale;
    public float normalSpeed;
    public float speedMultier = 1;
    public bool isStunned = false, isSlowing=false,isForce=false;

    RocketLauncher rocketLauncher;
    private void Awake()
    {
        rocketLauncher = GetComponent<RocketLauncher>();
        normalSpeed = speed;
    }
    private void Update()
    {
        Movement();
    }

    public virtual void Skill(CollectableSkillBase skillBase)
    {
        skillBase.Use(this);
    }
    #region Movement
 

    public virtual void Movement()
    {
        if (isStunned)
        {
            return;
        }
    }
    #endregion

    public abstract void Roll();

    public virtual void Stun()//
    {
        StartCoroutine(IEStun());
    }
    public virtual void FireTimer()
    {

    }
    IEnumerator IEStun()
    {
        isStunned = true;
        speed = 0;
        yield return new WaitForSeconds(3f);
        speed = normalSpeed;
        isStunned = false;
    }
    public virtual void Slowing()
    {
        StartCoroutine(IESlowing());
    }
    private IEnumerator IESlowing()
    {
        isSlowing = true;
        speedMultier *= .5f;
        yield return new WaitForSeconds(1f);
        speedMultier *= 2;
        isSlowing = false;
    }
    public virtual void Force(Vector3 contactPoint)
    {
        isForce = true;
        StartCoroutine(IEForce(contactPoint));
    }
    private IEnumerator IEForce(Vector3 contactPoint)
    {
        Vector3 poz = (transform.position - contactPoint).normalized;
        float timer = 1f,force=2f;
        while (timer>0)
        {
            transform.position += force * Time.deltaTime * poz;
            force -= Time.deltaTime * 3;
            timer -= Time.deltaTime;
            yield return null;
        }
        
    }
}