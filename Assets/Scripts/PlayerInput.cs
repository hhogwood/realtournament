using System;
using UnityEngine;
using InControl;
using System.Collections.Generic;

public class PlayerInput : MonoBehaviour
{
    public CharacterActions PlayerAction = new CharacterActions();
    public ControlState state;

    public IControllable controlTarget;
    
    public ControlBus InputBus;
    public UseBus CurrentGun;

    public void SetTarget(IControllable target)
    {
        controlTarget = target;
    }

    void Start()
    {
        #region InControl setup
        PlayerAction = new CharacterActions();

        PlayerAction.Left.AddDefaultBinding(Key.A);
        PlayerAction.Right.AddDefaultBinding(Key.D);
        PlayerAction.Up.AddDefaultBinding(Key.W);
        PlayerAction.Down.AddDefaultBinding(Key.S);

        PlayerAction.LookUp.AddDefaultBinding(Mouse.PositiveY);
        PlayerAction.LookDown.AddDefaultBinding(Mouse.NegativeY);
        PlayerAction.LookRight.AddDefaultBinding(Mouse.PositiveX);
        PlayerAction.LookLeft.AddDefaultBinding(Mouse.NegativeX);

        PlayerAction.Fire.AddDefaultBinding(Mouse.LeftButton);
        PlayerAction.AltFire.AddDefaultBinding(Mouse.RightButton);
        PlayerAction.Jump.AddDefaultBinding(Key.Space);
        PlayerAction.Interact.AddDefaultBinding(Key.E);
        #endregion
    }

    void Update()
    {
        state.movement = PlayerAction.Move.Vector;
        state.rotation = PlayerAction.Look.Vector;

        if(PlayerAction.Fire.WasPressed)
        {
            if(CurrentGun != null)
            {
                CurrentGun.Use();
            }
        }
        
        if(InputBus != null)
        {
            InputBus.UpdateControl(state);
        }
    }
}

