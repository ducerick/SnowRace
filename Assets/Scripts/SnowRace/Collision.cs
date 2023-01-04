using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collision : MonoBehaviour
{
    [SerializeField] private BridgePlayer bridge;
    [SerializeField] private JoystickPlayer joystick;
    [SerializeField] bool onBridge = false;
    //private Vector3 velocity;
    //private Rigidbody myRigidbody;
    private PlayerController _player;
    //private Transform planeTranform;

    private void Start()
    {
        //myRigidbody = transform.GetComponent<Rigidbody>();
        //velocity = myRigidbody.velocity;
        _player = GameManager.Instance.player;
    }

    private void Update()
    {
        if (onBridge)
        {
            transform.position = new Vector3(bridge.transform.position.x, transform.position.y, transform.position.z);
        }
    }
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        //if (collision.transform.CompareTag("Boat"))
        //{
        //    transform.SetParent(collision.transform);
        //    transform.localPosition = new Vector3(1, 0, 3);
        //}

        if (collision.transform.CompareTag("Plane"))
        {
            _player.OnPlane = true;
            //planeTranform = collision.transform;
        }

        //if (collision.transform.CompareTag("IceBridge"))
        //{
        //    onBridge = true;
        //    joystick.SetJoystick(AxisOptions.Vertical);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("Water"))
        //{
        //    StartCoroutine(ResetGame());
        //}

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

        //if (collision.transform.CompareTag("IceBridge"))
        //{
        //    onBridge = false;
        //    joystick.SetJoystick(AxisOptions.Both);
        //}
    }

    //IEnumerator ResetGame()
    //{
    //    yield return new WaitForSeconds(1f);
    //    transform.position = planeTranform.position + new Vector3(0, 0.06f, 0);
    //    GameManager.Instance.snowBall.BallScale = Vector3.zero;
    //    velocity = Vector3.zero;
    //    AnimatorPlayer.Instance.Reset();
    //    _player.OnPlane = true;
    //}

    public bool GetCollisionBridge() => onBridge;
}
