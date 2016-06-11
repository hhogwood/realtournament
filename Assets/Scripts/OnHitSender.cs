using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OnHitSender : MonoBehaviour
{ 
    public UnityEngine.Events.UnityEvent OnHitActions;
    public HitInfo Info;
    public List<string> TagsToIgnore = new List<string>();
    private Rigidbody body;

    // Use this for initialization
    void Start()
    {
        body = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision other)
    {
        bool ignore = false;

        for (int i = 0; i < TagsToIgnore.Count; i++)
        {
            if (other.gameObject.tag == TagsToIgnore[i])
            {
                ignore = true;
            }
        }

        if (!ignore)
        {
            OnHitBus otherBus = other.gameObject.GetComponent<OnHitBus>();

            if (OnHitActions != null)
            {
                OnHitActions.Invoke();
            }

            if (otherBus != null)
            {
                Info.Velocity = body.velocity;
                Info.CollisionData = other;
                otherBus.Hit(Info);
            }
        }
    }
}
