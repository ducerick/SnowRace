using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    [SerializeField] private AIAnimator aiAnimator;
    [SerializeField] private AIController aiController;
    [SerializeField] private AISnowBall aiSnowBall;
    [SerializeField] private AICollision aiCollision;
    [SerializeField] private Plane plane;


    public static AIManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
