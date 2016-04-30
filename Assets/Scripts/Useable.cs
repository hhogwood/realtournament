using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Useable : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent action;

    public void Use()
    {
        Debug.Log("Recieved use message");
        if(action != null)
            action.Invoke();
    }

}
