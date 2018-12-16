using System;
using System.Collections;
using UnityEngine;

namespace Sierra.Unity2D.TopDown
{
    [RequireComponent(typeof(AttackManager))]
    [RequireComponent(typeof(MotionController3D))]
    public class PlayerController3D : MonoBehaviour
    {
        [ReadOnly]
        public Vector2 MoveInput;

        protected MotionController3D c_MotionController;
        protected AttackManager c_AttackManager;

        protected virtual void Awake()
        {
            c_MotionController = GetComponent<MotionController3D>();
            c_AttackManager = GetComponent<AttackManager>();

            if (c_AttackManager == null ||
                c_MotionController == null)
            {
                Utility.ThrowNoComponentException(name);
            }
        }
        protected virtual void FixedUpdate()
        {
            // Call movement method
            c_MotionController.ResetVelocity();
            c_MotionController.MoveInDirection(MoveInput);
        }
        protected void Update()
        {
            // Get input
            GetDirectionalInput();
            GetActionInput();
        }

        #region Private Methods
        /// <summary>
        /// Set <see cref="MoveInput"/> based on either keyboard or left stick controller input. Currently defaults to controller exclusivle input.
        /// </summary>
        private void GetDirectionalInput()
        {
            if (GameManager.Instance.InputManager.ControllerConnected)
            {
                MoveInput = GetControllerLeftStickAxis();
            }
            else
            {
                MoveInput = GetWasdAxis();
            }

        }
        private void GetActionInput()
        {
            if (GameManager.Instance.InputManager.Attack1)
            {
                c_AttackManager.Attack();
            }
        }
        private Vector2 GetWasdAxis()
        {
            var w = Input.GetKey(KeyCode.W);
            var a = Input.GetKey(KeyCode.A);
            var s = Input.GetKey(KeyCode.S);
            var d = Input.GetKey(KeyCode.D);
            var moveInput = Vector2.zero;

            if (!(a && d))
            {
                if (a) moveInput.x = -1;
                else if (d) moveInput.x = 1;
            }
            if (!(w && s))
            {
                if (w) moveInput.y = 1;
                else if (s) moveInput.y = -1;
            }

            return moveInput;
        }
        /// <summary>
        /// Returns a normalized Vector2 based on controller left stick inputs.
        /// </summary>
        /// <returns></returns>
        private Vector2 GetControllerLeftStickAxis()
        {
            var x = Input.GetAxis("ControllerHorizontal");
            var y = Input.GetAxis("ControllerVertical");
            var moveInput = new Vector2(x, y).normalized;

            return moveInput;
        }

        #endregion
    }
}