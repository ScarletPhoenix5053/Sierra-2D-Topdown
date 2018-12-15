using System;
using System.Collections;
using UnityEngine;


namespace Reference
{
    public class ProtoPlayer : ProtoCharControl
    {
        public float MoveSpeed;
        public AttackData[] Attacks;
        public bool LogAttacks = true;

        [ReadOnly]
        public bool Attacking;

        public Vector2 MoveInput { get; set; }

        private Animator _anim;

        protected override void Awake()
        {
            _anim = GetComponent<Animator>();
            base.Awake();
        }
        protected override void FixedUpdate()
        {
            GetVelocity();
            ApplyMovement();
        }
        private void Update()
        {
            GetDirectionalInput();
            GetActionInput();
        }

        private void GetDirectionalInput()
        {
            var w = Input.GetKey(KeyCode.W);
            var a = Input.GetKey(KeyCode.A);
            var s = Input.GetKey(KeyCode.S);
            var d = Input.GetKey(KeyCode.D);
            var moveInput = Vector2.zero;

            if (!(a && d))
            {
                if (a) moveInput.x = -1;
                else if (d) moveInput.x = 1;
            }
            if (!(w && s))
            {
                if (w) moveInput.y = 1;
                else if (s) moveInput.y = -1;
            }
            MoveInput = moveInput;
        }
        private void GetActionInput()
        {
            var sLP = KeyCode.U;
            var sMP = KeyCode.I;
            var sHP = KeyCode.O;

            if (!Attacking)
            {
                if (Input.GetKeyDown(sLP)) DoStandLightPunch();
                if (Input.GetKeyDown(sMP)) DoStandMediumPunch();
                if (Input.GetKeyDown(sHP)) DoStandHeavyPunch();
            }
            else
            {
                // write (layered?) statemachine to enable chaining
            }
        }
        private void GetVelocity()
        {
           
            /*
            var velocity = Vector3.zero;

            switch (PerspectiveSwitcher.CurrentPerspective)
            {
                case PerspectiveSwitcher.CubePerspective.top:
                    velocity.x = MoveInput.x;
                    velocity.z = MoveInput.y;
                    break;
                case PerspectiveSwitcher.CubePerspective.bottom:
                    break;
                case PerspectiveSwitcher.CubePerspective.left:
                    velocity.z = MoveInput.x * -1;
                    break;
                case PerspectiveSwitcher.CubePerspective.right:
                    velocity.z = MoveInput.x;
                    break;
                case PerspectiveSwitcher.CubePerspective.back:
                    velocity.x = MoveInput.x;
                    break;
                case PerspectiveSwitcher.CubePerspective.front:

                    velocity.x = MoveInput.x * -1;
                    break;
                default:
                    break;
            }

            UpdateVelocity(velocity * MoveSpeed);*/
        }
        private void DoStandLightPunch()
        {
            Debug.Log("s.LP");
            StartCoroutine(IE_Attack(Attacks[0]));
        }
        private void DoStandMediumPunch()
        {
            Debug.Log("s.MP");
            StartCoroutine(IE_Attack(Attacks[1]));
        }
        private void DoStandHeavyPunch()
        {
            Debug.Log("s.HP");
            StartCoroutine(IE_Attack(Attacks[2]));
        }
        private IEnumerator IE_Attack(AttackData data)
        {
            Attacking = true;
            yield return new WaitForSeconds(Utility.FramesToSeconds(data.Startup));

            _anim.SetTrigger(data.Animation);
            data.Hitbox.ActivateHitBox(data);
            if (LogAttacks) Debug.Log("\"" + data.Hitbox.name + "\"");
            yield return new WaitForSeconds(Utility.FramesToSeconds(data.Active));

            data.Hitbox.DeactivateHitBox();
            yield return new WaitForSeconds(Utility.FramesToSeconds(data.Recovery));

            Attacking = false;
            _anim.SetTrigger("Stand");
        }
    }
    [Serializable]
    public class AttackData
    {
        public Hitbox Hitbox;
        public string Animation = "Stand";
        public float Damage = 1;
        public float KnockUp = 0;
        public float KnockBack = 0;
        public int Startup = 6;
        public int Active = 1;
        public int Recovery = 8;
        public int Hitstun = 20;
        public int Hitstop = 3;
    }
}
