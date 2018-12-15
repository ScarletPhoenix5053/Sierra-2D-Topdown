using UnityEngine;
using UnityEngine.Events;
using System;

namespace Sierra.Unity2D.TopDown
{
    public class Health : MonoBehaviour
    {
        public int Hp = 10;
        public int HpMax = 10;

        [Serializable]
        public class OnDamageEvent : UnityEvent { };
        public OnDamageEvent OnDamage = new OnDamageEvent();
        [Serializable]
        public class OnDeathEvent : UnityEvent { };
        public OnDeathEvent OnDeath = new OnDeathEvent();

        public AttackData PreviousHitData { get; private set; }
        public bool Dead { get { return Hp <= 0; } }

        private void Awake()
        {

        }
        private void Update()
        {

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
        /// Removes HP and stores additional information in <see cref="PreviousHitData"/>.
        /// Will call <see cref="OnDamage"/>.
        /// Does not work if instance is <see cref="Dead"/>.
        /// </summary>
        /// <param name="hitInfo"></param>
        public void RemoveHp(AttackData data)
        {
            // log error and return if dead
            if (Dead)
            {
                Debug.LogError(name + " is already dead");
                return;
            }

            // Assign data and adjust hp
            PreviousHitData = data;
            if (data.Damage != 0) Hp -= Convert.ToInt16(data.Damage);

            // Call events
            OnDamage.Invoke();
            if (Dead)
            {
                Hp = 0;
                OnDeath.Invoke();
            }
        }

        public void LogHp()
        {
            Debug.Log(name + ": " + Hp + "/ " + HpMax);
        }
        public void LogDeath()
        {
            Debug.Log(name + " is dead");
        }
    }
}