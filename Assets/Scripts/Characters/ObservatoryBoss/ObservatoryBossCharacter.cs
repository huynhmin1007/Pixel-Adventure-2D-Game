using Assets.Scripts.Base.State;
using Assets.Scripts.Characters.Enemy;
using Assets.Scripts.Characters.Skeleton;
using Assets.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Characters.ObservatoryBoss
{
    public class ObservatoryBossCharacter : EnemyCharacter
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
            states.Add(EState.Battle, new ObservatoryBossBattleState(this, stateMachine, EState.Move));
            states.Add(EState.Attack, new EnemyAttackState(this, stateMachine, EState.Attack, comboWindow));
            states.Add(EState.Stunned, new EnemyStunnedState(this, stateMachine, EState.Stunned));
        }

        protected override void StateController()
        {
        }
    }
}
