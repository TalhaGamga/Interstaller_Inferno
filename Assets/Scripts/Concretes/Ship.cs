using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : ShipBase
{

    float brakeInput;
    float fireEngineInput;
    float yawInput;
    float rollInput;
    //[SerializeField] float speed = 80f;
    //[SerializeField] float boostedSpeed = 120f;
    [SerializeField] float brakeMultier = 50;
    [SerializeField] float rotationSpeed = 50f;

    [SerializeField] float yawRotationSpeed = 80;
    [SerializeField] float yawAcceleration = .15f;

    [SerializeField] GameObject model;

    Vector2 lookInput, screenCenter, mouseDistance;

    float xRot;
    float yRot;
    float zRot;

    float xYawRot;
    float yYawRot;
    float zYawRot;

    bool isRolling = false;
    bool canRoll = true;
    void Start()
    {
        screenCenter.x = Screen.width / 2;
        screenCenter.y = Screen.height / 2;

        Cursor.lockState = CursorLockMode.Confined;
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

        //speed -= transform.forward.y * Time.deltaTime * 50;


        if (speed < 35f)
        {
            speed = 35f;
        }

        if (Input.GetKey(KeyCode.S))
        {

            speed -= 50 * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.S))
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
            Debug.Log(coolDown);
            model.transform.Rotate(0, 0, inp * Time.deltaTime * 360);
            coolDown += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        isRolling = false;
        yield return new WaitForSeconds(2f);
        canRoll = true;
    }
}
