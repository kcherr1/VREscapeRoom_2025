using System;
using System.Collections.Generic;
using UnityEngine;
using State = DoorState;

public enum DoorState {
    CLOSED,
    OPENING,
    OPEN,
}

public class Door : MonoBehaviour {
    private Dictionary<State, Action> stateEnterMethods;
    private Dictionary<State, Action> stateStayMethods;
    private Dictionary<State, Action> stateExitMethods;
    public State CurState { get; private set; }
    private Animator animator;

    void Start() {
        stateEnterMethods = new() {
            [State.CLOSED] = StateEnterClosed,
            [State.OPENING] = StateEnterOpening,
            [State.OPEN] = StateEnterOpen,
        };
        stateStayMethods = new() {
            [State.CLOSED] = StateStayClosed,
            [State.OPENING] = StateStayOpening,
            [State.OPEN] = StateStayOpen,
        };
        stateExitMethods = new() {
            [State.CLOSED] = StateExitClosed,
            [State.OPENING] = StateExitOpening,
            [State.OPEN] = StateExitOpen,
        };
        CurState = State.CLOSED;
        animator = GetComponent<Animator>();
    }

    public void ChangeState(State NewState) {
        if (CurState != NewState) {
            if (stateExitMethods.ContainsKey(CurState)) {
                stateExitMethods[CurState]();
            }
            CurState = NewState;
            if (stateEnterMethods.ContainsKey(CurState)) {
                stateEnterMethods[CurState]();
            }
        }
    }

    void Update() {
        if (stateStayMethods.ContainsKey(CurState)) {
            stateStayMethods[CurState]();
        }
    }

    private void StateEnterClosed() {

    }
    private void StateEnterOpening() {
    }
    private void StateEnterOpen() {

    }

    private void StateStayClosed() {

    }
    private void StateStayOpening() {
        animator.Play("Open");
        SoundManager.Instance.Play(SoundType.SOLVED);
        ChangeState(DoorState.OPEN);
    }
    private void StateStayOpen() {

    }

    private void StateExitClosed() {

    }
    private void StateExitOpening() {
    }
    private void StateExitOpen() {

    }
}
