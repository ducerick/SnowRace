using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISnowBall : MonoBehaviour
{
    public bool isFall; 
    float bridgeMultiplier = 5;
    RaycastHit hit;
    [SerializeField] SphereCollider sphereCollider;
    [SerializeField] float scaleMultiplier = 0f;
    [SerializeField] Transform snowball;
    [SerializeField] LayerMask layer;
    [SerializeField] AIAnimations anim;

    private void Start()
    {
        sphereCollider = GetComponentInChildren<SphereCollider>();
    }


    private void Update()
    {
        snowball.Rotate(new Vector3(1, 0, 0), 10);
        //if (GameManager.instance. isFinish)
        //    return;
        //Physics.Raycast(transform.position + Vector3.up / 5, transform.forward, out hit, 10, layer);
        //Debug.DrawRay(transform.position + Vector3.up / 5 , transform.forward );
        //if (hit.collider == null)
        //{
        //    return;
        //}
        //if(hit.collider.tag == "Finish")
        //{
        //    if(TryGetComponent(out AI ai))
        //    {
        //        ai.agent.isStopped = true;
        //    }
        //    if(TryGetComponent(out Movement movement))
        //    {
        //        movement.speed = 0;
        //    }
        //    anim.SetBool("Run", false);
        //    anim.SetBool("Idle", false);
        //    anim.SetTrigger("Win");
        //    //GameManager.instance.isFinish = true;
        //    CameraFollow.instance.WinAnim(transform);
        //}
        //if (hit.collider.tag == "Plane"  )
        //{
        //    CollectSnow();
        //}
        //if (hit.collider.tag == "AIRoad" && snowball.transform.localScale.x > 0)
        //{
        //    Debug.Log($"Ai Pos: {transform.position}");
        //    Debug.Log($"Ball Scale: {snowball.localScale.x}");
        //    //transform.position = new Vector3(hit.collider.transform.position.x, transform.position.y, transform.position.z);
        //    if (!hit.collider.GetComponentInParent<AIBridgeController>().isFinish)
        //    {
        //        hit.collider.GetComponentInParent<AIBridgeController>().StretchBridge((.005f * bridgeMultiplier));
        //        MakeBridge(hit.collider.gameObject);
        //    }
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AIRoad"))
        {
            StartCoroutine(MakeBridge(other));
        }

        if (other.CompareTag("Step"))
        {
            other.GetComponent<Renderer>().enabled = true;
        }
        //Fall(other);
    }
    public void Fall(Collider other)
    {
        Transform rollSnow;
        if (other.gameObject.tag == "Snowball")
        {
            rollSnow = other.GetComponent<SnowBall>().transform;
        }
        else if (other.gameObject.tag == "Player")
        {
            rollSnow = other.GetComponentInChildren<SnowBall>().transform;
        }
        else
            return;

        if (rollSnow.localScale.x > transform.localScale.x)
        {
            isFall = true;
            GetComponent<Collider>().enabled = false;
            sphereCollider.enabled = false;
            //anim.SetBool("Run", false);
            //anim.SetBool("Idle", false);
            //anim.SetTrigger("Death");
            //if (other.TryGetComponent(out JoystickPlayer joystick))
            //{
            //    joystick.speed = 0;
            //}
            //else
            if (TryGetComponent(out AI aI))
            {
                aI.agent.isStopped = true;
            }
            //collectedSnow = 0;
            snowball.transform.localScale = Vector3.zero;
        }
    }
    //public IEnumerator Stand()
    //{
    //isFall = false;
    //if (TryGetComponent(out Movement movement))
    //{
    //    anim.SetBool("Run", false);
    //    anim.SetBool("Idle", true);
    //    movement.speed = 2;
    //}
    //else if (TryGetComponent(out AI aI))
    //{
    //    anim.SetBool("Run", true);
    //    anim.SetBool("Idle", false);
    //    aI.agent.isStopped = false;
    //}
    //scaleMultiplier = 0f;
    //snowball.transform.localScale = Vector3.one * scaleMultiplier;
    //sphereCollider.transform.localPosition = new Vector3(0, .25f, 0);
    //yield return new WaitForSeconds(1f);
    //GetComponent<Collider>().enabled = true;
    //sphereCollider.enabled = true;
    //}
    public IEnumerator MakeBridge(Collider other)
    {
        //if (collectedSnow >= 100)
        //{
        //    snowball.SetActive(true);
        //}
        //else
        //    snowball.SetActive(false);
        //if (collectedSnow < bridgeMultiplier)
        //    collectedSnow = 0;
        //else
        //    collectedSnow -= (int)bridgeMultiplier;
        AIBridgeController bridge = other.GetComponentInParent<AIBridgeController>();
        while (snowball.localScale.x > 0.0f && !AI.Instance.makeBridgeState.IsFinish())
        {
            scaleMultiplier -= .005f * bridgeMultiplier;
            snowball.localPosition = new Vector3(0, snowball.localScale.y * 0.5f, snowball.localScale.z * 0.5f + 0.5f);
            snowball.localScale = Vector3.one * scaleMultiplier;
            bridge.StretchBridge(.01f * bridgeMultiplier);
            yield return new WaitForSeconds(0f);
        }
    }
    public IEnumerator CollectSnow(float requiredSnow)
    {
        //collectedSnow += 1;

        //if (collectedSnow >= 100)
        //{
        //    snowball.SetActive(true);
        //}
        //else
        //    snowball.SetActive(false);
        while (snowball.localScale.x < requiredSnow)
        {
            scaleMultiplier += .001f;
            snowball.localPosition = new Vector3(0, snowball.localScale.y * 0.5f, snowball.localScale.z * 0.5f + 0.5f);
            snowball.localScale = Vector3.one * scaleMultiplier;
            yield return new WaitForSeconds(1f);
        }
    }

    public float GetSnowScale() => snowball.localScale.x;

}
