using UnityEngine;
using System;

namespace Sierra.Unity2D.TopDown
{
    [RequireComponent(typeof(CharacterController))]
    public class MotionController3D : MonoBehaviour
    {
        [HideInInspector]
        public CharacterController CharControl;
        public BaseMotionVars Motion = new BaseMotionVars();

        protected float _xVel = 0f;
        protected float _yVel = 0f;
        protected const float _zeroThreshold = 0.1f;

        public virtual bool IsGrounded
        {
            get { return CharControl.isGrounded; }
        }

        [Serializable]
        public class BaseMotionVars
        {
            public float Speed = 1f;
            public float Acceleration = 0.2f;
            public float DragX = 0.05f;
            public float DragY = 0.05f;
        }

        protected virtual void Awake()
        {
            CharControl = GetComponent<CharacterController>();
        }
        protected virtual void FixedUpdate()
        {
            ApplyMovement();
        }


        /// <summary>
        /// Immediatley set velocities to this force
        /// </summary>
        /// <remarks>
        /// Ignores MaxSpeed
        /// </remarks>
        /// <param name="force"></param>
        public void SetVelocity(Vector2 force)
        {
            _xVel = force.x;
            _yVel = force.y;
        }
        /// <summary>
        /// Zero out all forces
        /// </summary>
        public void ResetVelocity()
        {
            _xVel = 0;
            _yVel = 0;
        }
        /// <summary>
        /// Move in a direction, based on the motion controller's speed variables
        /// </summary>
        /// <param name="dir">Directional Vector. Will be normalized.</param>
        public void MoveInDirection(Vector2 dir)
        {
            dir = dir.normalized;
            _xVel = dir.x * Motion.Speed;
            _yVel = dir.y * Motion.Speed;
        }

        protected void ApplyMovement()
        {
            CharControl.Move(new Vector3(_xVel, _yVel, 0));
        }
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
            // apply Z drag
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
        }

    }
}