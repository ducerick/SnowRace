using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIStepBridge : MonoBehaviour
{
    [SerializeField] private List<GameObject> Step = new List<GameObject>();

    private void Start()
    {
        foreach (var obj in Step)
        {
            obj.GetComponent<Renderer>().enabled = false;
        }
    }

}
