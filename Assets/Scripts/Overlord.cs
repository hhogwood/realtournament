using UnityEngine;

public class GameMom : MonoBehaviour
{
    public static GameMom instance;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        instance = new GameMom();
    }
}