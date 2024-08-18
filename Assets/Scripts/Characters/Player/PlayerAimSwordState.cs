using Assets.Scripts.Base;
using Assets.Scripts.Common;
using System;
using UnityEngine;

namespace Assets.Scripts.Characters.Player
{
    public class PlayerAimSwordState : BaseState
    {
        private PlayerCharacter character;

        public PlayerAimSwordState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            character = (PlayerCharacter)characterBase;
        }

        public override void Enter()
        {
            base.Enter();
            character.ResetVelocity();
            character.skill.sword.DotsActive(true);
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (Input.GetKeyUp(KeyCode.Tab))
            {
                stateMachine.ChangeState(characterBase.GetState(EState.Idle));
            }

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (character.transform.position.x > mousePosition.x && character.Direction == Direction.RIGHT)
            {
                character.Flip();
            }
            else if (character.transform.position.x < mousePosition.x && character.Direction == Direction.LEFT)
            {
                character.Flip();
            }
        }
    }
}
