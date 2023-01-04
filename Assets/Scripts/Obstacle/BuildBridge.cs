using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBridge : MonoBehaviour
{
    public Camera cam = null;
    public LineRenderer lineRenderer = null;
    private Vector3 playerPos;
    private Vector3 Pos;
    private Vector3 previousPos;
    public List<Vector3> linePosition = new List<Vector3>();

    private void Update()
    {
        if (GameManager.Instance.playerCollision.GetCollisionBridge())
        {
            playerPos = transform.position;
            Pos = cam.ScreenToWorldPoint(playerPos);
            previousPos = Pos;
            linePosition.Add(Pos);
        }
    }
}
