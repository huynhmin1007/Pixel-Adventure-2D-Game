using Assets.Scripts.Common;
using System;
using UnityEngine;

namespace Assets.Scripts.Base.State
{
    public class PrimaryAttackState : BaseState
    {
        private int comboCounter;
        private float lastTimeAttacked;
        private int comboWindow;

        public PrimaryAttackState(Character characterBase, StateMachine stateMachine, Enum animBoolName, int comboWindow)
            : base(characterBase, stateMachine, animBoolName)
        {
            this.comboWindow = comboWindow;
        }

        public override void Enter()
        {
            base.Enter();

            if (comboCounter > comboWindow || Time.time >= lastTimeAttacked + comboWindow)
                comboCounter = 0;

            characterBase.animator.SetInteger("ComboCounter", comboCounter);

            float attackDir = characterBase.XInput != 0 ? characterBase.XInput : characterBase.Direction.XValue();
            float xVelocity = !characterBase.IsGrounded() ? attackDir * characterBase.MoveSpeed * .2f : attackDir;

            characterBase.SetVelocity(
                characterBase.AttackMove[comboCounter].x * xVelocity, characterBase.AttackMove[comboCounter].y, true);

            stateTimer = .2f;
        }

        public override void Exit()
        {
            base.Exit();
            characterBase.StartCoroutine("BusyFor", .15f);

            comboCounter++;
            lastTimeAttacked = Time.time;
        }

        public override void Update()
        {
            base.Update();

            if (stateTimer < 0 && characterBase.IsGrounded())
                characterBase.ResetVelocity();

            if (triggerCalled)
                stateMachine.ChangeState(characterBase.GetState(EState.Idle));
        }
    }
}
