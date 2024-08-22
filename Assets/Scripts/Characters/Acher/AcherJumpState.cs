using Assets.Scripts.Base;
using Assets.Scripts.Base.State;
using Assets.Scripts.Common;
using System;

namespace Assets.Scripts.Characters.Acher
{
    public class AcherJumpState : AirState
    {
        private AcherCharacter acher;
        public AcherJumpState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            acher = (AcherCharacter)characterBase;
        }

        public override void Enter()
        {
            base.Enter();

            acher.SetVelocity(acher.jumpVelocity.x * -acher.Direction.XValue(), acher.jumpVelocity.y);
        }

        public override void Update()
        {
            base.Update();

            if (acher.YVelocity < 0 && acher.IsGrounded())
            {
                stateMachine.ChangeState(acher.GetState(EState.Battle));
            }
        }
    }
}
