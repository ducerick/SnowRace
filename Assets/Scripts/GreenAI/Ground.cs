using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class Ground : MonoBehaviour
{
    public AIBridgeController[] aiBridgeControllers = new AIBridgeController[3];// black,red,green,blue
    public List<Transform> wayPoints;
    //[SerializeField] private GameObject _waypointPrefab;
    //[SerializeField] [Range(.1f, 3)] float spacingX;
    //[SerializeField] [Range(.1f, 3)] float spacingZ;
    //const float minSpacing = .1f;
    //[SerializeField] Vector3 scale;
    //[ContextMenu("Generate")]
    //void Generate()
    //{
    //    DestroyObjects();
    //    scale = transform.localScale;
    //    if (_waypointPrefab == null && scale.x < .1f && scale.z < .1f)
    //        return;
    //    spacingX = Mathf.Max(spacingX, minSpacing);
    //    spacingZ = Mathf.Max(spacingZ, minSpacing);
    //    Vector3 pos = transform.localPosition;
    //    pos.x -= (scale.x/2) - spacingX;
    //    pos.y = scale.y/2;
    //    pos.z -= (scale.z/2) - spacingZ;
    //    Debug.Log((scale.x / spacingX) + " x");
    //    Debug.Log((scale.z / spacingZ) + " z");
    //    float oldposz = pos.z;
    //    for (int i = 1; i < (scale.x / spacingX) -1; i++)
    //    {
    //        pos.z = oldposz;
    //        pos.x += spacingX;
    //        for (int j = 0; j < (scale.z / spacingZ)  ; j++)
    //        {
    //            pos.z += spacingZ;
    //            var obj = Instantiate(_waypointPrefab, pos, Quaternion.identity,this.transform);
    //            wayPoints.Add(obj.transform);
    //        }
    //    }
    //}
    //[ContextMenu("Destroy")]
    //void DestroyObjects()
    //{
    //    for (int i = 0; i < wayPoints.Count; i++)
    //    {
    //        DestroyImmediate (wayPoints[i].gameObject,false);
    //    }
    //    wayPoints.Clear();
    //}
    
}
