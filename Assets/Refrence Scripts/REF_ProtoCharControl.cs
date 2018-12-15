using UnityEngine;
using System;

namespace Reference
{
    [RequireComponent(typeof(CharacterController))]
    public class ProtoCharControl : MonoBehaviour
    {
        public BaseMotionVars Motion = new BaseMotionVars();
        public GravityVars Gravity = new GravityVars();

        protected float _xVel = 0f;
        protected float _yVel = 0f;
        protected float _zVel = 0f;
        protected float _zeroThreshold = 0.1f;
        protected Rigidbody _rb;
        protected Collider _collider;
        protected CharacterController _charController;

        public virtual bool IsGrounded
        {
            get
            {
                // return var
                var isGrounded = false;

                isGrounded = _charController.isGrounded;

                return isGrounded;
            }
        }

        [Serializable]
        public class BaseMotionVars
        {
            public float Speed = 1f;
            public float Acceleration = 0.2f;
            public float DragX = 0.05f;
            public float DragY = 0.00f;
            public float DragZ = 0.05f;
        }
        [Serializable]
        public class GravityVars
        {
            public bool UsesGravity = false;
            public float GravityMax = 4f;
            public float GravityAcceleration = 0.1f;
            public LayerMask Ground;
        }

        protected virtual void Awake()
        {
            _charController = GetComponent<CharacterController>();
        }
        protected virtual void FixedUpdate()
        {
            if (Gravity.UsesGravity) ApplyGravity();
            ApplyMovement();
        }


        /// <summary>
        /// Immediatley set velocities to this force
        /// </summary>
        /// <remarks>
        /// Ignores MaxSpeed
        /// </remarks>
        /// <param name="force"></param>
        public void UpdateVelocity(Vector3 force)
        {
            _xVel = force.x;
            _yVel = force.y;
            _zVel = force.z;
        }
        public void ResetVelocity()
        {
            _xVel = 0;
            _yVel = 0;
            _zVel = 0;
        }

        /// <summary>
        /// Move the charater
        /// </summary>
        protected void ApplyMovement()
        {
            _charController.Move(new Vector3(_xVel, _yVel, _zVel));
        }
        /// <summary>
        /// Check if the player is grounded, and apply gravity if they aren't
        /// </summary>
        protected void ApplyGravity()
        {
            // if not on ground
            if (!IsGrounded)
            {
                // add negative velocity
                if (_yVel >= -Gravity.GravityMax) _yVel -= Gravity.GravityAcceleration;
            }
            else
            {
                if (_yVel < 0) _yVel = 0;
            }

        }
        /// <summary>
        /// Apply drag to the character's Y axis.
        /// </summary>
        protected void ApplyDrag()
        {
            // apply X drag
            if (Motion.DragX != 0)
            {
                if (_xVel != 0)
                {
                    if ((_xVel < _zeroThreshold && _xVel > 0) ||
                        (_xVel > -_zeroThreshold && _xVel < 0))
                    {
                        _xVel = 0;
                    }
                    else
                    {
                        _xVel -= Motion.DragX * Math.Sign(_xVel);
                    }
                }
            }

            // apply Y drag
            if (Motion.DragY != 0)
            {
                if (_yVel != 0)
                {
                    if ((_yVel < _zeroThreshold && _yVel > 0) ||
                        (_yVel > -_zeroThreshold && _yVel < 0))
                    {
                        _yVel = 0;
                    }
                    else
                    {
                        _yVel -= Motion.DragY * Math.Sign(_yVel);
                    }
                }
            }

            // apply Z drag
            if (Motion.DragZ != 0)
            {
                if (_zVel != 0)
                {
                    if ((_zVel < _zeroThreshold && _zVel > 0) ||
                        (_zVel > -_zeroThreshold && _zVel < 0))
                    {
                        _zVel = 0;
                    }
                    else
                    {
                        _zVel -= Motion.DragZ * Math.Sign(_zVel);
                    }
                }
            }
        }

    }

}