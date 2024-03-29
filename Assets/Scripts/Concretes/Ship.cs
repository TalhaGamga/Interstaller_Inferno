using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Ship : ShipBase
{
    float brakeInput;
    float fireEngineInput;
    float yawInput;
    float rollInput;

    [SerializeField] float brakeMultier = 50;
    [SerializeField] float rotationSpeed = 50f;

    [SerializeField] float yawRotationSpeed = 80;
    [SerializeField] float yawAcceleration = .15f;

    [SerializeField] GameObject model;

    Vector2 lookInput, screenCenter, mouseDistance;
    [SerializeField] LayerMask layer;
    float xRot;
    float yRot;
    float zRot;

    float xYawRot;
    float yYawRot;
    float zYawRot;

    bool isRolling = false;
    bool canRoll = true;

    public Collider[] hits = new Collider[20];

    [SerializeField] private Camera main;

    [SerializeField] RocketLauncher shipRocketLauncher;

    [SerializeField] RocketBase rocket;

    [SerializeField] LayerMask aiLayer;
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 100);
    }
    void Start()
    {
        EventManager.player = this;
        screenCenter.x = Screen.width / 2;
        screenCenter.y = Screen.height / 2;

        Cursor.lockState = CursorLockMode.Confined;
    }

    protected override void Update()
    {
        Movement();

        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }
    /*INPUTS TODO   */

    ShipBase source;
    public void Fire()
    {
        ShipBase shipBase = this;
        float temp;
        float maxSize = float.MaxValue;
        int hitSize = Physics.OverlapSphereNonAlloc(transform.position, 500f, hits, aiLayer);
        if (hitSize == 0)
        {
            return;//en �ndeki objeye do�ru git
        }
        for (int i = 0; i < hitSize; i++)
        {
            temp = Vector3.Distance(transform.position, hits[i].transform.position);
            if (temp<maxSize)
            {
                maxSize = temp;
                if (hits[i].TryGetComponent(out ShipBase ship))
                {
                    Debug.Log(ship.gameObject + " ------------------------");
                    source = ship;
                }
            }
        }

        Debug.Log("Fire bitti " + source);

        shipManager.OnRocketLaunching.Invoke(source);
    }


    public override void Movement()
    {
        #region Inputs
        fireEngineInput = Input.GetAxis("FireEngine");
        brakeInput = Input.GetAxis("Brake");
        yawInput = Input.GetAxis("HorizontalYaw");
        rollInput = Input.GetAxisRaw("Roll");
        #endregion

        #region MouseDistance 
        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;
        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;
        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);
        #endregion

        #region Translation
        transform.Translate((transform.forward * (speed + fireEngineInput * (1.5f * speed))) * Time.deltaTime, Space.World);

        if (Input.GetKey(KeyCode.S) && !isStunned)
        {
            if (speed < 35f)
            {
                speed = 35f;
            }
            else
            {
                speed -= 50 * Time.deltaTime;
            }
        }


        if (Input.GetKeyUp(KeyCode.S) && !isStunned)
        {
            speed = 80f;
        }

        xRot = -mouseDistance.y * rotationSpeed * Time.deltaTime;
        yRot = mouseDistance.x * rotationSpeed * Time.deltaTime;

        transform.Rotate(xRot, yRot, 0, Space.Self);
        #endregion

        Roll();

        if (!isRolling)
        {
            /******/
            #region Yaw Mechanic
            xYawRot = -mouseDistance.y * rotationSpeed / 2;
            zYawRot = -mouseDistance.x * rotationSpeed * 1.2f;
            yYawRot = model.transform.localRotation.y;

            Quaternion rot = Quaternion.Euler(xYawRot, yYawRot, zYawRot);

            if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
            {
                model.transform.localRotation = Quaternion.Slerp(model.transform.localRotation, rot, yawAcceleration / 15);
            }

            Quaternion yawRot = Quaternion.Euler(model.transform.localRotation.x, model.transform.localRotation.y, -yawInput * yawRotationSpeed);
            model.transform.localRotation = Quaternion.Slerp(model.transform.localRotation, yawRot, yawAcceleration / 10);
            #endregion
        }
    }

    public override void Roll()
    {
        if (canRoll)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Q))
            {
                canRoll = false;
                isRolling = true;

                StartCoroutine(IERoll(rollInput));
            }
        }
    }

    IEnumerator IERoll(float inp)
    {
        float coolDown = 0;
        while (coolDown < 1)
        {
            model.transform.Rotate(0, 0, inp * Time.deltaTime * 360);
            coolDown += Time.deltaTime;
            yield return null;
        }

        isRolling = false;
        yield return new WaitForSeconds(2f);
        canRoll = true;
    }

    public override void Stun()
    {
        base.Stun();
        Shake(1, 100, 10, 1);
    }
    public void Shake(float duration, float strength, int vibrato, float randomness)
    {
        main.transform.DOShakePosition(duration, strength, vibrato, randomness);
    }
}
