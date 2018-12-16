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
        private void GetDirectionalInput()
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
            MoveInput = moveInput;
        }
        private void GetActionInput()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                c_AttackManager.Attack();
            }
        }

        #endregion
    }
}