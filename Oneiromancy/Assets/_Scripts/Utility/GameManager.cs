using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameManager : MonoBehaviour
{
    private void Awake() {
        ReferenceManager.GameManager = this;
    }

    public Action<GameState, GameState> OnStateChange = delegate { };
    public GameState CurrentState { get; private set; }
    public enum GameState {
        MainMenu,
        InControl,
        NoControl,
        GameOver
    }

    public void SetState(GameState state) {
        Debug.Log("Gamestate changed from " + CurrentState + " to " + state);
        GameState oldState = CurrentState;
        CurrentState = state;
        OnStateChange(oldState, CurrentState);
    }
}
