using UnityEngine;
using System.Collections;

public class GameObjectSpawner : MonoBehaviour
{
    public Transform SpawnPoint;
    public GameObject ObjectToSpawn;

	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
	    
	}

    public void Spawn()
    {
        Instantiate(ObjectToSpawn).gameObject.transform.position = SpawnPoint.position;
    }
}
