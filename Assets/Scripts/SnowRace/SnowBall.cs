using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBall : MonoBehaviour
{
    [SerializeField] private Transform snowBall;
    private float expansionSpeed = 0.005f;
    private float compressionSpeed = 0.001f;

    private Vector3 resetPosition;
    private const float maxScaleBall = 3.0f;

    public float MaxBallScale = 3.0f;
    public bool mouseMove;
    private float brideSize;

    [SerializeField] public GameObject Player;
    public Vector3 BallScale
    {
        get { return transform.localScale; }
        set { transform.localScale = value; }
    }

    private void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {
        snowBall.localScale = new Vector3(0, 0, 0);
        brideSize = GameManager.Instance.bridgePlayer.SizeRoad;
        mouseMove = false;

    }

    // Update is called once per frame
    void Update()
    {
        CheckMouseMove();
        if (GameManager.Instance.playerCollision.GetCollisionBridge() 
            && GameManager.Instance.joystickPlayer.turnback == false
            && (GameManager.Instance.bridgePlayer.buildRoad 
            || GameManager.Instance.stepBridge.buildStep))
        {
            BallCompress();
        }
    }

    private void FixedUpdate()
    {
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
            snowBall.Rotate(new Vector3(1, 0, 0), 10);
            if (GameManager.Instance.player.OnPlane)
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
            }

        }
    }

    public void BallCompress()
    {
        if (BallScale.x >= 0f)
        {
            float offset = Player.GetComponent<Rigidbody>().velocity.z * Time.deltaTime;
            float compress = offset * MaxBallScale / brideSize;
            BallScale -= new Vector3(compress, compress, compress);
            snowBall.localPosition = new Vector3(0, snowBall.localScale.y * 0.5f, snowBall.localScale.z * 0.5f + 0.5f);
        }
        else if (BallScale.x < 0f)
        {
            GameState.Instance.GState = State.Stop;
        }

    }

    public bool GetMouseMove() => mouseMove;

    public float GetCompressionSpeed() => compressionSpeed;

    public float GetExpansionSpeed() => expansionSpeed;

}
