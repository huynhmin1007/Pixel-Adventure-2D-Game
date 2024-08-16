using Assets.Scripts.Base;
using Assets.Scripts.Base.State;
using Assets.Scripts.Characters.Player;
using Assets.Scripts.Common;
using System;
using UnityEngine;

namespace Assets.Scripts.Characters.Enemy
{
    public class EnemyGroundedState : GroundedState
    {
        private EnemyCharacter character;
        private Transform player;

        public EnemyGroundedState(EnemyCharacter characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            this.character = characterBase;
        }

        public override void Enter()
        {
            base.Enter();
            player = PlayerManager.instance.player.transform;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            /**
             * Nếu phát hiện Player (tia thẳng màu vàng) hoặc Player ở 1 khoảng cách nhất định phía sau lưng
             * -> Vào trạng thái battle
             */
            if (character.IsPlayerDetected()
                 || Vector2.Distance(character.transform.position, player.position) < character.BackPlayerCheckDistance)
            {
                stateMachine.ChangeState(character.GetState(EState.Battle));
                return;
            }
        }
    }
}
