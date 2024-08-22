using Assets.Scripts.Base;
using Assets.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Characters.ObservatoryBoss.states
{
    public class FireBallState : ObservatoryBossBaseState
    {
        public Animator animator;
        private int count;
        private float fireBallCoolDown = 1f;
        private float fireBallTimer;

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

            fireBallTimer -= Time.deltaTime;

            if(count < 4 && fireBallTimer < 0)
            {
                fireBallTimer = fireBallCoolDown;
                count++;

                boss.fireBallSkill.UseSkill();
            }

            if(count == 4)
            {
                boss.fireBallSkill.TriggerExitSkill();
                stateMachine.ChangeState(boss.GetState(EState.Idle));
            }
        }
    }
}
