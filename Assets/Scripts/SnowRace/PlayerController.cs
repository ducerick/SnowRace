using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody myRigidBody;
    public bool OnPlane { get; set; }
    [SerializeField] private JoystickPlayer joystickPlayer;

    private void Start()
    {
        OnPlane = true;
        GameState.Instance.GState = State.Idle;
        myRigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckGameStart();
    }

    private void CheckGameStart()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameState.Instance.GState = State.Playing;
            AnimatorPlayer.Instance.RunInPlace();
        }
        if (Input.GetMouseButtonUp(0))
        {
            myRigidBody.velocity = Vector3.zero;
            GameState.Instance.GState = State.Stop;
            AnimatorPlayer.Instance.Idle();
        }
    }
}
