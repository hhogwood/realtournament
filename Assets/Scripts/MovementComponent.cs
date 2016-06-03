using UnityEngine;

public class MovementComponent : MonoBehaviour, IControllable 
{
    public Rigidbody Body;
    public ForceMode Mode;
    public Vector3 moveForce;
    public float speed;

    private Vector3 normalizedMovement;
    public void Move(Vector3 _moveVect)
    {
        normalizedMovement = _moveVect.normalized;
        //moveForce = _moveVect.normalized * speed;
        moveForce = Vector3.zero;
        moveForce += Camera.main.transform.right * normalizedMovement.x * speed;
        moveForce += Camera.main.transform.forward * normalizedMovement.y * speed;
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