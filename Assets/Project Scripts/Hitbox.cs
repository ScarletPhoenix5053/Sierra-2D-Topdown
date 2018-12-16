using UnityEngine;
using System.Collections.Generic;

namespace Sierra.Unity2D.TopDown
{
    /// <summary>
    /// When active, checks for collisions with colliders attached to a Health component and sends a damage command to it if it finds one.
    /// </summary>
    public class Hitbox : MonoBehaviour
    {
        public LayerMask LayerMask;
        public bool UseAnimation;

        protected Collider c_collider;
        protected List<Health> _checked;
        protected AttackData _data;
        
        protected void Awake()
        {
            c_collider = GetComponent<Collider>();

            // Check all essential refrences are set
            if (
                c_collider == null)
            {
                Utility.ThrowNoComponentException(name);
            }

            // Ensure collider is not active when instance is spawned.
            c_collider.enabled = false;
        }
        protected void Update()
        {
            ResetColliderWhenInactive();
        }
        protected void OnTriggerStay(Collider other)
        {
            CheckForTarget(other);
        }

        public void ActivateHitBox(AttackData data)     
        {
            c_collider.enabled = true;
            _data = data;
        }
        public void DeactivateHitBox()
        {
            c_collider.enabled = false;
            _data = null;
        }

        protected void CheckForTarget(Collider other)
        {
            Health health;

            // Check for health component
            if (health = other.GetComponent<Health>())
            {
                // Check if health component has already been sent a command
                if (_checked == null) _checked = new List<Health>();
                foreach (Health h in _checked)
                {
                    if (health = h) return;
                }

                // If not, send command and add to checked list
                health.RemoveHp(_data);
                _checked.Add(health);
            }
        }
        protected void ResetColliderWhenInactive()
        {
            if (!c_collider.enabled && _checked != null) _checked = null;
        }
    }
}