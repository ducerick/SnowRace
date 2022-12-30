using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LongBridgeRay : MonoBehaviour
{
    public Transform Player;
    public Transform Obstacle;
    public List<Transform> ListPosObstacle = new List<Transform>();
    private Vector3[] positionsObstacle = new Vector3[19];
    public List<Transform> ListPosPlayer = new List<Transform>();
    private Vector3[] positionsPlayer = new Vector3[3];

    void Start()
    {
        Init();
    }

    private void Update()
    {
    }

    public void Init()
    {
        for (int i = 0; i < ListPosObstacle.Count; i++)
        {
            positionsObstacle[i] = ListPosObstacle[i].position;
        }
        for (int i = 0; i < ListPosPlayer.Count; i++)
        {
            positionsPlayer[i] = ListPosPlayer[i].position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player.SetParent(Obstacle);
            Player.localPosition = new Vector3(0, 1, -1);
            Obstacle.DOPath(positionsObstacle, positionsObstacle.Length * 0.2f).OnComplete(() =>
            {
                Player.SetParent(null);
                Obstacle.GetComponent<Rigidbody>().isKinematic = false;
                Player.DOPath(positionsPlayer, positionsPlayer.Length);
            });
        }
    }

}
