using UnityEngine;
using System.Collections;

public enum TransformDirection {up, down, left, right, forward, back, random}

public class AddForceOnStart : MonoBehaviour
{
    public float Strength;
    public TransformDirection Direction;

	// Use this for initialization
	void Start ()
    {
        switch (Direction)
        {
            case TransformDirection.up:
                GetComponent<Rigidbody>().AddForce(transform.up * Strength);
                break;
            case TransformDirection.down:
                GetComponent<Rigidbody>().AddForce(-transform.up * Strength);
                break;
            case TransformDirection.left:
                GetComponent<Rigidbody>().AddForce(-transform.right * Strength);
                break;
            case TransformDirection.right:
                GetComponent<Rigidbody>().AddForce(transform.right * Strength);
                break;
            case TransformDirection.forward:
                GetComponent<Rigidbody>().AddForce(transform.forward * Strength);
                break;
            case TransformDirection.back:
                GetComponent<Rigidbody>().AddForce(-transform.forward * Strength);
                break;
            case TransformDirection.random:
                GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)) * Strength);
                break;
            default:
                break;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
