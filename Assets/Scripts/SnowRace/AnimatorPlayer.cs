using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimatorPlayer : MonoBehaviour
{
    [SerializeField] private SnowBall snowBall;
    private float changeState = 0.0f;
    private Animator animator;
    private float accleration = 0.1f;

    public static AnimatorPlayer Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Idle()
    {
        animator.SetInteger("stage", 0);
    }

    public void RunInPlace()
    {
        animator.SetInteger("stage", 1);
    }

    public void RollingBall()
    {
        animator.SetInteger("stage", 12);
        if(snowBall.transform.localScale.y < 0.5f && changeState < 0.5f)
        {
            changeState = snowBall.transform.localScale.y;
            animator.SetFloat("change_stage", changeState);
        }
        else if(snowBall.transform.localScale.y > 0.5f && changeState < 0.8f)
        {
            changeState += Time.deltaTime * accleration;
            animator.SetFloat("change_stage", changeState);
        }
        else
        {
            animator.SetInteger("stage", 3);
        }
    }
}
