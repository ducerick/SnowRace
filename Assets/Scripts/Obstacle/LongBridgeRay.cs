using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongBridgeRay : MonoBehaviour, IObstacleMove
{
    public Transform Player;
    public Transform Obstacle;
    public List<Transform> ListPosObstacle = new List<Transform>();
    private Vector3[] positionsObstacle = new Vector3[19];
    public List<Transform> ListPosPlayer = new List<Transform>();
    private Vector3[] positionsPlayer = new Vector3[3];
    public float MoveSpeed = 8;
    private int changeState = 0;
    private bool onObstacle = false;
    Coroutine MoveIE;

    void Start()
    {
        Init();
    }

    private void Update()
    {
        //Debug.Log(Player.finishBridge);
        if (onObstacle)
        {
            StartBridgeRay();
            onObstacle = false;
        }
        if (changeState == 1 && !onObstacle)
        {
            Obstacle.GetComponent<Rigidbody>().isKinematic = false;
            Player.GetComponent<Rigidbody>().isKinematic = true;
            Player.SetParent(null);
            StartCoroutine(movePlayer());
            changeState = 0;
        }
        if (changeState == -1 && !onObstacle)
        {
            Player.GetComponent<Rigidbody>().isKinematic = false;
            changeState = 0;
        }

    }

    public void StartBridgeRay()
    {
        Player.SetParent(Obstacle);
        Player.localPosition = new Vector3(0, 1, -1);
        if (changeState == 0) StartCoroutine(moveObstacle());
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

    public IEnumerator moveObstacle()
    {
        for (int i = 0; i < positionsObstacle.Length; i++)
        {
            MoveIE = StartCoroutine(Moving(i, positionsObstacle, Obstacle));
            yield return MoveIE;
        }
        changeState = 1;
    }

    public IEnumerator movePlayer()
    {
        for (int i = 0; i < positionsPlayer.Length; i++)
        {
            MoveIE = StartCoroutine(Moving(i, positionsPlayer, Player));
            yield return MoveIE;
        }
        changeState = -1;
        onObstacle = false;
    }

    public IEnumerator Moving(int currentPosition, Vector3[] pos, Transform obj)
    {
        while (obj.transform.position != pos[currentPosition])
        {
            obj.transform.position = Vector3.MoveTowards(obj.transform.position, pos[currentPosition], MoveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            onObstacle = true;
        }
    }

}
