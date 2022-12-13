using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Playing,
    Failed,
    Stop,
    Success,
}

public class GameState : MonoBehaviour
{
    public static GameState Instance;
    public State GState { get; set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GState = State.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
