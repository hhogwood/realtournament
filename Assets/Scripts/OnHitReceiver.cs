using UnityEngine;
using System.Collections;

public class OnHitReceiver : MonoBehaviour
{    
    public UnityEngine.Events.UnityEvent OnHitActions;

    void Awake()
    {
        gameObject.GetComponentInParent<OnHitBus>().Subscribe(OnHit);
    }

    public void OnHit(HitInfo info)
    {
        if (OnHitActions != null)
        {
            OnHitActions.Invoke();
        }        
    }
}
