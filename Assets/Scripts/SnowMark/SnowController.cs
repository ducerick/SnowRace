using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowController : MonoBehaviour
{
    public Transform Player;        //Public variable to store a reference to the player game object

    private Vector3 _offset;            //Private variable to store the offset distance between the player and camera

    [SerializeField] private GameObject snow;

    // Use this for initialization
    void Start()
    {
        //Calculate and store the offset value by getting the distance between the player's position and camera's position.
        _offset = snow.transform.position - Player.position;
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        SetPosition(Player);
    }

    public static SnowController Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);

    }

    private void Update()
    {

    }

    public void SetPosition(Transform tranform)
    {
        // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
        snow.transform.position = tranform.position + _offset;
    }
}
