using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LongBridgeRay : MonoBehaviour
{
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
            other.transform.DOKill();
            other.transform.SetParent(Obstacle);
            other.transform.localPosition = new Vector3(0, 1, 0);
            Obstacle.DOPath(positionsObstacle, positionsObstacle.Length * 0.2f).OnComplete(() =>
            {
                other.transform.SetParent(null);
                Obstacle.GetComponent<Rigidbody>().isKinematic = false;
                other.transform.DOPath(positionsPlayer, positionsPlayer.Length).OnComplete(() => {
                    AI.Instance.agent.enabled = true;
                    AI.Instance.currState = AI.Instance.rollSnowState;
                });
            });
        }
    }

}
