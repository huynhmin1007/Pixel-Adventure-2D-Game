using Assets.Scripts.Common;
using System;

namespace Assets.Scripts.Base.State
{
    public class AirState : BaseState
    {
        public AirState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
            characterBase.animator.SetFloat("yVelocity", characterBase.YVelocity);

            if (characterBase.IsWallDetected())
                stateMachine.ChangeState(characterBase.GetState(EState.WallSlide));
        }
    }
}
