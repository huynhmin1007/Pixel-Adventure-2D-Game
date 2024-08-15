using Assets.Scripts.Base;
using Assets.Scripts.Characters.Player;
using Assets.Scripts.Common;
using System;
using UnityEngine;

namespace Assets.Scripts.Characters.Enemy
{
    public class EnemyAttackState : BaseState
    {
        private EnemyCharacter character;
        private Transform player;
        private int comboCounter;
        private int comboWindow;

        public EnemyAttackState(EnemyCharacter characterBase, StateMachine stateMachine, Enum animBoolName, int comboWindow)
            : base(characterBase, stateMachine, animBoolName)
        {
            this.character = characterBase;
            this.comboWindow = comboWindow;
        }

        public override void Enter()
        {
            base.Enter();
            player = PlayerManager.instance.player.transform;

            if (comboCounter > comboWindow || Time.time >= character.LastTimeAttacked + comboWindow)
                comboCounter = 0;

            characterBase.animator.SetInteger("ComboCounter", comboCounter);

            if (player.position.x < character.Direction.XValue())
            {
                character.Flip();
            }

            if (characterBase.AttackMove.Length > 0)
            {
                float xVelocity = !characterBase.IsGrounded() ? characterBase.MoveSpeed * .2f : 1;
                characterBase.SetVelocity(
                characterBase.AttackMove[comboCounter].x * xVelocity, characterBase.AttackMove[comboCounter].y);
            }
            else
            {
                characterBase.SetVelocity(0, character.YVelocity);
            }
        }

        public override void Exit()
        {
            base.Exit();

            comboCounter++;
            character.LastTimeAttacked = Time.time;
        }

        public override void Update()
        {
            base.Update();

            if (triggerCalled)
                stateMachine.ChangeState(characterBase.GetState(EState.Battle));
        }
    }
}
