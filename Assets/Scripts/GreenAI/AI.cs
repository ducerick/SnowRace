using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public int aiIndex;
    public NavMeshAgent agent;
    public AIAnimations animations;
    public WinState winState;
    public RollSnowState rollSnowState;
    public DeathState deathState;
    public MakeBridgeState makeBridgeState;
    [SerializeField] private AIState _currState;
    public AIState currState
    {
        get => _currState;
        set
        {
            _currState = value;
            _currState.StartState(animations);
        }
    }

    private void Start()
    {
        animations = GetComponent<AIAnimations>();
        agent = GetComponent<NavMeshAgent>();
        currState = rollSnowState;
        currState.StartState(animations);
    }
    private void Update()
    {
        currState.UpdateState(animations);
    }

}
