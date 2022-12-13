using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    [SerializeField] private Transform snowBall;
    private float expansionSpeed = 0.002f;
    private bool mouseMove;
    private Vector3 resetPosition;
    // Start is called before the first frame update
    void Start()
    {
        snowBall.localScale = new Vector3(0, 0, 0);
        mouseMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMouseMove();
        BallExpansion();
    }

    private void CheckMouseMove()
    {
        if (Input.GetMouseButtonDown(0) && mouseMove == false)
        {
            resetPosition = Input.mousePosition;
            mouseMove = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseMove = false;
        }
    }

    private void BallExpansion()
    {
        if (mouseMove)
        {
            if (Input.mousePosition != resetPosition)
            {
                snowBall.localScale += Vector3.one * expansionSpeed;
                AnimatorPlayer.Instance.RollingBall();
            }
        }
    }
}
