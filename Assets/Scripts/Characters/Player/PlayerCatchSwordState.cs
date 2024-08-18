using Assets.Scripts.Base;
using Assets.Scripts.Common;
using System;
using UnityEngine;

namespace Assets.Scripts.Characters.Player
{
    public class PlayerCatchSwordState : BaseState
    {
        private Transform sword;
        private PlayerCharacter character;

        public PlayerCatchSwordState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            this.character = (PlayerCharacter)characterBase;
        }

        public override void Enter()
        {
            base.Enter();

            sword = character.sword.transform;

            if (character.transform.position.x > sword.position.x && character.Direction == Direction.RIGHT)
            {
                character.Flip();
            }
            else if (character.transform.position.x < sword.position.x && character.Direction == Direction.LEFT)
            {
                character.Flip();
            }

            character.SetVelocity(character.SwordReturnImpact * -character.Direction.XValue(), character.YVelocity);
        }

        public override void Update()
        {
            base.Update();

            if (triggerCalled)
                stateMachine.ChangeState(character.GetState(EState.Idle));
        }
    }
}
