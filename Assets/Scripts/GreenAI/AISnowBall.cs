using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISnowBall : MonoBehaviour
{
    public bool isFall;
    [SerializeField] SphereCollider sphereCollider;

    Animator anim;
    [SerializeField] LayerMask layer;
    float bridgeMultiplier = 5;

    public int collectedSnow = 0;
    [SerializeField] float scaleMultiplier = 0f;
    [SerializeField] Transform snowball;
    RaycastHit hit;
    private void Start()
    {
        sphereCollider = GetComponentInChildren<SphereCollider>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        snowball.Rotate(new Vector3(1, 0, 0), 10);
        //if (GameManager.instance. isFinish)
        //    return;
        Physics.Raycast(transform.position + transform.forward / 3 + Vector3.up, -transform.up, out hit, 100, layer);
        Debug.DrawRay(transform.position  + transform.forward / 3 + transform.transform.up, -transform.up * 5);
        if (hit.collider == null)
        {
            return;
        }
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
        if (hit.collider.tag == "Road" && collectedSnow > 0)
        {
            transform.position = new Vector3(hit.collider.transform.position.x, transform.position.y, transform.position.z);
            if (!hit.collider.GetComponent<AIBridgeController>().isFinish)
            {
                hit.collider.GetComponent<AIBridgeController>().StretchBridge((.005f * bridgeMultiplier));
                MakeBridge(hit.collider.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
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
    private void MakeBridge(GameObject obj)
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
        scaleMultiplier -= .005f * bridgeMultiplier;
        snowball.localPosition = new Vector3(0, snowball.localScale.y * 0.5f, snowball.localScale.z * 0.5f + 0.5f);
        snowball.localScale = Vector3.one * scaleMultiplier;
    }
    public void CollectSnow()
    {
        //collectedSnow += 1;
        scaleMultiplier += .005f;
        snowball.localPosition = new Vector3(0, snowball.localScale.y * 0.5f, snowball.localScale.z * 0.5f + 0.5f);
        //if (collectedSnow >= 100)
        //{
        //    snowball.SetActive(true);
        //}
        //else
        //    snowball.SetActive(false);
        snowball.localScale = Vector3.one * scaleMultiplier;
    }

    public float GetSnowScale() => snowball.localScale.x;
}
