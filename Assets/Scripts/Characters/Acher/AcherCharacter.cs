using Assets.Scripts.Base.State;
using Assets.Scripts.Characters.Enemy;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Characters.Acher
{
    public class AcherCharacter : EnemyCharacter
    {
        [Header("Acher spisifc info")]
        [SerializeField] private GameObject arrowPrefab;
        [SerializeField] private float arrowSpeed;
        [SerializeField] private float arrowDamage;

        public Vector2 jumpVelocity;
        public float jumpCooldown;
        public float safeDistance;
        [HideInInspector] public float lastTimeJumped;


        public override bool CanBattle()
        {
            bool isPlayerDetected = IsPlayerDetected();

            if (isPlayerDetected)
            {
                if (CanPrimaryAttack())
                {
                    stateMachine.ChangeState(states[EState.Attack]);
                }
                else
                    stateMachine.ChangeAnimation(EState.Idle);
                return true;
            }

            return false;
        }

        protected override void InitializeStates()
        {
            states.Add(EState.Idle, new EnemyIdleState(this, stateMachine, EState.Idle));
            states.Add(EState.Move, new EnemyMoveState(this, stateMachine, EState.Move));
            states.Add(EState.Fall, new FallState(this, stateMachine, EState.Air));
            states.Add(EState.Jump, new AcherJumpState(this, stateMachine, EState.Air));
            states.Add(EState.Battle, new AcherBattleState(this, stateMachine, EState.Move));
            states.Add(EState.Attack, new AcherAttackState(this, stateMachine, EState.Attack, comboWindow));
            states.Add(EState.Stunned, new EnemyStunnedState(this, stateMachine, EState.Stunned));
            states.Add(EState.Dead, new EnemyDeadState(this, stateMachine, EState.Idle));
        }

        protected override void StateController()
        {

        }

        public override void Stun()
        {
            base.Stun();
            stateMachine.ChangeState(states[EState.Stunned]);
        }

        protected override void Update()
        {
            base.Update();
        }

        public override void AnimationSpecialAttackTrigger()
        {
            base.AnimationSpecialAttackTrigger();

            GameObject newArrow = Instantiate(arrowPrefab, hitboxAttack.transform.position, Quaternion.identity);
            newArrow.GetComponent<Arrow_Controller>().SetupArrow(arrowSpeed * Direction.XValue(), stats);
        }
    }
}
