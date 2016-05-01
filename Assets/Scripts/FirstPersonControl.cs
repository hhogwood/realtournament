using UnityEngine;
using System.Collections;
using System;

public class FirstPersonControl : MonoBehaviour
{
    public Camera pointOfView;
    public Transform cameraPivot;
    public Rigidbody body;

    public float rotationSpeed;
    public float movementSpeed;

    public float minAngle = -45;
    public float maxAngle = 45;
    
    private InputBus InputBus;

    private Vector3 rotation;
    private Vector3 movement;

    private Vector3 deltaRotation;
    private Vector3 movementForce;
    private ForceMode forceMode = ForceMode.Acceleration;

    public Vector3 eulerAngles;

    // Use this for initialization
    
    void Awake()
    {
        InputBus = GetComponent<InputBus>();
        InputBus.Subscribe(InputUpdate);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        deltaRotation = Vector3.zero;
        deltaRotation -= transform.right * rotation.y;
        deltaRotation += Vector3.up * rotation.x;
        deltaRotation *= rotationSpeed * Time.deltaTime;

        eulerAngles += deltaRotation;

        eulerAngles.y = eulerAngles.y % 360;
        eulerAngles.x = eulerAngles.x % 360;
        eulerAngles.x = Mathf.Clamp(eulerAngles.x, minAngle, maxAngle);
        eulerAngles.z = 0;

        cameraPivot.transform.rotation = Quaternion.Euler(eulerAngles);
    }

    void FixedUpdate()
    {
        movementForce = Vector3.zero;
        movementForce += new Vector3(cameraPivot.forward.x, 0, cameraPivot.forward.z) * movement.y;
        movementForce += cameraPivot.right * movement.x;
        movementForce *= movementSpeed * Time.fixedDeltaTime;
        body.AddForce(movementForce, forceMode);
    }
    
    void InputUpdate(InputState state)
    {
        rotation = state.rotation;
        movement = state.movement;
    }
    
    void OnDestroy()
    {
        InputBus.UnSubscribe(InputUpdate);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}
