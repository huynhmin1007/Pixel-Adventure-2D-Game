using Assets.Scripts.Common;
using System;

namespace Assets.Scripts.Base.State
{
    public class WallSlideState : BaseState
    {
        public WallSlideState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();

            if (characterBase.YVelocity > 0)
                return;

            if ((characterBase.XInput != 0 && characterBase.Direction.XValue() != characterBase.XInput) ||
                characterBase.IsGrounded())
                stateMachine.ChangeState(characterBase.GetState(EState.Idle));

            if (characterBase.YInput < 0)
                characterBase.SetVelocity(0, characterBase.YVelocity);
            else if (characterBase.YInput == 0)
                characterBase.SetVelocity(0, characterBase.YVelocity * .7f);
            else
                stateMachine.ChangeState(characterBase.GetState(EState.WallJump));
        }
    }
}
