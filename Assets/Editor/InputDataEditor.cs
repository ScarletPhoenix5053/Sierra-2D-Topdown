using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace Sierra.Unity2D.InputManagement
{
    [CustomEditor(typeof(InputData))]
    public class InputDataEditor : Editor
    {
        
        protected bool kb_checkForKeyOnPress = false;
        protected bool _initialMouseClick = true;
        protected Device _device = Device.KeyboardAndMouse;
        protected Vector2 _scroll;
        protected InputData _inputData;

        protected int kb_inputWindowFrames = 5;
        protected int kb_timer_inputWindow = 0;
        protected KeyType kb_keyType = KeyType.None;
        protected KeyCode kb_keyCode = KeyCode.None;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            _inputData = (InputData)target;

            Render_Selection_Device();
            Fork_ByDevice();
        }

        /// <summary>
        /// Waits for the user to press a key, and sets <see cref="_inputData"/>'s <see cref="KeyCode"/> to match.
        /// Will only work for keyboard.
        /// </summary>
        protected void kb_GetKeyOnPress()
        {
            // Initialize
            var foundKey = false;

            // Check for input
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                // Ignore the following:
                if (Event.current.keyCode == KeyCode.None) continue;

                // Store Keycode on match
                if (Event.current.keyCode == keyCode)
                {
                    _inputData.KeyCode = keyCode;
                    Log_SettingKeyCodeTo(keyCode.ToString());
                    if (!foundKey) foundKey = true;
                }
            }

            // Keep checking untill a key is found
            if (!foundKey) return;

            // Reset the method
            kb_checkForKeyOnPress = false;
            _initialMouseClick = true;
        }

        protected void Fork_ByDevice()
        {
            switch (_device)
            {
                case Device.Controller:
                    break;
                case Device.KeyboardAndMouse:
                    kb_Render_PressAnyKey();
                    if (kb_checkForKeyOnPress) kb_GetKeyOnPress();
                    kb_Render_Header();
                    kb_Render_FullKeyboard();
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
        protected void kb_Render_PressAnyKey()
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Click here to set input");
            if (GUILayout.Button("")) kb_checkForKeyOnPress = true;
            GUILayout.EndHorizontal();
        }
        /// <summary>
        /// Renders a button, and sets <see cref="_inputData"/>'s <see cref="KeyCode"/>
        /// to the provided keycode on button press.
        /// </summary>
        /// <param name="buttonName">Name to display on the button</param>
        /// <param name="key">The <see cref="KeyCode"/> to set <see cref="_inputData"/> to</param>
        protected void Render_ButtonForKey(string buttonName, KeyCode key)
        {
            if (GUILayout.Button(buttonName))
            {
                Log_SettingKeyCodeTo(key.ToString());
                _inputData.KeyCode = key;
            }
        }
        /// <summary>
        /// Renders a button at a fixed size, and sets <see cref="_inputData"/>'s <see cref="KeyCode"/>
        /// to the provided keycode on button press.
        /// </summary>
        /// <param name="buttonName">Name to display on the button</param>
        /// <param name="key">The <see cref="KeyCode"/> to set <see cref="_inputData"/> to</param>
        /// <param name="absoluteSize">Size of the button</param>
        protected void Render_ButtonForKey(string buttonName, KeyCode key, Vector2 absoluteSize)
        {
            // Initialize
            var style = GUI.skin.button;
            style.padding = new RectOffset(1, 1, 1, 1);
            style.alignment = TextAnchor.UpperCenter;

            // Render and check button
            if (GUILayout.Button(
                buttonName,
                style,
                GUILayout.Height(absoluteSize.y),
                GUILayout.Width(absoluteSize.x)
                ))
            {
                // Update KeyCode on button press
                Log_SettingKeyCodeTo(key.ToString());
                _inputData.KeyCode = key;
            }
        }
        protected void Log_SettingKeyCodeTo(string keyCode)
        {
            Debug.Log("Setting KeyCode to: " + keyCode);
        }

        protected void kb_Render_Header()
        {
            GUILayout.Label("Checking keyboard and mouse controls");
        }
        protected void kb_Render_LeftHandLetters()
        {
            var buttonSize = new Vector2(22, 22);
            var leftSpace = 6f;

            GUILayout.BeginVertical();
                    GUILayout.Label("Left Hand Letters");

                GUILayout.BeginHorizontal();
                    GUILayout.Space(leftSpace);
                    Render_ButtonForKey("Q", KeyCode.Q, buttonSize);
                    Render_ButtonForKey("W", KeyCode.W, buttonSize);
                    Render_ButtonForKey("E", KeyCode.E, buttonSize);
                    Render_ButtonForKey("R", KeyCode.R, buttonSize);
                    Render_ButtonForKey("T", KeyCode.T, buttonSize);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                    GUILayout.Space(leftSpace*2);
                    Render_ButtonForKey("A", KeyCode.A, buttonSize);
                    Render_ButtonForKey("S", KeyCode.S, buttonSize);
                    Render_ButtonForKey("D", KeyCode.D, buttonSize);
                    Render_ButtonForKey("F", KeyCode.F, buttonSize);
                    Render_ButtonForKey("G", KeyCode.G, buttonSize);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                    GUILayout.Space(leftSpace*3);
                    Render_ButtonForKey("Z", KeyCode.Z, buttonSize);
                    Render_ButtonForKey("X", KeyCode.X, buttonSize);
                    Render_ButtonForKey("C", KeyCode.C, buttonSize);
                    Render_ButtonForKey("V", KeyCode.V, buttonSize);
                    Render_ButtonForKey("B", KeyCode.B, buttonSize);
                GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
        protected void kb_Render_RightHandLetters()
        {
            var buttonSize = new Vector2(22, 22);
            var leftSpace = 6f;

            GUILayout.BeginVertical();
                GUILayout.Label("Right Hand Letters");

                GUILayout.BeginHorizontal();
                    GUILayout.Space(leftSpace);
                    Render_ButtonForKey("Y", KeyCode.Y, buttonSize);
                    Render_ButtonForKey("U", KeyCode.U, buttonSize);
                    Render_ButtonForKey("I", KeyCode.I, buttonSize);
                    Render_ButtonForKey("O", KeyCode.O, buttonSize);
                    Render_ButtonForKey("P", KeyCode.P, buttonSize);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                    GUILayout.Space(leftSpace * 2);
                    Render_ButtonForKey("H", KeyCode.H, buttonSize);
                    Render_ButtonForKey("J", KeyCode.J, buttonSize);
                    Render_ButtonForKey("K", KeyCode.K, buttonSize);
                    Render_ButtonForKey("L", KeyCode.L, buttonSize);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                    GUILayout.Space(leftSpace * 3);
                    Render_ButtonForKey("Z", KeyCode.Z, buttonSize);
                    Render_ButtonForKey("M", KeyCode.M, buttonSize);
                GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }
        protected void kb_Render_FullKeyboard()
        {
            var leftSpace = 6f;
            var buttonSize = new Vector2(22, 22);
            var commandButtonSizeX = buttonSize.x * 1.5f;
            var tabButtonSize = new Vector2(commandButtonSizeX + leftSpace, buttonSize.y);
            var capsButtonSize = new Vector2(commandButtonSizeX + leftSpace*2, buttonSize.y);
            var shiftButtonSize = new Vector2(commandButtonSizeX + 1+leftSpace*3, buttonSize.y);
            var enterButtonSize = new Vector2(commandButtonSizeX*2- leftSpace-1, buttonSize.y);
            var tildeButtonSize = new Vector2(buttonSize.x/2, buttonSize.y);
            var backSpaceButtonSize = new Vector2(1+commandButtonSizeX*2, buttonSize.y);
            var windowsButtonSize = new Vector2(commandButtonSizeX+3, buttonSize.y);
            var spaceBarSize = new Vector2(120, buttonSize.y);


            _scroll = GUILayout.BeginScrollView(_scroll, GUILayout.Height(165));
            GUILayout.BeginVertical();
                    GUILayout.Label("Full Keyboard");
            /*
                // KEYBOARD FUNCTION ROW
                GUILayout.BeginHorizontal();
                    Render_ButtonForKey("esc", KeyCode.Escape, buttonSize);
                    Render_ButtonForKey("F1", KeyCode.F1, buttonSize);
                    Render_ButtonForKey("F2", KeyCode.F2, buttonSize);
                    Render_ButtonForKey("F3", KeyCode.F3, buttonSize);
                    Render_ButtonForKey("F4", KeyCode.F4, buttonSize);
                    Render_ButtonForKey("F5", KeyCode.F5, buttonSize);
                    Render_ButtonForKey("F6", KeyCode.F6, buttonSize);
                    Render_ButtonForKey("F7", KeyCode.F7, buttonSize);
                    Render_ButtonForKey("F8", KeyCode.F8, buttonSize);
                    Render_ButtonForKey("F9", KeyCode.F9, buttonSize);
                    Render_ButtonForKey("F10", KeyCode.F10, buttonSize);
                    Render_ButtonForKey("F11", KeyCode.F11, buttonSize);
                    Render_ButtonForKey("F12", KeyCode.F12, buttonSize);
                    Render_ButtonForKey("ins", KeyCode.Insert, buttonSize);
                    Render_ButtonForKey("del", KeyCode.Delete, buttonSize);
                GUILayout.EndHorizontal();*/

                // KEYBOARD NUM ROW
                GUILayout.BeginHorizontal();
                    Render_ButtonForKey("`", KeyCode.BackQuote, tildeButtonSize);
                    Render_ButtonForKey("1", KeyCode.Alpha1, buttonSize);
                    Render_ButtonForKey("2", KeyCode.Alpha2, buttonSize);
                    Render_ButtonForKey("3", KeyCode.Alpha3, buttonSize);
                    Render_ButtonForKey("4", KeyCode.Alpha4, buttonSize);
                    Render_ButtonForKey("5", KeyCode.Alpha5, buttonSize);
                    Render_ButtonForKey("6", KeyCode.Alpha6, buttonSize);
                    Render_ButtonForKey("7", KeyCode.Alpha7, buttonSize);
                    Render_ButtonForKey("8", KeyCode.Alpha8, buttonSize);
                    Render_ButtonForKey("9", KeyCode.Alpha9, buttonSize);
                    Render_ButtonForKey("0", KeyCode.Alpha0, buttonSize);
                    Render_ButtonForKey("-", KeyCode.Minus, buttonSize);
                    Render_ButtonForKey("-", KeyCode.Equals, buttonSize);
                    Render_ButtonForKey("backspace", KeyCode.Backspace, backSpaceButtonSize);
                GUILayout.EndHorizontal();                

                // KEYBOARD ROW 1
                GUILayout.BeginHorizontal();
                    //GUILayout.Space(leftSpace);
                    Render_ButtonForKey("tab", KeyCode.Tab, tabButtonSize);
                    Render_ButtonForKey("Q", KeyCode.Q, buttonSize);
                    Render_ButtonForKey("W", KeyCode.W, buttonSize);
                    Render_ButtonForKey("E", KeyCode.E, buttonSize);
                    Render_ButtonForKey("R", KeyCode.R, buttonSize);
                    Render_ButtonForKey("T", KeyCode.T, buttonSize);
                    Render_ButtonForKey("Y", KeyCode.Y, buttonSize);
                    Render_ButtonForKey("U", KeyCode.U, buttonSize);
                    Render_ButtonForKey("I", KeyCode.I, buttonSize);
                    Render_ButtonForKey("O", KeyCode.O, buttonSize);
                    Render_ButtonForKey("P", KeyCode.P, buttonSize);
                    Render_ButtonForKey("[", KeyCode.LeftBracket, buttonSize);
                    Render_ButtonForKey("]", KeyCode.RightBracket, buttonSize);
                    Render_ButtonForKey("\\", KeyCode.Backslash, tabButtonSize);
                GUILayout.EndHorizontal();

                // KEYBOARD ROW 2
                GUILayout.BeginHorizontal();
                    //GUILayout.Space(leftSpace*2);
                    Render_ButtonForKey("caps", KeyCode.CapsLock, capsButtonSize);
                    Render_ButtonForKey("A", KeyCode.A, buttonSize);
                    Render_ButtonForKey("S", KeyCode.S, buttonSize);
                    Render_ButtonForKey("D", KeyCode.D, buttonSize);
                    Render_ButtonForKey("F", KeyCode.F, buttonSize);
                    Render_ButtonForKey("G", KeyCode.G, buttonSize);
                    Render_ButtonForKey("H", KeyCode.H, buttonSize);
                    Render_ButtonForKey("J", KeyCode.J, buttonSize);
                    Render_ButtonForKey("K", KeyCode.K, buttonSize);
                    Render_ButtonForKey("L", KeyCode.L, buttonSize);
                    Render_ButtonForKey(";", KeyCode.Semicolon, buttonSize);
                    Render_ButtonForKey("\'", KeyCode.Quote, buttonSize);
                    Render_ButtonForKey("enter", KeyCode.Return, enterButtonSize);
                GUILayout.EndHorizontal();

                // KEYBOARD ROW 3
                GUILayout.BeginHorizontal();
                    //GUILayout.Space(leftSpace*3);
                    Render_ButtonForKey("shift", KeyCode.LeftShift, shiftButtonSize);
                    Render_ButtonForKey("Z", KeyCode.Z, buttonSize);
                    Render_ButtonForKey("X", KeyCode.X, buttonSize);
                    Render_ButtonForKey("C", KeyCode.C, buttonSize);
                    Render_ButtonForKey("V", KeyCode.V, buttonSize);
                    Render_ButtonForKey("B", KeyCode.B, buttonSize);
                    Render_ButtonForKey("Z", KeyCode.Z, buttonSize);
                    Render_ButtonForKey("M", KeyCode.M, buttonSize);
                    Render_ButtonForKey(",", KeyCode.Comma, buttonSize);
                    Render_ButtonForKey(".", KeyCode.Period, buttonSize);
                    Render_ButtonForKey("/", KeyCode.Slash, buttonSize);
                    Render_ButtonForKey("/\\", KeyCode.UpArrow, buttonSize);
                    Render_ButtonForKey("shift", KeyCode.RightShift, shiftButtonSize);
                GUILayout.EndHorizontal();


            // KEYBOARD SPACEBAR ROW
                GUILayout.BeginHorizontal();
                //GUILayout.Space(leftSpace*3);
                    Render_ButtonForKey("ctrl", KeyCode.LeftControl, buttonSize);
                    Render_ButtonForKey("fn", KeyCode.None, buttonSize);                    
                    Render_ButtonForKey("start", KeyCode.LeftWindows, windowsButtonSize);                    
                    Render_ButtonForKey("alt", KeyCode.LeftAlt, buttonSize);
                    Render_ButtonForKey("", KeyCode.Space, spaceBarSize);                    
                    Render_ButtonForKey("alt", KeyCode.RightAlt, buttonSize);
                    Render_ButtonForKey("ctrl", KeyCode.RightControl, buttonSize);
                    Render_ButtonForKey("<", KeyCode.LeftArrow, buttonSize);
                    Render_ButtonForKey("\\/", KeyCode.DownArrow, buttonSize);
                    Render_ButtonForKey(">", KeyCode.RightArrow, buttonSize);
                    Render_ButtonForKey("fn", KeyCode.None, buttonSize);
                GUILayout.EndHorizontal();

            GUILayout.EndVertical();
            GUILayout.EndScrollView();
        }
    }
}