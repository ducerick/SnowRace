using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collision : MonoBehaviour
{
    private bool colBridge = false;
    private void Update()
    {
        if (colBridge)
        {
            BuildRoad();
        }
    }
    private void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.transform.CompareTag("Boat"))
        {
            transform.SetParent(collision.transform);
            transform.localPosition = new Vector3(1, 0, 3);
        }

        if (collision.transform.CompareTag("IceBridge"))
        {
            colBridge = true;
            EventDispatcher.Instance.PostEvent(EventID.OnCharacterBuildRoad, collision.gameObject);
        }
    }


    private void BuildRoad()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var moveForward = SnowBall.Instance.GetCompressionSpeed();
            transform.localPosition += new Vector3(0, 0, moveForward);
            SnowBall.Instance.BallCompress();
        }
    }
}
