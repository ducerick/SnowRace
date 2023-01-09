using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    AISnowBall aiSnowBall;
    private void Start()
    {
        aiSnowBall = GetComponentInParent<AISnowBall>();
        Physics.IgnoreCollision(GetComponent<Collider>(), GetComponentInParent<BoxCollider>());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            aiSnowBall.Fall(other);
        }
    }
}
