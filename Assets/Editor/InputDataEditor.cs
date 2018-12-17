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
        protected Vector2 _scroll;

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
                    kb_Render_Header();/*
                    kb_Render_LeftHandLetters();
                    kb_Render_RightHandLetters();*/
                    kb_Render_AllLetters();
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
        protected void kb_Render_AllLetters()
        {
            var buttonSize = new Vector2(22, 22);
            var leftSpace = 6f;

            _scroll = GUILayout.BeginScrollView(_scroll, GUILayout.Height(100));
            GUILayout.BeginVertical();
                    GUILayout.Label("Left Hand Letters");

                GUILayout.BeginHorizontal();
                    GUILayout.Space(leftSpace);
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
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                    GUILayout.Space(leftSpace*2);
                    Render_ButtonForKey("A", KeyCode.A, buttonSize);
                    Render_ButtonForKey("S", KeyCode.S, buttonSize);
                    Render_ButtonForKey("D", KeyCode.D, buttonSize);
                    Render_ButtonForKey("F", KeyCode.F, buttonSize);
                    Render_ButtonForKey("G", KeyCode.G, buttonSize);
                    Render_ButtonForKey("H", KeyCode.H, buttonSize);
                    Render_ButtonForKey("J", KeyCode.J, buttonSize);
                    Render_ButtonForKey("K", KeyCode.K, buttonSize);
                    Render_ButtonForKey("L", KeyCode.L, buttonSize);
                GUILayout.EndHorizontal();

                GUILayout.BeginHorizontal();
                    GUILayout.Space(leftSpace*3);
                    Render_ButtonForKey("Z", KeyCode.Z, buttonSize);
                    Render_ButtonForKey("X", KeyCode.X, buttonSize);
                    Render_ButtonForKey("C", KeyCode.C, buttonSize);
                    Render_ButtonForKey("V", KeyCode.V, buttonSize);
                    Render_ButtonForKey("B", KeyCode.B, buttonSize);
                    Render_ButtonForKey("Z", KeyCode.Z, buttonSize);
                    Render_ButtonForKey("M", KeyCode.M, buttonSize);
                GUILayout.EndHorizontal();

            GUILayout.EndVertical();
            GUILayout.EndScrollView();
        }

        protected void Render_ButtonForKey(
            string buttonName,
            KeyCode key)
        {
            if (GUILayout.Button(buttonName)) _inputData.KeyCode = key;
        }
        protected void Render_ButtonForKey(
           string buttonName,
           KeyCode key,
           Vector2 absoluteSize)
        {
            var style = GUI.skin.button;
            style.padding = new RectOffset(1, 1, 1, 1);
            style.alignment = TextAnchor.UpperCenter;

            if (GUILayout.Button(
                buttonName,
                style,
                GUILayout.Height(absoluteSize.y),
                GUILayout.Width(absoluteSize.x)
                ))
                _inputData.KeyCode = key;
        }

        protected enum ElementFlexebility
        {
            rigid, flexibleHeight, flexibleWidth, flexible
        }
    }
}