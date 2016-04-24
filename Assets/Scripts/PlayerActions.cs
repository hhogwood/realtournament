using UnityEngine;
using InControl;
public class CharacterActions : PlayerActionSet
{
    public PlayerAction Left;
    public PlayerAction Right;
    public PlayerAction Up;
    public PlayerAction Down;
    public PlayerAction Jump;
    public PlayerAction Fire;
    public PlayerAction AltFire;
    public PlayerAction Interact;
    public PlayerTwoAxisAction Move;

    public CharacterActions()
    {
        Left = CreatePlayerAction( "Move Left" );
        Right = CreatePlayerAction( "Move Right" );
        Up = CreatePlayerAction( "Move Left" );
        Down = CreatePlayerAction( "Move Right" );
        Jump = CreatePlayerAction( "Jump" );
        Fire = CreatePlayerAction( "Fire" );
        AltFire = CreatePlayerAction( "AltFire" );
        Interact = CreatePlayerAction( "Interact" );
        Move = CreateTwoAxisPlayerAction(Left, Right, Down, Up);
    }
}