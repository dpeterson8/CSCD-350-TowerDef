using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager: Singleton<GameManager> {

    public static event Action<GameState> OnBeforeStateChanged;
    // public static event Action<GameState> OnAfterStateChanged;

    public GameState State { get; private set; }

    void Start() {
        ChangeState(GameState.StartLevel);
    }

    public void ChangeState(GameState newState) {
        OnBeforeStateChanged?.Invoke(newState);

        State = newState;
        switch (newState) {
            case GameState.StartLevel:
                StartLevel();
                break;
            case GameState.Placing:
                Placing();
                break;
            case GameState.Attack:
                Attack();
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }
    }

    public void StartLevel() {
        ChangeState(GameState.Placing);
    }

    public void Placing() {
        Time.timeScale = 0;
        ChangeState(GameState.Attack);
    }

    public void Attack() {
        Time.timeScale = 1;
        ChangeState(GameState.Win);
    }

}

public enum GameState {
    StartLevel = 0,
    Placing = 1,
    Attack = 2,
    Win = 3,
    Lose = 4
}