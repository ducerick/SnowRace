using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayer : MonoBehaviour
{
    public float speed;
    public FloatingJoystick variableJoystick;
    public Rigidbody rb;
    public Vector3 direction;
    public static bool directOnBuild;
    private bool turnback = false;
    private float lastPosOnRoad;
    private float offset;

    private void Start()
    {
        directOnBuild = false;
    }

    private void FixedUpdate()
    {
        if(GameState.Instance.GState == State.Playing && (PlayerController.OnPlane || directOnBuild))
        {
            direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
            Debug.Log(direction);
            rb.velocity = direction * speed;
        }

        if (directOnBuild)
        {
            if (variableJoystick.Vertical < 0.0f)
            {
                EventDispatcher.Instance.PostEvent(EventID.OnCharacterUnBuild, gameObject);
                transform.localEulerAngles = new Vector3(0, 180, 0);
                turnback = true;
                lastPosOnRoad = transform.localPosition.z;
            }
            if (variableJoystick.Vertical > 0.0f && turnback)
            {
                transform.localEulerAngles = new Vector3(0, 0, 0);
                offset = transform.localPosition.z - lastPosOnRoad;
                lastPosOnRoad = transform.localPosition.z;
            }

            if (variableJoystick.Vertical > 0.0f)
            {
                if(transform.localPosition.z >= lastPosOnRoad + offset)
                {
                    EventDispatcher.Instance.PostEvent(EventID.OnCharacterBuildRoad, gameObject);
                }
            }
        } 
        else
        {
            variableJoystick.AxisOptions = AxisOptions.Both;
        }
    }

    private void OnEnable()
    {
        EventDispatcher.Instance.RegisterListener(EventID.OnCharacterBuildRoad, OnCharacterBuildRoad);
    }

    private void OnCharacterBuildRoad(object obj)
    {
        variableJoystick.AxisOptions = AxisOptions.Vertical;
        directOnBuild = true;
    }
}
