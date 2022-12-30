using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerController player;
    public Collision playerCollision;
    public JoystickPlayer joystickPlayer;
    public BridgePlayer bridgePlayer;
    public StepBridge stepBridge;
    public SnowBall snowBall;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }


}
