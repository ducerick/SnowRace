using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollSnow : MonoBehaviour
{
    public bool isFall;
    //[SerializeField] bool isAI;
    [SerializeField] SphereCollider sphereCollider;
    
    Animator anim;
    [SerializeField] LayerMask layer;
    float bridgeMultiplier = 5;
    //MyJoystick _joy;
    public int collectedSnow = 0;
    [SerializeField] float scaleMultiplier = .5f;
    [SerializeField] GameObject snowball;
    RaycastHit hit;
    private void Start()
    {
        sphereCollider = GetComponentInChildren<SphereCollider>();
        anim = GetComponent<Animator>();
        //_joy = MyJoystick.instance;
    }
    private void Update()
    {
        //if (GameManager.instance. isFinish)
        //    return;
        Physics.Raycast(transform.position + Vector3.up,-transform.up, out hit, 100,layer);
        if(hit.collider == null)
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
        if(hit.collider.tag == "Plane" && anim.GetBool("Run"))
        {
            CollectSnow();
        }
        if(hit.collider.tag == "Bridge" && collectedSnow > 0)
        {
            if(!hit.collider.GetComponent<Bridge>().isFinish)
            {
                hit.collider.GetComponent<Bridge>().StretchBridge((.001f * bridgeMultiplier));
                MakeBridge(hit.collider.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Fall(other);
    }
    public void Fall(Collider other)
    {
        RollSnow rollSnow;
        if (other.gameObject.tag == "Snowball")
        {
            rollSnow = other.GetComponentInParent<RollSnow>();
        }
        else if (other.gameObject.tag == "Player")
        {
            rollSnow = other.GetComponent<RollSnow>();
        }
        else
            return;
        
        if (rollSnow.collectedSnow > collectedSnow)
        {
            isFall = true;
            GetComponent<Collider>().enabled = false;
            sphereCollider.enabled = false;
            anim.SetBool("Run", false);
            anim.SetBool("Idle", false);
            anim.SetTrigger("Death");
            if (TryGetComponent(out Movement movement))
            {
                movement.speed = 0;
            }
            else if (TryGetComponent(out AI aI))
            {
                aI.agent.isStopped = true;
            }
            collectedSnow = 0;
            snowball.transform.localScale = Vector3.zero;
        }
    }
    public IEnumerator Stand()
    {
        isFall = false;
        if (TryGetComponent(out Movement movement))
        {
            anim.SetBool("Run", false);
            anim.SetBool("Idle", true);
            movement.speed = 2;
        }
        else if (TryGetComponent(out AI aI))
        {
            anim.SetBool("Run", true);
            anim.SetBool("Idle", false);
            aI.agent.isStopped = false;
        }
        scaleMultiplier = .5f;
        snowball.transform.localScale = Vector3.one * scaleMultiplier;
        sphereCollider.transform.localPosition = new Vector3(0, .25f, 0);
        yield return new WaitForSeconds(1f);
        GetComponent<Collider>().enabled = true;
        sphereCollider.enabled = true;
    }
    private void MakeBridge(GameObject obj)
    {
        if (collectedSnow >= 100)
        {
            snowball.SetActive(true);
        }
        else
            snowball.SetActive(false);
        if (collectedSnow < bridgeMultiplier)
            collectedSnow = 0;
        else
            collectedSnow -= (int) bridgeMultiplier;
        scaleMultiplier -= .001f * bridgeMultiplier;
        snowball.transform.Translate((transform.forward * (.001f * -bridgeMultiplier) / 4),Space.World);
        snowball.transform.localScale = Vector3.one * scaleMultiplier;
    }
    private void CollectSnow()
    {
        collectedSnow += 1;
        scaleMultiplier += .001f;
        snowball.transform.Translate((transform.forward * (.001f) / 4 ),Space.World);
        if (collectedSnow >= 100)
        {
            snowball.SetActive(true);
        }
        else
            snowball.SetActive(false);
        snowball.transform.localScale = Vector3.one * scaleMultiplier;
    }
}
