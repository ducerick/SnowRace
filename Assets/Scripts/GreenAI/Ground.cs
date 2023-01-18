using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class Ground : MonoBehaviour
{
    public AIBridgeController[] aiBridgeControllers = new AIBridgeController[3];// black,red,green,blue
    public List<Transform> wayPoints;
}
