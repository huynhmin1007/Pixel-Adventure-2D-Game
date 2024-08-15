using Assets.Scripts.Common;
using System;

namespace Assets.Scripts.Base.State
{
    public class IdleState : GroundedState
    {
        public IdleState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            characterBase.ResetVelocity();
        }

        public override void Update()
        {
            base.Update();

            if (characterBase.XInput != 0 && !characterBase.IsBusy)
                stateMachine.ChangeState(characterBase.GetState(EState.Move));
        }
    }
}
