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
        private float lastTimeSignleAttacked;

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

            if (comboCounter > comboWindow || Time.time >= lastTimeSignleAttacked + comboWindow)
            {
                comboCounter = 0;
            }

            characterBase.animator.SetInteger("ComboCounter", comboCounter);

            if ((player.position.x < character.transform.position.x && character.Direction == Direction.RIGHT)
                || (player.position.x > character.transform.position.x && character.Direction == Direction.LEFT))
            {
                character.Flip();
            }

            if (comboCounter < characterBase.AttackMove.Length)
            {
                float xVelocity = !characterBase.IsGrounded() ? characterBase.MoveSpeed * .2f : 1;
                characterBase.SetVelocity(
                characterBase.AttackMove[comboCounter].x * xVelocity * character.Direction.XValue(), characterBase.AttackMove[comboCounter].y);
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
            float time = Time.time;

            lastTimeSignleAttacked = time;

            if (comboCounter > comboWindow || time >= lastTimeSignleAttacked + comboWindow)
            {
                character.LastTimeAttacked = time;
            }
        }

        public override void Update()
        {
            base.Update();

            if (triggerCalled)
                stateMachine.ChangeState(characterBase.GetState(EState.Battle));
        }
    }
}
