using Assets.Scripts.Base.State;
using Assets.Scripts.Characters.Enemy;
using Assets.Scripts.Characters.Skeleton;
using Assets.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Characters.HeroTracker
{
    public class HeroTrackerCharacter : EnemyCharacter
    {
        protected override void InitializeStates()
        {
            states.Add(EState.Idle, new EnemyIdleState(this, stateMachine, EState.Idle));
            states.Add(EState.Move, new EnemyMoveState(this, stateMachine, EState.Move));
            states.Add(EState.Fall, new FallState(this, stateMachine, EState.Air));
            states.Add(EState.Battle, new HeroTrackerBattleState(this, stateMachine, EState.Move));
            states.Add(EState.Attack, new EnemyAttackState(this, stateMachine, EState.Attack, comboWindow));
            states.Add(EState.Stunned, new EnemyStunnedState(this, stateMachine, EState.Stunned));
        }

        protected override void StateController()
        {
            Debug.Log(stateMachine.CurrentState.AnimBoolName);
        }

        public override void Stun()
        {
            base.Stun();
            stateMachine.ChangeState(states[EState.Stunned]);
        }

        public override bool CanBattle()
        {
            /**
             * Kiểm tra có phát hiện Player không (tia thẳng màu vàng)
             */
            bool isPlayerDetected = IsPlayerDetected();

            if (isPlayerDetected)
            {
                /**
                 * Nếu player trong phạm vi tấn công của enemy
                 */
                if (IsPlayerInAttackRange())
                {
                    /**
                     * Nếu có thể tấn công bình thường
                     * (Check dựa vào cooldown tránh enemy tấn công liên tục không nghỉ)
                     */
                    if (CanPrimaryAttack())
                    {
                        /**
                         * Chuyển sang trạng thái đánh thường
                         */
                        stateMachine.ChangeState(states[EState.Attack]);
                    }
                    /**
                     * Nếu ko thể tấn công bình thường và player đang trong phạm vi tấn công của enemy
                     * Enemy dùng Animation Idle, đứng thở chờ tới khi được đánh tiếp
                     */
                    else
                        stateMachine.ChangeAnimation(EState.Idle);
                    /**
                     * Trả về true (vẫn có thể tiếp tục battle, theo đuổi player)
                     * (Check lại EnemyBattleState coi logic của CanBattle() khi trả về true - false)
                     */
                    return true;
                }
                /**
                 * Nếu player ko trong phạm vi tấn công của enemy
                 * Di chuyển đến chỗ player
                 */
                else
                {
                    stateMachine.ChangeAnimation(EState.Move);
                    SetVelocity(Direction.XValue() * MoveSpeed, YVelocity);
                }
            }

            /**
             * Trả về giá trị có phát hiện player không để quyết định nên theo đuổi tiếp hay không
             */
            return isPlayerDetected;
        }
    }
}
