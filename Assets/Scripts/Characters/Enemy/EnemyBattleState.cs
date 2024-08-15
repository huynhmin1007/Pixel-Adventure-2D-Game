using Assets.Scripts.Base;
using Assets.Scripts.Characters.Player;
using Assets.Scripts.Common;
using System;
using UnityEngine;

namespace Assets.Scripts.Characters.Enemy
{
    public class EnemyBattleState : BaseState
    {
        private Transform player;
        private EnemyCharacter character;
        private float moveDir;

        public EnemyBattleState(EnemyCharacter characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            this.character = characterBase;
        }

        public override void Enter()
        {
            base.Enter();
            player = PlayerManager.instance.player.transform;
            character.ResetVelocity();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (player.position.x > character.transform.position.x)
                moveDir = 1;
            else if (player.position.x < character.transform.position.x)
                moveDir = -1;
            
            if (character.IsPlayerDetected())
            {
                
                stateTimer = character.BattleTime;
                character.HandleBattle();
                return;
            }
            else if (stateTimer < 0
                || Vector2.Distance(player.position, character.transform.position) > character.MaxFollowDistance)
                stateMachine.ChangeState(character.GetState(EState.Idle));

            character.SetVelocity(character.MoveSpeed * moveDir, character.YVelocity, true);
        }
    }
}
