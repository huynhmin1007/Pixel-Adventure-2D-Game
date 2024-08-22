using Assets.Scripts.Base;
using System;

namespace Assets.Scripts.Characters.Player
{
    public class PlayerDeadState : BaseState
    {
        private PlayerCharacter player;

        public PlayerDeadState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            player = (PlayerCharacter)characterBase;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            player.ResetVelocity();
        }
    }
}