using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeControl : MonoBehaviour
{
    [HideInInspector] public float SizeRoad = 10f;
    [SerializeField] private Transform Road;


    public void PushRoad()
    {
        if (GameManager.Instance.snowBall.BallScale.x >= 0f)
        {
            Road.localPosition += new Vector3(0, 0, GameManager.Instance.joystickPlayer.Offset.z);
            GameManager.Instance.snowBall.BallCompress(SizeRoad);
        }
        else
        {
            Road.GetComponentInChildren<BoxCollider>().isTrigger = false;
        }
    }

    public void EnableRoad()
    {
        Road.GetComponentInChildren<BoxCollider>().isTrigger = true;
    }
}


