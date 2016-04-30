using UnityEngine;

public class MovementComponent : MonoBehaviour
{
    public Rigidbody Body;
    public ForceMode Mode;
    private Vector3 moveForce;
    private float speed;
    public void Move(Vector3 _moveVect)
    {
        moveForce = _moveVect.normalized * speed;
    }
    
    void FixedUpdate()
    {
        Body.AddForce(moveForce, Mode);
        moveForce = Vector3.zero;
    }
}