using Assets.Scripts.Common;
using System;

namespace Assets.Scripts.Base.State
{
    public class FallState : AirState
    {
        public FallState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
        }

        public override void Update()
        {
            base.Update();

            if (characterBase.YVelocity == 0 || characterBase.IsGrounded())
                stateMachine.ChangeState(characterBase.GetState(EState.Idle));
            else if (characterBase.YInput > 0)
                stateMachine.ChangeState(characterBase.GetState(EState.Jump));

            float xVelocity = characterBase.XInput * characterBase.MoveSpeed * .8f;

            if (characterBase.XVelocity != 0 && characterBase.XInput == 0)
                xVelocity = characterBase.Direction.XValue() * characterBase.MoveSpeed * .8f;

            characterBase.SetVelocity(xVelocity, characterBase.YVelocity, flip: true);
        }
    }
}
