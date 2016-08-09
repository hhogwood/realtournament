using UnityEngine;

public class Movement : MonoBehaviour, IControllable 
{
    public ControlBus controlBus;
    public Rigidbody Body;
    public ForceMode Mode;
    public Vector3 moveForce;
    public float speed;

    private Vector3 normalizedMovement;
    public void Start()
    {
        controlBus = GetComponent<ControlBus>();
        controlBus.Subscribe(InputUpdate);
    }
    public void Move(Vector3 _moveVect)
    {
        normalizedMovement = _moveVect.normalized;
        moveForce = _moveVect.normalized * speed;
    }
    
    void FixedUpdate()
    {
        Body.AddForce(moveForce, Mode);
        moveForce = Vector3.zero;
    }

    public void InputUpdate(ControlState state)
    {
        Move(state.movement);
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }
}