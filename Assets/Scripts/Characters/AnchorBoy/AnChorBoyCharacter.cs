using Assets.Scripts.Base.State;
using Assets.Scripts.Characters.Enemy;
using Assets.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Characters.AnchorBoy
{
    public class AnChorBoyCharacter : EnemyCharacter
    {
        public override bool CanBattle()
        {

            return CanPrimaryAttack();

        }

        //public override void HandleBattle()
        //{
        //    if (CanPrimaryAttack())
        //    {
        //        stateMachine.ChangeState(states[EState.Attack]);
        //    }
        //    else
        //    {
        //        stateMachine.ChangeState(states[EState.Idle]);
        //    }
        //}

        protected override void InitializeStates()
        {
            states.Add(EState.Idle, new EnemyIdleState(this, stateMachine, EState.Idle));
            states.Add(EState.Move, new EnemyMoveState(this, stateMachine, EState.Move));
            states.Add(EState.Fall, new FallState(this, stateMachine, EState.Air));
            states.Add(EState.Battle, new AnchorBoyBattleState(this, stateMachine, EState.Move));
            states.Add(EState.Jump, new JumpState(this, stateMachine, EState.Air));
            states.Add(EState.Attack, new EnemyAttackState(this, stateMachine, EState.Attack, comboWindow));
            states.Add(EState.Stunned, new EnemyStunnedState(this, stateMachine, EState.Stunned));

        }

        protected override void StateController()
        {
            //if (Input.GetKeyDown(KeyCode.U)) {
            //    stateMachine.ChangeState(states[EState.Jump]);
            //}

        }
    }
}
