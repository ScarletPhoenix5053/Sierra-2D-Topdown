using UnityEngine;
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
            yield return new WaitForSeconds(Utility.FramesToSeconds(Data.Startup));

            Data.Hitbox.ActivateHitBox(Data);
            yield return new WaitForSeconds(Utility.FramesToSeconds(Data.Active));

            Data.Hitbox.DeactivateHitBox();
            yield return new WaitForSeconds(Utility.FramesToSeconds(Data.Recovery));

            Attacking = false;
        }
    }
}
