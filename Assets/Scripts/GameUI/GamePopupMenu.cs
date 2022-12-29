using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePopupMenu : MonoBehaviour
{
    [SerializeField] private Button ContinueButton;

    public static GamePopupMenu Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
    }
    public void LoadScene(string name)
    {
        Upgrade(name);
    }
    private void Upgrade(string name)
    {
        SceneManager.LoadScene(name);
    }
}
