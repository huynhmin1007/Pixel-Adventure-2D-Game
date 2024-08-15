using Assets.Scripts.Common;
using System;

namespace Assets.Scripts.Base.State
{
    public class MoveState : GroundedState
    {
        public MoveState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();

            if (characterBase.XInput == 0)
                stateMachine.ChangeState(characterBase.GetState(EState.Idle));

            characterBase.SetVelocity(characterBase.XInput * characterBase.MoveSpeed, characterBase.YVelocity, flip: true);
        }
    }
}
