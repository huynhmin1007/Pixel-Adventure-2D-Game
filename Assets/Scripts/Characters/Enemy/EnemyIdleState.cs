using Assets.Scripts.Base;
using Assets.Scripts.Common;
using System;

namespace Assets.Scripts.Characters.Enemy
{
    public class EnemyIdleState : EnemyGroundedState
    {
        private EnemyCharacter character;

        public EnemyIdleState(EnemyCharacter characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            character = characterBase;
        }

        public override void Enter()
        {
            base.Enter();

            characterBase.ResetVelocity();
            stateTimer = character.IdleTime;

        }

        public override void Update()
        {
            base.Update();

            if (stateTimer < 0)
                character.XInput = character.Direction.XValue();

            if (character.XInput != 0 && !character.IsBusy)
                stateMachine.ChangeState(character.GetState(EState.Move));
        }
    }
}
