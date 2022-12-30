using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayer : MonoBehaviour
{
    public float speed;
    public FloatingJoystick variableJoystick;
    public Rigidbody rb;
    public Vector3 direction;
    public Collision playerCollision;
    public bool turnback = false;
    private float slopeForce = 10f;

    private void FixedUpdate()
    {
        if(GameState.Instance.GState == State.Playing && (GameManager.Instance.player.OnPlane || playerCollision.GetCollisionBridge()))
        {
            direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
            rb.velocity = direction * speed;
        }

        if (playerCollision.GetCollisionBridge())
        {
            if (variableJoystick.Vertical < 0.0f)
            {
                EventDispatcher.Instance.PostEvent(EventID.OnCharacterUnBuild, gameObject);
                EventDispatcher.Instance.PostEvent(EventID.OnCharacterUnBuildStep, gameObject);
                transform.localEulerAngles = new Vector3(0, 180, 0);
                turnback = true;
            }
            if (variableJoystick.Vertical > 0.0f)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
                turnback = false;
            }
        } 
    }

    private void LateUpdate()
    {
        if (turnback)
        {
            SetForce();
            GameState.Instance.GState = State.Playing;
        }
    }

    private void SetForce()
    {
        if (playerCollision.GetCollisionBridge())
            rb.AddForce(Vector3.down * slopeForce, ForceMode.Force);
    }

    public void SetJoystick(AxisOptions option)
    {
        variableJoystick.AxisOptions = option;
    }
}
