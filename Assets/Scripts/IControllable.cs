using UnityEngine;
using System.Collections;

public interface IControllable
{
    void InputUpdate(InputState state);
    GameObject GetGameObject();
}

[System.Serializable]
public class InputState
{
    public Vector3 movement;
    public Vector3 rotation;
}
