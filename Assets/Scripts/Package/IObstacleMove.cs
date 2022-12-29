using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacleMove
{
    void StartBridgeRay();
    void Init();
    IEnumerator moveObstacle();
    IEnumerator movePlayer();
    IEnumerator Moving(int currentPosition, Vector3[] pos, Transform obj);
}
