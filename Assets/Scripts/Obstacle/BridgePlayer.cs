using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class BridgePlayer : MonoBehaviour
{
    public Transform Road;
    public Transform MyBridge;
    public bool buildRoad = false;
    private GameObject player;
    public float SizeRoad;
    public float MaxPos;
    private void Start()
    {
        SizeRoad = Road.localScale.y;
        MaxPos = -Road.localPosition.z;
    }

    private void LateUpdate()
    {
        if (buildRoad && Road.localPosition.z < MaxPos)
        {
            float offset = player.GetComponent<Rigidbody>().velocity.z * Time.deltaTime;
            Road.localPosition += new Vector3(0, 0, offset);
        }
    }

    private void OnEnable()
    {
        EventDispatcher.Instance.RegisterListener(EventID.OnCharacterUnBuild, OnCharacterUnBuild);
    }

    private void OnCharacterUnBuild(object obj)
    {
        buildRoad = false;
        player = (GameObject)obj;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.Instance.snowBall.BallScale.x >= 0f)
            {
                buildRoad = true;
                player = other.gameObject;
                EventDispatcher.Instance.PostEvent(EventID.OnCharacterBuildRoad);
            }
        }
    }

}

