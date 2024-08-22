using Assets.Scripts.Base;
using Assets.Scripts.Characters.Enemy;
using Assets.Scripts.Characters.Player;
using Assets.Scripts.Common;
using System;
using UnityEngine;

namespace Assets.Scripts.Characters.Acher
{
    public class AcherBattleState : EnemyBattleState
    {
        private AcherCharacter acher;
        private PlayerCharacter player;
        private float moveDir;

        public AcherBattleState(EnemyCharacter characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            acher = (AcherCharacter)characterBase;
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
            acher.ResetVelocity();
            stateTimer = acher.BattleTime;
        }

        public override void Exit()
        {
            base.Exit();
            AnimBoolName = EState.Move;
        }

        public override void Update()
        {
            base.Update();

            if (acher.IsPlayerDetected() && acher.IsPlayerDetected().distance < acher.safeDistance)
            {
                if (CanJump())
                {
                    stateMachine.ChangeState(acher.GetState(EState.Jump));
                    return;
                }
            }

            if (player.XVelocity == 0 && Vector2.Distance(acher.Hitbox.bounds.min, player.transform.position) <= 1)
            {
                if (player.transform.position.x > acher.Hitbox.bounds.min.x)
                    moveDir = -1;
                else if (player.transform.position.x < acher.Hitbox.bounds.min.x)
                    moveDir = 1;

                stateMachine.ChangeAnimation(EState.Move);
                acher.SetVelocity(acher.MoveSpeed * moveDir * 4, 4);

                return;
            }

            if (acher.CanBattle())
            {
                stateTimer = acher.BattleTime;
                return;
            }
            else if (stateTimer < 0
                || Vector2.Distance(player.transform.position, acher.transform.position) > acher.MaxFollowDistance)
            {
                stateMachine.ChangeState(acher.GetState(EState.Idle));
                return;
            }

            if (player.transform.position.x > acher.transform.position.x)
                moveDir = 1;
            else if (player.transform.position.x < acher.transform.position.x)
                moveDir = -1;

            if (acher.IsWallDetected())
            {
                stateMachine.ChangeAnimation(EState.Idle);
            }
            else
                stateMachine.ChangeAnimation(EState.Move);
            acher.SetVelocity(acher.MoveSpeed * moveDir, acher.YVelocity, true);
        }

        private bool CanJump()
        {
            if (Time.time >= acher.lastTimeJumped + acher.jumpCooldown)
            {
                acher.lastTimeJumped = Time.time;
                return true;
            }
            return false;
        }
    }
}
