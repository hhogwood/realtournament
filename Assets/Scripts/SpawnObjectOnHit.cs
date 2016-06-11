using UnityEngine;
using System.Collections;

public class SpawnObjectOnHit : MonoBehaviour
{
    public GameObject Object;

    void Start ()
    {
        gameObject.GetComponentInParent<OnHitBus>().Subscribe(OnHit);
    }
	
	void Update ()
    {
	    
	}

    public void OnHit(HitInfo info)
    {
        Debug.Log("onhit");
        GameObject obj;
        obj = Instantiate(Object);
        obj.transform.position = info.CollisionData.contacts[0].point;
        obj.transform.LookAt(info.CollisionData.contacts[0].point + Vector3.Reflect(info.Velocity.normalized, info.CollisionData.contacts[0].normal));
    }
}
