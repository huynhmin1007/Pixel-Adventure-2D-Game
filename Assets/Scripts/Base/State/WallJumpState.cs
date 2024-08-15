using Assets.Scripts.Common;
using System;

namespace Assets.Scripts.Base.State
{
    public class WallJumpState : BaseState
    {
        public WallJumpState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            characterBase.SetVelocity(5 * -characterBase.Direction.XValue(), characterBase.JumpForce, true);
        }

        public override void Update()
        {
            base.Update();

            characterBase.animator.SetFloat("yVelocity", characterBase.YVelocity);

            if (characterBase.XInput != 0 || characterBase.YVelocity <= 0)
                stateMachine.ChangeState(characterBase.GetState(EState.Fall));
        }
    }
}
