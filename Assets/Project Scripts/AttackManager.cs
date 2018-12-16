using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Sierra.Unity2D.TopDown
{
    /// <summary>
    /// Class holding refrences to an entity's attacks. Temporarily: the attack itself.
    /// </summary>
    public class AttackManager : MonoBehaviour
    {
        [ReadOnly]
        public bool Attacking;
        public List<AttackData> Attacks;
        
        /// <summary>
        /// Checks for an attack with the name provided. Throws an exception if a match is not found.
        /// </summary>
        /// <param name="attackName"></param>
        public void Attack(string attackName)
        {
            foreach (AttackData attack in Attacks)
            {
                if (attackName.ToLower() == attack.Name.ToLower())
                {
                    StartCoroutine(IE_Attack(attack));
                    return;
                }
            }
            throw new MissingReferenceException(attackName + " could not be found in " + name + "'s attack method list.");
        }
        
        private IEnumerator IE_Attack(AttackData attack)
        {
            Attacking = true;
            attack.PlayAnimation();
            yield return new WaitForSeconds(Utility.FramesToSeconds(attack.Startup));

            attack.Hitbox.ActivateHitBox(attack);
            yield return new WaitForSeconds(Utility.FramesToSeconds(attack.Active));

            attack.Hitbox.DeactivateHitBox();
            yield return new WaitForSeconds(Utility.FramesToSeconds(attack.Recovery));

            Attacking = false;
        }
    }
    [Serializable]
    public class AttackData
    {
        #region Public Vars
        public string Name = "new attack";

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
