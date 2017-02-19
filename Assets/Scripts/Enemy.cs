using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour 
{
    public Actor actor;

    protected void Init()
    {
        actor = GetComponent<Actor>();
    }
}
