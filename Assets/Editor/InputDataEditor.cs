using UnityEditor;
using UnityEngine;

namespace Sierra.Unity2D.InputManagement
{
    [CustomEditor(typeof(InputData))]
    public class InputDataEditor : Editor
    {
        protected InputData _inputData;
        protected Device _device = Device.KeyboardAndMouse;
        protected KeyType kb_keyType = KeyType.None;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            _inputData = (InputData)target;

            Render_Selection_Device();
            Fork_ByDevice();
        }


        protected void Fork_ByDevice()
        {
            switch (_device)
            {
                case Device.Controller:
                    break;
                case Device.KeyboardAndMouse:
                    kb_Render_Header();
                    kb_Render_Selection_KeyType();
                    kb_Fork_ByKey();
                    break;
                default:
                    break;
            }
        }
        protected void Render_Selection_Device()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Device Type");
            _device = (Device)EditorGUILayout.EnumPopup(_device);
            GUILayout.EndHorizontal();
        }

        protected void kb_Fork_ByKey()
        {
            switch (kb_keyType)
            {
                case KeyType.None:
                    kb_Render_Key_None();
                    break;
                case KeyType.Number:
                    kb_Render_Key_Number();
                    break;
                case KeyType.Letter:
                    kb_Render_Key_Letter();
                    break;
                case KeyType.function:
                    kb_Render_Key_Function();
                    break;
                case KeyType.SpecialCharacter:
                    kb_Render_Key_SpecialCharacter();
                    break;
                case KeyType.Command:
                    kb_Render_Key_Command();
                    break;
                default:
                    break;
            }
        }
        protected void kb_Render_Selection_KeyType()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Key Type");
            kb_keyType = (KeyType)EditorGUILayout.EnumPopup(kb_keyType);
            GUILayout.EndHorizontal();
        }
        protected void kb_Render_Header()
        {
            GUILayout.Label("Checking keyboard and mouse controls");
        }
        protected void kb_Render_Key_Number()
        {
            GUILayout.Label("Please select a Key Type");
        }
        protected void kb_Render_Key_Letter()
        {
            GUILayout.Label("Please select a Key Type");
        }
        protected void kb_Render_Key_SpecialCharacter()
        {
            GUILayout.Label("Please select a Key Type");
        }
        protected void kb_Render_Key_Command()
        {
            GUILayout.Label("Please select a Key Type");
        }
        protected void kb_Render_Key_Function()
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            Render_ButtonForKey("F1", KeyCode.F1);
            Render_ButtonForKey("F2", KeyCode.F2);
            Render_ButtonForKey("F3", KeyCode.F3);
            Render_ButtonForKey("F5", KeyCode.F4);
            Render_ButtonForKey("F5", KeyCode.F5);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            Render_ButtonForKey("F6", KeyCode.F6);
            Render_ButtonForKey("F7", KeyCode.F7);
            Render_ButtonForKey("F8", KeyCode.F8);
            Render_ButtonForKey("F9", KeyCode.F9);
            Render_ButtonForKey("F10", KeyCode.F10);
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            Render_ButtonForKey("F10", KeyCode.F10);
            Render_ButtonForKey("F11", KeyCode.F11);
            Render_ButtonForKey("F12", KeyCode.F12);
            Render_ButtonForKey("F13", KeyCode.F13);
            Render_ButtonForKey("F14", KeyCode.F14);
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
        protected void kb_Render_Key_None()
        {
            GUILayout.Label("Please select a Key Type");
        }

        protected void Render_ButtonForKey(string buttonName, KeyCode key)
        {
            if (GUILayout.Button(buttonName,GUILayout.MaxWidth(30f))) _inputData.KeyCode = key;
        }
    }
}