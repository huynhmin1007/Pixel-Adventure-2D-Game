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

            if (character.IsPlayerDetected()
                || Vector2.Distance(character.transform.position, player.position) < character.BackPlayerCheckDistance)
            {
                if (character.CanBattle())
                    stateMachine.ChangeState(character.GetState(EState.Battle));
                else if (!character.IsPlayerInAttackRange())
                {
                    if (player.position.x > character.transform.position.x)
                        character.XInput = 1;
                    else if (player.position.x < character.transform.position.x)
                        character.XInput = -1;
                }
            }
        }
    }
}
