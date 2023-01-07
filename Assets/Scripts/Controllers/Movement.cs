using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    float brakeInput;
    float fireEngineInput;
    float yawInput;
    public float yawRotationSpeed = 30;

    float speed = 80f;
    float boostedSpeed = 120f;
    [SerializeField] float brakeMultier = 50;
    
    float rotationSpeed = 50f;

    Vector2 lookInput, screenCenter, mouseDistance;
    public float yawAcceleration;

    [SerializeField] GameObject model;

    float xRot;
    float yRot;
    float zRot;

    void Start()
    {
        screenCenter.x = Screen.width / 2;
        screenCenter.y = Screen.height / 2;

        Cursor.lockState = CursorLockMode.Confined;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        fireEngineInput = Input.GetAxis("FireEngine");
        brakeInput = Input.GetAxis("Brake");

        yawInput = Input.GetAxis("HorizontalYaw");

        /*Movement*/
        transform.Translate((transform.forward * (speed + fireEngineInput * boostedSpeed)) * Time.deltaTime, Space.World);
        speed -= transform.forward.y * Time.deltaTime * 50;

        speed -= brakeInput*brakeMultier * Time.deltaTime;

        Debug.Log(brakeInput);

        if (speed < 35f)
        {
            speed = 35f;
        }

        lookInput.x = Input.mousePosition.x;
        lookInput.y = Input.mousePosition.y;

        mouseDistance.x = (lookInput.x - screenCenter.x) / screenCenter.y;
        mouseDistance.y = (lookInput.y - screenCenter.y) / screenCenter.y;

        mouseDistance = Vector2.ClampMagnitude(mouseDistance, 1f);

        transform.Rotate(-mouseDistance.y * rotationSpeed * Time.deltaTime, mouseDistance.x * rotationSpeed * Time.deltaTime, 0, Space.Self);

        /******/
        /*YawInput*/
        xRot = -mouseDistance.y * rotationSpeed / 2;
        zRot = -mouseDistance.x * rotationSpeed * 1.2f;
        yRot = model.transform.localRotation.y;

        Quaternion rot = Quaternion.Euler(xRot, yRot, zRot);

        if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            model.transform.localRotation = Quaternion.Slerp(model.transform.localRotation, rot, yawAcceleration / 15);
        }

        yawInput = Input.GetAxis("HorizontalYaw");

        Quaternion yawRot = Quaternion.Euler(model.transform.localRotation.x, model.transform.localRotation.y, -yawInput * yawRotationSpeed);
        model.transform.localRotation = Quaternion.Slerp(model.transform.localRotation, yawRot, yawAcceleration / 10);
    }
}
 