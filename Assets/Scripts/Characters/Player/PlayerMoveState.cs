using Assets.Scripts.Base;
using Assets.Scripts.Base.State;
using Assets.Scripts.Common;
using System;

namespace Assets.Scripts.Characters.Player
{
    public class PlayerMoveState : MoveState
    {
        private PlayerCharacter character;
        public PlayerMoveState(PlayerCharacter characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            this.character = characterBase;
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

            if (characterBase.XInput == characterBase.Direction.XValue() && characterBase.IsWallDetected())
                stateMachine.ChangeState(characterBase.GetState(EState.Idle));
        }
    }
}
