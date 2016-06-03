using UnityEngine;
using System.Collections;

public interface IControllable
{
    void InputUpdate(ControlState state);
    GameObject GetGameObject();
}


