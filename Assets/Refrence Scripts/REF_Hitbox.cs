using UnityEngine;
using System.Collections.Generic;
namespace Reference
{
    public class Hitbox : MonoBehaviour
    {
        public LayerMask LayerMask;
        private Collider _collider;
        private List<Health> _checked;
        private AttackData _data;

        // !!! MAKE ENABLING/DISABLING COLLIDER A METHOD IN THIS CLASS AND USE IT TO PASS A REFRENCE TO ATTACKDATA
        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }
        private void OnTriggerStay(Collider other)
        {
            Health health;
            if (health = other.GetComponent<Health>())
            {
                if (_checked == null) _checked = new List<Health>();
                foreach (Health h in _checked)
                {
                    if (health = h) return;
                }
                health.RemoveHp(_data);
                _checked.Add(health);
            }
        }
        private void Update()
        {
            if (!_collider.enabled && _checked != null) _checked = null;
        }

        public void ActivateHitBox(AttackData data)
        {
            _collider.enabled = true;
            _data = data;
        }
        public void DeactivateHitBox()
        {
            _collider.enabled = false;
            _data = null;
        }
    }
}