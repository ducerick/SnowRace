using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollSnowState : AIState
{
    [SerializeField] LayerMask layer;
    [SerializeField] private AISnowBall aiSnowBall;
    [SerializeField] float minCollectedSnow, maxCollectedSnow;
    [SerializeField] float requiredSnow;
    [SerializeField] private AI ai;
    RaycastHit hit;
    Ground ground;
    Vector3 currDestination;

    public override void StartState(AIAnimations action)
    {
        requiredSnow = Random.Range(minCollectedSnow, maxCollectedSnow);
        currDestination = FindWaypoint().position;
        ai.agent.SetDestination(currDestination);
        ai.animations.StartRoll();
    }

    public override void UpdateState(AIAnimations action)
    {
        if (aiSnowBall.GetSnowScale() < requiredSnow)
        {
            ai.animations.RollingBall();
            StartCoroutine(aiSnowBall.CollectSnow(requiredSnow));
        }
        if (aiSnowBall.GetSnowScale() >= requiredSnow)
        {
            ai.makeBridgeState.aiBridgeController = ground.aiBridgeControllers[ai.aiIndex];
            ai.currState = ai.makeBridgeState;
            return;
        }
        else if (Vector3.Distance(currDestination, ai.transform.position) < 1f)
        {
            currDestination = FindWaypoint().position;
            ai.agent.SetDestination(currDestination);
        }
    }
    public Transform FindWaypoint()
    {
        Physics.Raycast(ai.transform.position + Vector3.up, -transform.up, out hit, 100, layer);
        Debug.DrawRay(ai.transform.position + Vector3.up, -transform.up, Color.red);
        if (hit.collider.TryGetComponent(out Ground _ground) && hit.collider != null)
        {
            this.ground = _ground;
        }
        Transform destination = ground.wayPoints[Random.Range(0, ground.wayPoints.Count - 1)].transform;
        return destination;
    }
}
