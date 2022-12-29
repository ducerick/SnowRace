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
        GameStart();
    }

    private void GameStart()
    {
        SetRotation();
    }

    private void SetRotation()
    {
        if (joystickPlayer.direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(joystickPlayer.direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, joystickPlayer.speed);
        }
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
