using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    [SerializeField] private Transform snowBall;
    [SerializeField] private Transform player;
    private float expansionSpeed = 0.005f;
    private float compressionSpeed = 0.001f;
    
    private Vector3 resetPosition;
    private const float maxScaleBall = 3.0f;
    private bool fallWater = false;

    public float MaxBallScale = 3.0f;
    public bool mouseMove;
    public Vector3 BallScale
    {
        get { return transform.localScale; }
        set { transform.localScale = value; }
    }

    public static SnowBall Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        snowBall.localScale = new Vector3(0, 0, 0);
        mouseMove = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!fallWater)
        {
            CheckMouseMove();
            BallExpansion();
        }
        if (fallWater)
        {
            BallCompress();
        }
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
                if (snowBall.localScale.y <= maxScaleBall)
                {
                    Vector3 scale = Vector3.one * expansionSpeed;
                    snowBall.localScale += scale;
                    snowBall.localPosition = new Vector3(0, snowBall.localScale.y * 0.5f, snowBall.localScale.z * 0.5f + 0.5f);
                }
                AnimatorPlayer.Instance.RollingBall();
            }
            snowBall.Rotate(new Vector3(1, 0, 0), 10);
        }
    }

    public void BallCompress()
    {
        if (snowBall.localScale.x >= 0.0f)
        {
            Vector3 scale = Vector3.one * compressionSpeed;
            snowBall.localScale -= scale;
        }
    }

    public bool GetMouseMove() => mouseMove;

    public float GetCompressionSpeed() => compressionSpeed;

    public float GetExpansionSpeed() => expansionSpeed;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Water"))
    //    {
    //        snowBall.SetParent(null);
    //        fallWater = true;
    //    }
    //}
}
