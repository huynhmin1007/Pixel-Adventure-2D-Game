using Assets.Scripts.Characters.Enemy;
using Assets.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Characters.Doctor
{
    public class DoctorCharacter : EnemyCharacter
    {
        public override bool CanBattle()
        {
            return CanPrimaryAttack();
        }

        public override void HandleBattle()
        {
           
        }

        protected override void InitializeStates()
        {
            states.Add(EState.Idle, new EnemyIdleState(this, stateMachine, EState.Idle));
            states.Add(EState.Move, new EnemyMoveState(this, stateMachine, EState.Move));
        }

        protected override void StateController()
        {
            
        }
    }
}
