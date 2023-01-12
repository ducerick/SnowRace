using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBridgeController : MonoBehaviour
{
    public Transform startPos, finishPos, acrossBridge;
    [SerializeField] Transform Road;
    public bool isFinish;
    [HideInInspector] public float SizeRoad = 10f;
    public void StretchBridge(float value)
    {
        if (Road.localPosition.z >= SizeRoad/2 - 0.5)
        {
            isFinish = true;
            return;
        }
        Road.localPosition += new Vector3(0, 0, value);
        Debug.Log($"Bridge Pos: {Road.localPosition}");
    }
}

