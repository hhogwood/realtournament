using UnityEngine;
using System.Collections;

public class UsableResource : MonoBehaviour
{
    public ResourceContainer ResourceContainer;
    public string Resource;
    public int Amount;
    public UnityEngine.Events.UnityEvent OnSuccess;
    public UnityEngine.Events.UnityEvent OnFail;

    private UseBus useBus;

    void Awake()
    {
        useBus = GetComponentInParent<UseBus>();
        useBus.Subscribe(Use);
    }

    public void Use()
    {
        if (Consume() == true)
        {
            if (OnSuccess != null) OnSuccess.Invoke();
        }
        else
        {
            if (OnFail != null) OnFail.Invoke();
        }
        
    }

    void OnDestroy()
    {
        useBus.UnSubscribe(Use);
    }

    public bool Consume()
    {
        if (ResourceContainer.Consume(Resource, Amount) == Amount)
        {
            return true;
        }
        else
        {
            return false;
        }        
    }
}
