using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class BridgePlayer : MonoBehaviour
{
    [SerializeField] private List<GameObject> Road = new List<GameObject>();

    private void Start()
    {
        foreach(var obj in Road)
        {
            obj.GetComponent<Renderer>().enabled = false;
        }
    }

    private void Update()
    {
       
    }
    private void OnEnable()
    {
        EventDispatcher.Instance.RegisterListener(EventID.OnCharacterBuildRoad, OnCharacterBuildRoad);
    }

    private void OnCharacterBuildRoad(object obj)
    {
        if (Road.Contains((GameObject)obj))
        {
            Road[Road.IndexOf((GameObject)obj)].GetComponent<Renderer>().enabled = true;
        }
    }

}

