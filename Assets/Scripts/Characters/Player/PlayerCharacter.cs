using Assets.Scripts.Base.State;
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

        [Header("Attack")]
        [SerializeField] private float counterCooldown;
        private float counterTimer;

        #endregion

        #region Skill
        public DashSkill dashSkill { get; private set; }
        public CloneSkill cloneSkill { get; private set; }
        public SwordSkill swordSkill { get; private set; }
        public BlackHoleSkill blackHoleSkill { get; private set; }
        public CrystalSkill crystalSkill { get; private set; }
        public GameObject swordObj { get; private set; }

        #endregion

        protected override void Awake()
        {
            GameObject skillManagerObject = GameObject.Find("SkillManager");

            dashSkill = skillManagerObject.GetComponent<DashSkill>();
            cloneSkill = skillManagerObject.GetComponent<CloneSkill>();
            swordSkill = skillManagerObject.GetComponent<SwordSkill>();
            blackHoleSkill = skillManagerObject.GetComponent<BlackHoleSkill>();
            crystalSkill = skillManagerObject.GetComponent<CrystalSkill>();

            base.Awake();
        }

        protected override void Start()
        {
            base.Start();

            swordSkill.OnAssignSword += AssignNewSword;

            swordSkill.OnCatchSword += CatchSword;
            blackHoleSkill.OnExitBlackHoleAbility += ExitBlackHoleAbility;
        }

        private void OnDestroy()
        {
            if (swordSkill != null)
            {
                swordSkill.OnAssignSword -= AssignNewSword;
                swordSkill.OnCatchSword -= CatchSword;
            }

            if (blackHoleSkill != null)
            {
                blackHoleSkill.OnExitBlackHoleAbility -= ExitBlackHoleAbility;
            }
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
            states.Add(EState.Dash, new PlayerDashState(this, stateMachine, EState.Dash, dashSkill.DashSpeed, dashSkill.DashDuration));
            states.Add(EState.WallSlide, new PlayerWallSlideState(this, stateMachine, EState.WallSlide));
            states.Add(EState.WallJump, new WallJumpState(this, stateMachine, EState.Air));
            states.Add(EState.Attack, new PrimaryAttackState(this, stateMachine, EState.Attack, ComboWindow));
            states.Add(EState.CounterAttack, new PlayerCounterAttackState(this, stateMachine, EState.CounterAttack));
            states.Add(EPlayerState.AimSword, new PlayerAimSwordState(this, stateMachine, EPlayerState.AimSword));
            states.Add(EPlayerState.CatchSword, new PlayerCatchSwordState(this, stateMachine, EPlayerState.CatchSword));
            states.Add(EPlayerState.BlackHole, new PlayerBlackHoleState(this, stateMachine, EState.Air));
        }

        protected override void StateController()
        {
            XInput = Input.GetAxisRaw("Horizontal");
            YInput = Input.GetAxisRaw("Vertical");

            if (Input.GetKeyDown(KeyCode.R) && blackHoleSkill.CanUseSkill())
            {
                stateMachine.ChangeState(states[EPlayerState.BlackHole]);
            }
            else if (Input.GetKeyDown(KeyCode.F) && crystalSkill.CanUseSkill())
            {
                crystalSkill.UseSkill();
            }
            else if (Input.GetKeyDown(KeyCode.Tab) && HasNoSword())
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
            else if (Input.GetKeyDown(KeyCode.LeftControl) && dashSkill.CanUseSkill())
            {
                dashSkill.UseSkill();
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
            swordObj = _newSword;
        }

        public void CatchSword()
        {
            stateMachine.ChangeState(states[EPlayerState.CatchSword]);
            Destroy(swordObj);
        }

        private bool HasNoSword()
        {
            if (!swordObj)
            {
                return true;
            }

            swordObj.GetComponent<SwordSkillController>().ReturnSword();
            return false;
        }

        private void ExitBlackHoleAbility()
        {
            ((SkillState)states[EPlayerState.BlackHole]).IsCasting = false;
            stateMachine.ChangeState(states[EState.Fall]);
        }
    }
}
