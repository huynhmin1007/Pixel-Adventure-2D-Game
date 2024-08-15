using Assets.Scripts.Base.State;
using Assets.Scripts.Characters.Enemy;
using Assets.Scripts.Common;

namespace Assets.Scripts.Characters.Skeleton
{
    public class SkeletonCharacter : EnemyCharacter
    {
        protected override void InitializeStates()
        {
            states.Add(EState.Idle, new SkeletonIdleState(this, stateMachine, EState.Idle));
            states.Add(EState.Move, new EnemyMoveState(this, stateMachine, EState.Move));
            states.Add(EState.Fall, new FallState(this, stateMachine, EState.Air));
            states.Add(EState.Battle, new SkeletonBattleState(this, stateMachine, EState.Move));
            states.Add(EState.Attack, new EnemyAttackState(this, stateMachine, EState.Attack, comboWindow));
            states.Add(EState.Stunned, new EnemyStunnedState(this, stateMachine, EState.Stunned));
        }

        protected override void StateController()
        {
        }

        public override void HandleBattle()
        {
            if (IsPlayerInAttackRange())
            {
                xInput = 0;
                if (CanPrimaryAttack())
                {
                    stateMachine.ChangeState(states[EState.Attack]);
                }
                else
                    stateMachine.ChangeState(states[EState.Idle]);
            }
            else
            {
                SetVelocity(MoveSpeed * Direction.XValue(), YVelocity);
            }
        }

        public override void Stun()
        {
            base.Stun();
            stateMachine.ChangeState(states[EState.Stunned]);
        }

        public override bool CanBattle()
        {
            return CanPrimaryAttack();
        }
    }
}
