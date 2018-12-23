using UnityEngine;
using System;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public DebugOptions DebugLocal;
    [Serializable]
    public class DebugOptions
    {
        public bool LogControllers = true;
    }

    [ReadOnly] public bool ControllerConnected = false;
    [ReadOnly] public int ControllersConnected = 0;

    public bool Attack1 {  get { return Input.GetKeyDown(KeyCode.JoystickButton2); } }
    public bool Attack2 {  get { return Input.GetKeyDown(KeyCode.JoystickButton3); } }

    private void Awake()
    {
        DoSingletonCheck();
        CheckControllers();
    }
    private void OnGUI()
    {
        DoSingletonCheck();
    }

    private void CheckControllers()
    {
        var controllers = Input.GetJoystickNames();
        // If there are controllers connected
        if (controllers.Length > 0)
        {
            // Change refrence bool and count.
            ControllerConnected = true;
            ControllersConnected = controllers.Length;

            // Log them in the console if option bool is true.
            if (DebugLocal.LogControllers)
            {
                Debug.Log("Found " + controllers.Length + " controllers:");
                foreach (var controller in controllers) { Debug.Log(controller); }
            }
        }
        // Else log a warning.
        else
        {
            Debug.LogWarning("Did not find any controllers");
        }
        
    }
    private void DoSingletonCheck()
    {
        if (Instance == null) Instance = this;
        else { Debug.LogError("A Game Manager instance already exists. Destroying Game Manager attatched to " + name); Destroy(this); }
    }
}
