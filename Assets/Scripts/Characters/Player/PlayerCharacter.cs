using Assets.Scripts.Base.State;
using Assets.Scripts.Characters.Parameters;
using Assets.Scripts.Characters.Skills;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Characters.Player
{
    public class PlayerCharacter : Character
    {
        #region Parameters
        [Header("Movement")]
        [SerializeField] private int maxJumpCount;
        private int jumpCount;
        [SerializeField] private float swordReturnImpact;

        [SerializeField] private Dash dash;

        [Header("Attack")]
        [SerializeField] private float counterCooldown;
        private float counterTimer;

        public SkillManager skill { get; private set; }
        public GameObject sword;

        #endregion

        #region States
        #endregion

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();

            skill = SkillManager.instance;
        }

        protected override void Update()
        {
            counterTimer -= Time.deltaTime;
            base.Update();
        }

        protected override void InitializeStates()
        {
            states.Add(EState.Idle, new PlayerIdleState(this, stateMachine, EState.Idle));
            states.Add(EState.Move, new MoveState(this, stateMachine, EState.Move));
            states.Add(EState.Jump, new JumpState(this, stateMachine, EState.Air));
            states.Add(EState.Fall, new FallState(this, stateMachine, EState.Air));
            states.Add(EState.Dash, new PlayerDashState(this, stateMachine, EState.Dash, dash.DashSpeed, dash.DashDuration));
            states.Add(EState.WallSlide, new PlayerWallSlideState(this, stateMachine, EState.WallSlide));
            states.Add(EState.WallJump, new WallJumpState(this, stateMachine, EState.Air));
            states.Add(EState.Attack, new PrimaryAttackState(this, stateMachine, EState.Attack, ComboWindow));
            states.Add(EState.CounterAttack, new PlayerCounterAttackState(this, stateMachine, EState.CounterAttack));
            states.Add(EPlayerState.AimSword, new PlayerAimSwordState(this, stateMachine, EPlayerState.AimSword));
            states.Add(EPlayerState.CatchSword, new PlayerCatchSwordState(this, stateMachine, EPlayerState.CatchSword));
        }

        protected override void StateController()
        {
            XInput = Input.GetAxisRaw("Horizontal");
            YInput = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(KeyCode.Tab) && HasNoSword())
            {
                stateMachine.ChangeState(states[EPlayerState.AimSword]);
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1) && stateMachine.CurrentState is GroundedState && counterTimer < 0)
            {
                counterTimer = counterCooldown;
                stateMachine.ChangeState(states[EState.CounterAttack], true);
            }
            else if (Input.GetKey(KeyCode.Mouse0))
            {
                stateMachine.ChangeState(states[EState.Attack], true);
            }
            else if (Input.GetKeyDown(KeyCode.Space) && JumpCount < MaxJumpCount)
            {
                JumpCount++;
                YInput = 1;
            }
            else if (Input.GetKeyDown(KeyCode.LeftControl) && SkillManager.instance.dash.CanUseSkill())
            {
                stateMachine.ChangeState(states[EState.Dash]);
            }
        }

        public int MaxJumpCount { get => maxJumpCount; set => maxJumpCount = value; }
        public int JumpCount { get => jumpCount; set => jumpCount = value; }
        public float SwordReturnImpact { get => swordReturnImpact; set => swordReturnImpact = value; }

        public void AnimationEndCounterTrigger()
            => ((PlayerCounterAttackState)states[EState.CounterAttack]).AnimationEndCounterTrigger();

        public void AssignNewSword(GameObject _newSword)
        {
            sword = _newSword;
        }

        public void CatchSword()
        {
            stateMachine.ChangeState(states[EPlayerState.CatchSword]);
            Destroy(sword);
        }

        private bool HasNoSword()
        {
            if (!sword)
            {
                return true;
            }

            sword.GetComponent<SwordSkillController>().ReturnSword();
            return false;
        }
    }
}
