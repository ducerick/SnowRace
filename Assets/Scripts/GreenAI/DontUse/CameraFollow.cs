using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    [SerializeField] GameObject followObject;
    [SerializeField] Vector3 offset;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        //if(!GameManager.instance.isFinish)
        //    Follow();   
    }
    void Follow()
    {
        transform.position = Vector3.Lerp(transform.position, followObject.transform.position + offset, .1f);
    }
    public void WinAnim(Transform _transform)
    {
        transform.DOMove(_transform.position + offset, 2f);
    }
}
