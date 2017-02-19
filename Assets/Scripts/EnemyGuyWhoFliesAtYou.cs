using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGuyWhoFliesAtYou : Enemy
{
    private ControlBus controlBus;

    public Transform Target;

    private Vector3 rotation;
    private Vector3 movement;

    void Start()
    {
        Init();
        controlBus = GetComponent<ControlBus>();
        controlBus.Subscribe(InputUpdate);
    }

    void Update()
    {
        
    }

    void InputUpdate(ControlState state)
    {
        rotation = state.rotation;
        movement = state.movement;
    }
}
