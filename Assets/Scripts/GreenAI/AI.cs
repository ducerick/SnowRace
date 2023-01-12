using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public int aiIndex;
    public NavMeshAgent agent;
    public AIAnimations animations;
    public RollSnowState rollSnowState;
    public MakeBridgeState makeBridgeState;
    public OnLongBridgeState onLongBridgeState;
    [SerializeField] private AIState _currState;

    public static AI Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
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
