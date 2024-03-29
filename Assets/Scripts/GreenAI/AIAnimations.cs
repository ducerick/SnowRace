using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAnimations : MonoBehaviour
{
    [SerializeField] private AISnowBall aiSnowBall;
    [SerializeField] private Animator animator;
    private float changeState = 0.0f;
    private float accleration = 0.1f;

    public void RollingBall()
    {
        if (aiSnowBall.GetSnowScale() < 0.5f && changeState < 0.5f)
        {
            animator.SetInteger("stage", 12);
            changeState = aiSnowBall.GetSnowScale();
            animator.SetFloat("change_stage", changeState);
        }
        else if (aiSnowBall.GetSnowScale() > 0.5f && changeState < 0.8f)
        {
            changeState += Time.deltaTime * accleration;
            animator.SetFloat("change_stage", changeState);
        }
        else
        {
            animator.SetInteger("stage", 3);
        }
    }

    public void StartRoll()
    {
        animator.SetFloat("change_stage", 0);
        changeState = 0;
    }

}
