using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MakeBridgeState : AIState
{
    [SerializeField] bool backStartPos = false;
    [SerializeField] bool isFinishing;
    [SerializeField] AISnowBall aiSnowBall;
    public AIBridgeController aiBridgeController;
    [SerializeField] private AI ai;
    public override void StartState(AIAnimations action)
    {
        isFinishing = false;
        ai.agent.SetDestination(aiBridgeController.finishPos.position);
    }
    public override void UpdateState(AIAnimations action)
    {
        if(backStartPos)  // roll ball
        {
            ai.agent.SetDestination(aiBridgeController.startPos.position);
            if (Vector3.Distance(ai.transform.position, aiBridgeController.startPos.position) < .1f)
            {
                ai.currState = ai.rollSnowState;
                backStartPos = false;
            }
        }
        else
        {
            ai.agent.SetDestination(aiBridgeController.finishPos.position);
        
            if(aiSnowBall.GetSnowScale() <= 0 && !isFinishing)
            {
                backStartPos = true;
            }
            if (aiBridgeController.isFinish)
            {
                isFinishing = true;
            }
            if (isFinishing)
            {
                ai.currState = ai.onLongBridgeState;
                ai.agent.enabled = false;
                ai.transform.DOMoveZ(aiBridgeController.acrossBridge.position.z, 1f);
            }
        }
    }

    public bool IsFinish() => isFinishing;
}
