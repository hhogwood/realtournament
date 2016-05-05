using UnityEngine;

public class OnHitBus : MonoBehaviour
{
    public OnHit onHit;
    // OnUse this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Subscribe(OnHit _onHit)
    {
        onHit += _onHit;
    }

    public void UnSubscribe(OnHit _onHit)
    {
        onHit -= _onHit;
    }

    public void Hit(HitInfo _hitInfo)
    {
        if (onHit != null)
        {
            onHit.Invoke(_hitInfo);
        }
    }
}

public delegate void OnHit(HitInfo _hitInfo);
