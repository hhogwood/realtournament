using UnityEngine;
using System.Collections;

[System.Serializable]
public class HitInfo
{
    public int Damage;

    [System.NonSerialized]
    public Collision CollisionData;

    [System.NonSerialized]
    public Vector3 Velocity;
}
