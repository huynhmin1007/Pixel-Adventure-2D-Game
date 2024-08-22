using Assets.Scripts.Base;
using Assets.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.TextCore.Text;

namespace Assets.Scripts.Characters.ObservatoryBoss.states
{
    public class BossIdleState : BaseState
    {
        private ObservatoryBossCharacter boss;

        public BossIdleState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            boss = (ObservatoryBossCharacter)characterBase;
        }

        public override void Enter()
        {
            base.Enter();

            boss.ResetVelocity();
            stateTimer = boss.IdleTime;
        }

        public override void Update()
        {
            base.Update();
            if (stateTimer < 0)
                stateMachine.ChangeState(boss.GetState(EState.Battle));
        }
    }
}
