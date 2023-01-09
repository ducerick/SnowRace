using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayer : MonoBehaviour
{
    [SerializeField] private FloatingJoystick variableJoystick;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collision playerCollision;
    [SerializeField] private float slopeForce = 10f;
    [SerializeField] private LayerMask layer;
    [HideInInspector] public Vector3 Offset = Vector3.zero;
    private RaycastHit hit;

    public float speed;
    public Vector3 direction;
    public bool turnback = false;

    private void FixedUpdate()
    {
        Physics.Raycast(transform.position + transform.forward / 3 + transform.up, -transform.up, out hit, 100, layer);
        if (GameState.Instance.GState == State.Playing)
        {
            direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
            if (hit.collider != null)
            {
                Offset = (Vector3.right * direction.x + Vector3.forward * direction.z) * (Time.deltaTime * speed);
                transform.position += Offset;
            }
            transform.forward = new Vector3(direction.x, 0, direction.z);
        }

        if (playerCollision.OnBridge)
        {
            if (variableJoystick.Vertical < 0.0f)
            {
                GameManager.Instance.playerCollision.OnBuildRoad = false;
                if (hit.collider.CompareTag("Road"))
                {
                    hit.collider.GetComponent<BridgeControl>().EnableRoad();
                }
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

    public void SetJoystick(AxisOptions option)
    {
        variableJoystick.AxisOptions = option;
    }
}
