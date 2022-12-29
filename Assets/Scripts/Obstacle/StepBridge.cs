using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StepBridge : MonoBehaviour
{
    [SerializeField] private List<GameObject> Step = new List<GameObject>();
    public bool buildStep;
    private void Start()
    {
        foreach(var obj in Step)
        {
            obj.GetComponent<Renderer>().enabled = false;
        }
    }

    private void OnEnable()
    {
        EventDispatcher.Instance.RegisterListener(EventID.OnCharacterBuildStep, OnCharacterBuildStep);
        EventDispatcher.Instance.RegisterListener(EventID.OnCharacterUnBuildStep, OnCharacterUnBuildStep);
    }

    private void OnCharacterUnBuildStep(object obj)
    {
        buildStep = false;
    }


    private void OnCharacterBuildStep(object obj)
    {
        if(Step.Contains((GameObject)obj) )
        {
            var step = Step[Step.IndexOf((GameObject)obj)].GetComponent<Renderer>();
            if (step.enabled)
            {
                buildStep = false;
            }
            else
            {
                step.enabled = true;
                buildStep = true;
            }
        }
    }
}
