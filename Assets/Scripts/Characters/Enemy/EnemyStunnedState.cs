using Assets.Scripts.Base;
using Assets.Scripts.Common;
using System;

namespace Assets.Scripts.Characters.Enemy
{
    public class EnemyStunnedState : BaseState
    {
        private EnemyCharacter character;

        public EnemyStunnedState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            this.character = (EnemyCharacter)characterBase;
        }

        public override void Enter()
        {
            base.Enter();
            character.flashFX.InvokeRepeating("RedColorBlink", 0, .1f);

            stateTimer = character.StunDuration;
            character
                .SetVelocity(-character.Direction.XValue() * character.StunDirection.x, character.StunDirection.y);
        }

        public override void Exit()
        {
            base.Exit();

            character.flashFX.Invoke("CancelRedBlink", 0);
        }

        public override void Update()
        {
            base.Update();

            if (stateTimer < 0)
                stateMachine.ChangeState(character.GetState(EState.Idle));
        }
    }
}
