using UnityEngine;

namespace Sierra.Unity2D.InputManagement
{
    [CreateAssetMenu(fileName = "New Input", menuName = "Sierra/Input")]
    public class InputData : ScriptableObject
    {
        public string Name = "New Input";
        public KeyCode KeyCode = KeyCode.None;

        public bool CheckKey()
        {
            if (Input.GetKey(KeyCode)) return true; else return false;
        }
        public bool CheckKeyDown()
        {
            if (Input.GetKeyDown(KeyCode)) return true; else return false;
        }
        public bool CheckKeyUp()
        {
            if (Input.GetKeyUp(KeyCode)) return true; else return false;
        }
    }
    public enum Device
    {
        Controller, KeyboardAndMouse
    }
    public enum KeyType
    {
        None, Number, Letter, function, SpecialCharacter, Command
    }
}