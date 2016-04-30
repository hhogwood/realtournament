using UnityEngine;
using InControl;
public class CharacterActions : PlayerActionSet
{
    public PlayerAction Left;
    public PlayerAction Right;
    public PlayerAction Up;
    public PlayerAction Down;

    public PlayerAction LookLeft;
    public PlayerAction LookRight;
    public PlayerAction LookUp;
    public PlayerAction LookDown;

    public PlayerAction Jump;
    public PlayerAction Fire;
    public PlayerAction AltFire;
    public PlayerAction Interact;
    public PlayerTwoAxisAction Move;
    public PlayerTwoAxisAction Look;

    public CharacterActions()
    {
        Left = CreatePlayerAction( "Move Left" );
        Right = CreatePlayerAction( "Move Right" );
        Up = CreatePlayerAction( "Move Up" );
        Down = CreatePlayerAction( "Move Down" );

        LookLeft = CreatePlayerAction("Look Left");
        LookRight = CreatePlayerAction("Look Right");
        LookUp = CreatePlayerAction("Look Up");
        LookDown = CreatePlayerAction("Look Down");

        Jump = CreatePlayerAction( "Jump" );
        Fire = CreatePlayerAction( "Fire" );
        AltFire = CreatePlayerAction( "AltFire" );
        Interact = CreatePlayerAction( "Interact" );

        Move = CreateTwoAxisPlayerAction(Left, Right, Down, Up);
        Look = CreateTwoAxisPlayerAction(LookLeft, LookRight, LookDown, LookUp);
    }
}