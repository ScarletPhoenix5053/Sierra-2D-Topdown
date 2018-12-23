using UnityEngine;
using System.Collections;
using Sierra;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public InputManager InputManager;

    private void Awake()
    {
        DoSingletonCheck();

        InputManager = GetComponent<InputManager>();

        if (InputManager == null)
        {
            Utility.ThrowNoComponentException(name);
        }
    }
    private void Update()
    {
    }

    private void DoSingletonCheck()
    {
        if (Instance == null) Instance = this;
        else { Debug.LogError("A Game Manager instance already exists. Destroying Game Manager attatched to " + name); Destroy(this); }
    }
}
