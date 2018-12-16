using UnityEngine;
using System;
using System.Collections;

namespace Sierra.Unity2D.TopDown
{
    /// <summary>
    /// Class holding refrences to an entity's attacks. Temporarily: the attack itself.
    /// </summary>
    public class AttackManager : MonoBehaviour
    {
        [ReadOnly]
        public bool Attacking;
        public AttackData Data;

        public void Attack()
        {
            StartCoroutine(IE_Attack());
        }

        private IEnumerator IE_Attack()
        {
            Attacking = true;
            Data.PlayAnimation();
            yield return new WaitForSeconds(Utility.FramesToSeconds(Data.Startup));

            Data.Hitbox.ActivateHitBox(Data);
            yield return new WaitForSeconds(Utility.FramesToSeconds(Data.Active));

            Data.Hitbox.DeactivateHitBox();
            yield return new WaitForSeconds(Utility.FramesToSeconds(Data.Recovery));

            Attacking = false;
        }
    }
    [Serializable]
    public class AttackData
    {
        #region Public Vars
        public int Startup = 6;
        public int Active = 1;
        public int Recovery = 8;

        public float Damage = 1;
        public float KnockUp = 0;
        public float KnockBack = 0;

        public int Hitstun = 20;
        public int Hitstop = 3;

        public Hitbox Hitbox;
        public Animation AnimationComponent;
        public string AnimationName;
        public bool UseAnimation;
        #endregion

        /// <summary>
        /// Will not work if <see cref="UseAnimation"/> is false.
        /// </summary>
        public void PlayAnimation()
        {
            if (UseAnimation)
            {
                AnimationComponent.Play(AnimationName);
            }
        }
    }
}
