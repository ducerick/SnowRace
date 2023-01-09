using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBridgeController : MonoBehaviour
{
    public Transform startPos, finishPos, acrossBridge;
    public bool isFinish;
    [HideInInspector] public float SizeRoad = 10f;
    public float AddPosition = 0f;
    public void StretchBridge(float value)
    {
        if (transform.position.x >= SizeRoad/2)
        {
            isFinish = true;
            return;
        }
        AddPosition += value;
        transform.localPosition += new Vector3(0, 0, AddPosition);
    }
}

