using Assets.Scripts.Base;
using Assets.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Characters.ObservatoryBoss.states
{
    public class FireBallState : ObservatoryBossBaseState
    {
        private int count;

        public FireBallState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
            count = 0;
        }

        public override void Update()
        {
            base.Update();

            if(count < 4 && boss.fireBallSkill.CanUseSkill())
            {
                count++;

                boss.fireBallSkill.UseSkill();
            }

            if(count == 4)
            {
                stateMachine.ChangeState(boss.GetState(EState.Idle));
            }
        }
    }
}
