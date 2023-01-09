using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public Transform startPos, finishPos,acrossBridge;
    public bool isFinish;
    [SerializeField] float maxLenght;
    public float scaleMultiplier = 0.5f;
    public void StretchBridge(float value)
    {
        if(transform.localScale.x >= maxLenght)
        {
            isFinish = true;
            return;
        }
        Vector3 scale = transform.localScale;
        scaleMultiplier += value;
        scale.x = scaleMultiplier;
        transform.localScale = scale;
        transform.Translate(-transform.forward * ( value / 2));
    }
}
