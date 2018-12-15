using UnityEngine;
using UnityEngine.Events;
using System;

namespace Sierra.Unity2D.TopDown
{
    public class Health : MonoBehaviour
    {
        public int Hp = 10;
        public int HpMax = 10;
        public int Weight = 1;
        public bool AffectedByKnockback = false;

        [Serializable]
        public class OnDamageEvent : UnityEvent { };
        public OnDamageEvent OnDamage = new OnDamageEvent();
        [Serializable]
        public class OnDeathEvent : UnityEvent { };
        public OnDeathEvent OnDeath = new OnDeathEvent();

        protected Rigidbody _rb;

        public AttackData PreviousHitData { get; private set; }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        /// <summary>
        /// Removes all HP from this object and calls <see cref="OnDeath"/>.
        /// </summary>
        /// <remarks>
        /// Sets <see cref="PreviousHitData"/> to null.
        /// </remarks>
        public void Kill()
        {
            Hp = 0;
            OnDeath.Invoke();
            PreviousHitData = null;
        }
        /// <summary>
        /// Removes all HP from this object, and calls <see cref="OnDamage"/> in addition to <see cref="OnDeath"/>.
        /// </summary>
        /// <param name="hitInfo"></param>
        public void Kill(AttackData data)
        {
            PreviousHitData = data;
            OnDamage.Invoke();
            Kill();
        }
        /// <summary>
        /// Remove HP and store additional information in <see cref="PreviousHitData"/>.
        /// </summary>
        /// <param name="hitInfo"></param>
        public void RemoveHp(AttackData data)
        {
            PreviousHitData = data;
            if (data.Damage != 0) Hp -= Convert.ToInt16(data.Damage);
            if (Hp <= 0) OnDeath.Invoke();
            OnDamage.Invoke();
        }
    }
}