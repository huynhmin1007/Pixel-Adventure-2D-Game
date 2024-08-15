using Assets.Scripts.Base;
using Assets.Scripts.Common;
using System;

namespace Assets.Scripts.Characters.Enemy
{
    public class EnemyMoveState : EnemyGroundedState
    {
        private EnemyCharacter character;

        public EnemyMoveState(EnemyCharacter characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            character = characterBase;
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

            if (character.IsWallDetected() || !character.IsGrounded())
            {
                character.Flip();
                character.ResetVelocity();
                return;
            }

            if (character.XInput == 0)
                stateMachine.ChangeState(character.GetState(EState.Idle));

            character.SetVelocity(character.XInput * character.MoveSpeed, character.YVelocity, flip: true);
        }
    }
}
