using Assets.Scripts.Base.State;
using Assets.Scripts.Characters.Enemy;
using Assets.Scripts.Characters.Skeleton;
using Assets.Scripts.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArbiterCharacter : EnemyCharacter
{
    public override bool CanBattle()
    {
        return CanPrimaryAttack();
    }

    public override void HandleBattle()
    {
        if (CanPrimaryAttack())
        {
            stateMachine.ChangeState(states[EState.Attack]);
        }
        else
        {
            stateMachine.ChangeState(states[EState.Idle]);
        }
    }

    protected override void InitializeStates()
    {
        states.Add(EState.Idle, new EnemyIdleState(this, stateMachine, EState.Idle));
        states.Add(EState.Move, new EnemyMoveState(this, stateMachine, EState.Move));
        states.Add(EState.Fall, new FallState(this, stateMachine, EState.Air));
        states.Add(EState.Battle, new EnemyMoveState(this, stateMachine, EState.Move));
        states.Add(EState.Jump, new JumpState(this, stateMachine, EState.Air));
        states.Add(EState.Attack, new EnemyAttackState(this, stateMachine, EState.Attack, comboWindow));
        states.Add(EState.Stunned, new EnemyStunnedState(this, stateMachine, EState.Stunned));
    }

    protected override void StateController()
    {
        
    }
}
