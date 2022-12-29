using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collision : MonoBehaviour
{
    public BridgePlayer bridge;
    public JoystickPlayer joystick;
    private bool onBridge = false;
    private bool colWater = false;
    private Vector3 velocity;
    private Rigidbody myRigidbody;
    private PlayerController _player;
    private Transform planeTranform;

    private void Start()
    {
        myRigidbody = transform.GetComponent<Rigidbody>();
        velocity = myRigidbody.velocity;
        _player = GameManager.Instance.player;
    }

    private void Update()
    {
        if (colWater)
        {
            StartCoroutine(ResetGame());
        }

    }
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.transform.CompareTag("Boat"))
        {
            transform.SetParent(collision.transform);
            transform.localPosition = new Vector3(1, 0, 3);
        }

        if (collision.transform.CompareTag("Plane"))
        {
            _player.OnPlane = true;
            planeTranform = collision.transform;
        }

        if (collision.transform.CompareTag("IceBridge"))
        {
            onBridge = true;
            joystick.SetJoystick(AxisOptions.Vertical);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            colWater = true;
        }

        if (other.CompareTag("Ray"))
        {
            onBridge = false;
        }

        if (other.CompareTag("Elevator"))
        {
            onBridge = false;
        }

        if (other.CompareTag("Step"))
        {
            onBridge = true;
            
            EventDispatcher.Instance.PostEvent(EventID.OnCharacterBuildStep, other.gameObject);
        }

        if (other.CompareTag("IceBridge"))
        {
            onBridge = true;
            joystick.SetJoystick(AxisOptions.Vertical);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("IceBridge"))
        {
            onBridge = false;
            joystick.SetJoystick(AxisOptions.Both);
        }
    }


    private void OnCollisionExit(UnityEngine.Collision collision)
    {
        if (collision.transform.CompareTag("Plane"))
        {
            _player.OnPlane = false;
        }

        if (collision.transform.CompareTag("IceBridge"))
        {
            onBridge = false;
            joystick.SetJoystick(AxisOptions.Both);
        }
    }

    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(1f);
        transform.position = planeTranform.position + new Vector3(0, 0.06f, 0);
        SnowBall.Instance.BallScale = Vector3.zero;
        velocity = Vector3.zero;
        AnimatorPlayer.Instance.Reset();
        _player.OnPlane = true;
        colWater = false;
    }

    public bool GetCollisionBridge() => onBridge;
}
