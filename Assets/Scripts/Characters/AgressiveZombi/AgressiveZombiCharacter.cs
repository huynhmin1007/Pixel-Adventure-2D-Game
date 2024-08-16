using Assets.Scripts.Base.State;
using Assets.Scripts.Characters.Enemy;
using Assets.Scripts.Common;

namespace Assets.Scripts.Characters.AgressiveZombi
{
    public class AgressiveZombiCharacter : EnemyCharacter
    {
        public override bool CanBattle()
        {
            bool isPlayerDetected = IsPlayerDetected();

            if (isPlayerDetected)
            {
                if (IsPlayerInAttackRange())
                {
                    if (CanPrimaryAttack())
                    {
                        stateMachine.ChangeState(states[EState.Attack]);
                    }
                    else
                        stateMachine.ChangeAnimation(EState.Idle);
                    return true;
                }
                else
                {
                    stateMachine.ChangeAnimation(EState.Move);
                    SetVelocity(Direction.XValue() * MoveSpeed, YVelocity);
                }
            }
            return isPlayerDetected;
        }

        protected override void InitializeStates()
        {
            states.Add(EState.Idle, new EnemyIdleState(this, stateMachine, EState.Idle));
            states.Add(EState.Move, new EnemyMoveState(this, stateMachine, EState.Move));
            states.Add(EState.Fall, new FallState(this, stateMachine, EState.Air));
            states.Add(EState.Battle, new AgressiveZombiBattleState(this, stateMachine, EState.Move));
            states.Add(EState.Jump, new JumpState(this, stateMachine, EState.Air));
            states.Add(EState.Attack, new EnemyAttackState(this, stateMachine, EState.Attack, comboWindow));
            states.Add(EState.Stunned, new EnemyStunnedState(this, stateMachine, EState.Stunned));
        }

        protected override void StateController()
        {

        }

        public override void Stun()
        {
            base.Stun();
            stateMachine.ChangeState(states[EState.Stunned]);
        }
    }
}
