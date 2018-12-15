using System;
using System.Collections;
using UnityEngine;

namespace Sierra.Unity2D.TopDown
{
    [RequireComponent(typeof(MotionController3D))]
    public class PlayerController3D : MonoBehaviour
    {
        [ReadOnly]
        public bool Attacking;
        [ReadOnly]
        public Vector2 MoveInput;

        protected MotionController3D c_MotionController;

        protected virtual void Awake()
        {
            c_MotionController = GetComponent<MotionController3D>();
        }
        protected virtual void FixedUpdate()
        {
            // Get input
            GetDirectionalInput();

            // Call movement method
            c_MotionController.ResetVelocity();
            c_MotionController.MoveInDirection(MoveInput);
        }
        protected void Update()
        {
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
            if (!Attacking)
            {

            }
            else
            {

            }
        }

        private IEnumerator IE_Attack(AttackData data)
        {
            Attacking = true;
            yield return new WaitForSeconds(Utility.FramesToSeconds(data.Startup));
            
            data.Hitbox.ActivateHitBox(data);
            yield return new WaitForSeconds(Utility.FramesToSeconds(data.Active));

            data.Hitbox.DeactivateHitBox();
            yield return new WaitForSeconds(Utility.FramesToSeconds(data.Recovery));

            Attacking = false;
        }
        #endregion
    }
    [Serializable]
    public class AttackData
    {
        public Hitbox Hitbox;
        public float Damage = 1;
        public float KnockUp = 0;
        public float KnockBack = 0;
        public int Startup = 6;
        public int Active = 1;
        public int Recovery = 8;
        public int Hitstun = 20;
        public int Hitstop = 3;
    }
}