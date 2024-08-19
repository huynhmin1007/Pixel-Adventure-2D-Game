using Assets.Scripts.Common;
using System;
using UnityEngine;

namespace Assets.Scripts.Base.State
{
    public class DashState : BaseState
    {
        private float dashSpeed;
        private float dashDuration;

        public DashState(Character characterBase, StateMachine stateMachine, Enum animBoolName, float dashSpeed, float dashDuration)
            : base(characterBase, stateMachine, animBoolName)
        {
            this.dashSpeed = dashSpeed;
            this.dashDuration = dashDuration;
        }

        public override void Enter()
        {
            base.Enter();

            stateTimer = dashDuration;
            characterBase.SetVelocity(dashSpeed * characterBase.Direction.XValue(), Mathf.Max(characterBase.YVelocity, 0));
        }

        public override void Update()
        {
            base.Update();

            if (characterBase.YInput > 0)
            {
                stateMachine.ChangeState(characterBase.GetState(EState.Jump));
                return;
            }

            if (stateTimer < 0)
                stateMachine.ChangeState(characterBase.GetState(EState.Idle));
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
