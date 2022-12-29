using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LongBridgeFat : MonoBehaviour
{
    public Transform Player;
    public List<Transform> ListPosPlayer = new List<Transform>();
    private Vector3[] positionsPlayer = new Vector3[21];

    void Start()
    {
        Init();
    }

    public void Init()
    {
        for (int i = 0; i < ListPosPlayer.Count; i++)
        {
            positionsPlayer[i] = ListPosPlayer[i].position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.DOPath(positionsPlayer, positionsPlayer.Length * 0.5f).OnComplete(()=>
            {
                GamePopupMenu.Instance.gameObject.SetActive(true);
            });
        }
    }

}
