using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collision : MonoBehaviour
{
    public BridgePlayer bridge;
    private bool colBridge = false;
    private bool colWater = false;
    private Vector3 velocity;
    private Rigidbody myRigidbody;

    private void Start()
    {
        myRigidbody = transform.GetComponent<Rigidbody>();
        velocity = myRigidbody.velocity;
    }

    private void Update()
    {
        if (colBridge)
        {
            BuildRoad();
        }
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
            PlayerController.OnPlane = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("IceBridge"))
        {
            colBridge = true;
            EventDispatcher.Instance.PostEvent(EventID.OnCharacterBuildRoad, gameObject);
        }

        if (other.CompareTag("Water"))
        {
            colWater = true;
        }
    }

    private void OnCollisionExit(UnityEngine.Collision collision)
    {
        if (collision.transform.CompareTag("Plane"))
        {
            PlayerController.OnPlane = false;
            if (!colBridge)
            {
                velocity = Vector3.zero;
                transform.GetComponent<Rigidbody>().AddForce(Vector3.right, ForceMode.Force);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("IceBridge"))
        {
            colBridge = false;
            JoystickPlayer.directOnBuild = false;
        }
    }


    private void BuildRoad()
    {
        if (SnowBall.Instance.BallScale.x >= 0.0f)
        {
            var offset = velocity.z * Time.deltaTime;
            float compress = offset * SnowBall.Instance.MaxBallScale / bridge.SizeRoad;
            SnowBall.Instance.BallScale -= new Vector3(compress, compress, compress);
        }
        else
        {
            velocity = Vector3.zero;
            SnowBall.Instance.mouseMove = false;
        }
    }

    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(1f);
        transform.localPosition = Vector3.zero;
        SnowBall.Instance.BallScale = Vector3.zero;
        velocity = Vector3.zero;
        AnimatorPlayer.Instance.Reset();
        PlayerController.OnPlane = true;
        colWater = false;
    }
}
