using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Useable : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent action;

    private UseBus useBus;
    
    void Awake()
    {
        useBus = GetComponentInParent<UseBus>();
        useBus.Subscribe(Use);
    }

    public void Use()
    {
        if(action != null) action.Invoke();
    }
    
    void OnDestroy()
    {
        useBus.UnSubscribe(Use);
    }

}
