using Assets.Scripts.Common;
using System;

namespace Assets.Scripts.Base.State
{
    public class JumpState : AirState
    {
        public JumpState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            characterBase.SetVelocity(characterBase.XInput * characterBase.MoveSpeed * .8f, characterBase.JumpForce, flip: true);
        }

        public override void Update()
        {
            base.Update();

            if (characterBase.YVelocity < 0)
                stateMachine.ChangeState(characterBase.GetState(EState.Fall));
            else if (characterBase.YInput > 0)
                stateMachine.ChangeState(characterBase.GetState(EState.Jump));

            characterBase.SetVelocity(characterBase.XInput * characterBase.MoveSpeed * .8f, characterBase.YVelocity, flip: true);
        }
    }
}
