using Assets.Scripts.Base;
using Assets.Scripts.Characters.Player;
using Assets.Scripts.Common;
using System;
using UnityEngine;

namespace Assets.Scripts.Characters.Enemy
{
    public class EnemyBattleState : BaseState
    {
        private PlayerCharacter player;
        private EnemyCharacter character;
        private float moveDir;

        public EnemyBattleState(EnemyCharacter characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            this.character = characterBase;
        }

        public override void Enter()
        {
            base.Enter();
            /**
             * Character = enemy
             * Đặt lại vận tốc của enemy 
             * stateTimer (thời gian tối đa enemy đuổi theo player (tránh di chuyển vào gốc tường xong bị kẹt))
             */
            player = PlayerManager.instance.player;
            character.ResetVelocity();
            stateTimer = character.BattleTime;
        }

        public override void Exit()
        {
            base.Exit();
            AnimBoolName = EState.Move;
        }

        public override void Update()
        {
            base.Update();

            if (player.XVelocity == 0 && Vector2.Distance(character.Hitbox.bounds.min, player.transform.position) <= 1)
            {
                if (player.transform.position.x > character.Hitbox.bounds.min.x)
                    moveDir = -1;
                else if (player.transform.position.x < character.Hitbox.bounds.min.x)
                    moveDir = 1;

                stateMachine.ChangeAnimation(EState.Move);
                character.SetVelocity(character.MoveSpeed * moveDir * 4, 4);

                return;
            }

            /**
             * Nếu enemy có thể batttle (tự cài đặt logic riêng với mỗi enemy)
             * Cài lại thời gian theo đuổi 
             */
            if (character.CanBattle())
            {
                stateTimer = character.BattleTime;
                return;
            }
            /**
             * Nếu đuổi theo quá lâu hoặc player đi quá xa
             * -> Ngừng theo đuổi
             */
            else if (stateTimer < 0
                || Vector2.Distance(player.transform.position, character.transform.position) > character.MaxFollowDistance)
            {
                stateMachine.ChangeState(character.GetState(EState.Idle));
                return;
            }

            /**
             * Nếu player phía sau enemy thì đi về sau lưng và ngược lại
             */
            if (player.transform.position.x > character.transform.position.x)
                moveDir = 1;
            else if (player.transform.position.x < character.transform.position.x)
                moveDir = -1;

            /**
             * Đổi sang Animation Move và di chuyển về phía player
             */

            if (character.IsWallDetected())
            {
                stateMachine.ChangeAnimation(EState.Idle);
            }
            else
                stateMachine.ChangeAnimation(EState.Move);
            character.SetVelocity(character.MoveSpeed * moveDir, character.YVelocity, true);
        }
    }
}
