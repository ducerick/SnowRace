using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    [SerializeField] private Transform snowBall;
    [SerializeField] private Transform player;
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
                Vector3 scale = Vector3.one * expansionSpeed;
                snowBall.localScale += scale;
                snowBall.localPosition = new Vector3(0,snowBall.localScale.y * 0.5f, snowBall.localScale.z * 0.5f + 0.5f);
                AnimatorPlayer.Instance.RollingBall();
            }
            snowBall.Rotate(new Vector3(0, 0, 1));
        }
    }
}
