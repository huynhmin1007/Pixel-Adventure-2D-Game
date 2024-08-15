using Assets.Scripts.Common;
using System;

namespace Assets.Scripts.Base.State
{
    public class GroundedState : BaseState
    {
        public GroundedState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
        }

        public override void Update()
        {
            base.Update();

            if (characterBase.YInput > 0)
            {
                stateMachine.ChangeState(characterBase.GetState(EState.Jump));
                return;
            }
            else if (characterBase.YVelocity < 0 && !characterBase.IsGrounded())
            {
                stateMachine.ChangeState(characterBase.GetState(EState.Fall));
                return;
            }
        }
    }
}
