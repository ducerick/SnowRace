using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] private Rigidbody myRigidBody;
    [SerializeField] private bool isPlaying;
    

    public bool OnPlane { get; set; }

  
    private void Start()
    {
        OnPlane = true;
        myRigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }


}
