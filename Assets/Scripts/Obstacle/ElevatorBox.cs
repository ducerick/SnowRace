using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorBox : MonoBehaviour
{
    public Transform Obstacle;
    public Transform Door;
    public Transform Door1;
    public List<Transform> ListPosObstacle = new List<Transform>();
    private Vector3[] positionsObstacle = new Vector3[1];
    public List<Transform> ListPosPlayer = new List<Transform>();
    private Vector3[] positionsPlayer = new Vector3[1];

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
            other.transform.localPosition = new Vector3(0, 1, -1);
            CloseDoor(other.transform);
        }
    }

    void CloseDoor(Transform player)
    {
        Door.DOLocalMoveY(0, 1f).OnComplete(() => 
        {
            Obstacle.DOPath(positionsObstacle, positionsObstacle.Length * 2).OnComplete(()=> { 
                OpenDoor(player);
            });
        });
    }

    void OpenDoor(Transform player)
    {
        player.SetParent(null);
        Door1.DOLocalMoveY(-2, 1f).OnComplete(() =>
        {
            player.DOPath(positionsPlayer, positionsPlayer.Length).OnComplete(()=>
            {
                AI.Instance.agent.enabled = true;
                AI.Instance.currState = AI.Instance.rollSnowState;
            });
        });
    }
}
