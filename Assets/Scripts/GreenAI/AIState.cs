using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState : MonoBehaviour
{
    public abstract void StartState(AIAnimations action); 
    public abstract void UpdateState(AIAnimations action);
}
