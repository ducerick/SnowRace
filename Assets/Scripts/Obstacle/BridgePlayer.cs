using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class BridgePlayer : MonoBehaviour
{
    public Transform Road;
    private bool buildRoad = false;
    private GameObject player;
    public float SizeRoad;
    public float MaxPos;
    private void Start()
    {
        SizeRoad = Road.localScale.y;
        MaxPos = -Road.localPosition.z;
    }


    private void FixedUpdate()
    {
        if (buildRoad && Road.localPosition.z < MaxPos )
        {
            float offset = player.GetComponent<Rigidbody>().velocity.z * Time.deltaTime;
            Road.localPosition += new Vector3(0, 0, offset);
        }
    }

    private void OnEnable()
    {
        EventDispatcher.Instance.RegisterListener(EventID.OnCharacterUnBuild, OnCharacterUnBuild);
        EventDispatcher.Instance.RegisterListener(EventID.OnCharacterBuildRoad, OnCharacterBuildRoad);
    }

    private void OnCharacterUnBuild(object obj)
    {
        buildRoad = false;
        player = (GameObject)obj;
    }

    private void OnCharacterBuildRoad(object obj)
    {
        buildRoad = true;
        player = (GameObject)obj;
    }
}

