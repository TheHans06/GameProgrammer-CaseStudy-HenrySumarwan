using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;}

    public enum GameState { Exploration, Puzzle }
    public GameState currentState;
    public event Action<GameState> OnStateChanged;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start() => ChangeState(GameState.Exploration);

    public void ChangeState(GameState newState) {
        currentState = newState;

        bool exploring = newState == GameState.Exploration;
        Cursor.lockState = exploring ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !exploring;

        OnStateChanged?.Invoke(newState);
        Debug.Log("State Sekarang : " + currentState);
    }

}
