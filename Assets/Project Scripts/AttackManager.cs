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
        [ReadOnly] public bool Attacking;
        [ReadOnly] public AttackData CurrentAttack = null;

        public List<AttackData> Attacks;
        public List<AttackData> CanChainInto
        {
            get
            {
                var chainable = new List<AttackData>();

                // Go through all attacks
                foreach (AttackData attack in Attacks)
                {
                    // Check name with chianable strings
                    foreach (string name in CurrentAttack.ChainsInto)
                    {
                        // Add to new list if match
                        if (name == attack.Name) chainable.Add(attack);
                    }
                }

                return null;
            }
        }

        private void Awake()
        {
            // Ensure current attack is null.
            CurrentAttack = null;
        }

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
                    // Match found:
                    CurrentAttack = attack;
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
            attack.SetState(AttackData.AttackState.Startup);
            yield return new WaitForSeconds(Utility.FramesToSeconds(attack.Startup));

            attack.Hitbox.ActivateHitBox(attack);
            attack.SetState(AttackData.AttackState.Active);
            yield return new WaitForSeconds(Utility.FramesToSeconds(attack.Active));

            attack.SetState(AttackData.AttackState.Recovery);
            attack.Hitbox.DeactivateHitBox();
            yield return new WaitForSeconds(Utility.FramesToSeconds(attack.Recovery));

            Attacking = false;
            CurrentAttack = null;
            attack.SetState(AttackData.AttackState.Inactive);
        }
    }
    [Serializable]
    public class AttackData
    {
        #region Public Vars
        public string Name = "none";
        public AttackState State = AttackState.Inactive;

        public int Startup;
        public int Active ;
        public int Recovery ;

        public float Damage;
        public float KnockUp;
        public float KnockBack;

        public int Hitstun;
        public int Hitstop;

        public Hitbox Hitbox;
        public Animation AnimationComponent;
        public string AnimationName;
        public bool UseAnimation;

        public bool AllowChaining = false;
        public string[] ChainsInto;
        #endregion

        public enum AttackState
        {
            Inactive, Startup, Active, Recovery
        }

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
        public void SetState(AttackState newState)
        {
            State = newState;
        }
    }
}
