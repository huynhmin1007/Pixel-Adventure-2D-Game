using Assets.Scripts.Base;
using Assets.Scripts.Base.State;
using System;

namespace Assets.Scripts.Characters.Player
{
    public class PlayerWallSlideState : WallSlideState
    {
        private PlayerCharacter character;

        public PlayerWallSlideState(PlayerCharacter characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            this.character = characterBase;
        }

        public override void Enter()
        {
            base.Enter();
            character.JumpCount = 0;
        }
    }
}
