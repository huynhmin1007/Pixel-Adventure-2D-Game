using Assets.Scripts.Base;
using Assets.Scripts.Base.State;
using Assets.Scripts.Common;
using System;

namespace Assets.Scripts.Characters.Player
{
    public class PlayerIdleState : IdleState
    {
        private PlayerCharacter character;

        public PlayerIdleState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            this.character = (PlayerCharacter)characterBase;
        }

        public override void Enter()
        {
            if (character.IsGrounded()) character.JumpCount = 0;

            if (characterBase.XInput == characterBase.Direction.XValue() && characterBase.IsWallDetected())
            {
                return;
            }
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
