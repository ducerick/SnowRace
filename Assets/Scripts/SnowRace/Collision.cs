using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collision : MonoBehaviour
{
    private Transform bridge;
    [SerializeField] private JoystickPlayer joystick;
    [SerializeField] private LayerMask layer;
    private RaycastHit hit;
    private bool onBuildRoad;

    public bool OnBuildRoad
    {
        get { return onBuildRoad; }
        set { onBuildRoad = value; }
    }


    private bool onBridge;

    public bool OnBridge
    {
        get { return onBridge; }
        set { onBridge = value; }
    }


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
        Physics.Raycast(transform.position + Vector3.up, -transform.up, out hit, 100, layer);
        if (onBridge)
        {
            transform.position = new Vector3(bridge.transform.position.x, transform.position.y, transform.position.z);
        }

    }

    private void FixedUpdate()
    {
        if (onBuildRoad && GameState.Instance.GState == State.Playing)
        {
            if (hit.collider.CompareTag("Road"))
                hit.collider.GetComponent<BridgeControl>().PushRoad();
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
            onBuildRoad = false;
        }

        if (other.CompareTag("Elevator"))
        {
            onBuildRoad = false;
            onBridge = false;
        }

        if (other.CompareTag("Step"))
        {
            //if (GameManager.Instance.snowBall.BallScale.x >= 0f)
            //{
            //    onBridge = true;
            //    EventDispatcher.Instance.PostEvent(EventID.OnCharacterBuildStep, other.gameObject);
            //} else
            //{
            //    GameState.Instance.GState = State.Stop;
            //}
            onBridge = true;
            EventDispatcher.Instance.PostEvent(EventID.OnCharacterBuildStep, other.gameObject);

        }

        if (other.CompareTag("IceBridge"))
        {
            bridge = other.transform;
            onBridge = true;
            joystick.SetJoystick(AxisOptions.Vertical);

        }

        if (other.CompareTag("PlayerRoad"))
        {
            onBuildRoad = true;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("IceBridge"))
        {
            onBridge = false;
            onBuildRoad = false;
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
}
