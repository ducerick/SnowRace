using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayer : MonoBehaviour
{
    public float speed;
    public FloatingJoystick variableJoystick;
    public Rigidbody rb;
    public Vector3 direction;
    private void FixedUpdate()
    {
        if(GameState.Instance.GState == State.Playing && PlayerController.OnPlane)
        {
            direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
            rb.velocity = direction * speed;
        }
    }
}
