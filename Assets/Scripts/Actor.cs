using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider))]

public class Actor : MonoBehaviour
{
    [System.NonSerialized]
    public Rigidbody body;
    [System.NonSerialized]
    public new MeshRenderer renderer;
    [System.NonSerialized]
    public new Collider collider;

	// Use this for initialization
	void Awake ()
    {
        body = GetComponent<Rigidbody>();
        renderer = GetComponent<MeshRenderer>();
        collider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
