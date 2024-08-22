using Assets.Scripts.Base.State;
using Assets.Scripts.Characters.Enemy;
using Assets.Scripts.Characters.ObservatoryBoss.Skills;
using Assets.Scripts.Characters.ObservatoryBoss.states;
using Assets.Scripts.Characters.Player;
using Assets.Scripts.Characters.Skeleton;
using Assets.Scripts.Characters.Skills;
using Assets.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace Assets.Scripts.Characters.ObservatoryBoss
{
    public class ObservatoryBossCharacter : EnemyCharacter
    {
        private PlayerCharacter player;
        public FireBallSkill fireBallSkill;
        public LaserSkill laserSkill;

        protected override void Awake()
        {
            base.Awake();

            GameObject skillManagerObject = GameObject.Find("BossSkillManager");

            fireBallSkill = skillManagerObject.GetComponent<FireBallSkill>();
            laserSkill = skillManagerObject.GetComponent<LaserSkill>();
            if( laserSkill != null ) 
                Debug.Log("Laser skill ton tai");
        }

        protected override void Start()
        {
            base.Start();
            player = PlayerManager.instance.player;
        }

        protected override void Update()
        {
            base.Update();
        }

        public override bool CanBattle()
        {
            if (fireBallSkill.CanUseSkill())
            {
                Debug.Log(stateMachine.CurrentState.AnimBoolName);

                stateMachine.ChangeState(states[EObservatoryBossState.IdleBall]);
                return true;
            }
            else if (laserSkill.CanUseSkill()) 
            {
                //Debug.Log(stateMachine.CurrentState.AnimBoolName);
                stateMachine.ChangeState(states[EObservatoryBossState.LaserJumpLoad]);
                return true;
            }
            else if (IsPlayerInAttackRange())
            {
                if (CanPrimaryAttack())
                {
                    stateMachine.ChangeState(states[EState.Attack]);
                }
                else
                    stateMachine.ChangeAnimation(EState.Idle);
                return true;
            }

            MoveToPlayer();
                
            return true;
        }

        private void MoveToPlayer()
        {
            float moveDir = 0;

            if (player.transform.position.x > transform.position.x)
                moveDir = 1;
            else if (player.transform.position.x < transform.position.x)
                moveDir = -1;

            if (IsWallDetected())
            {
                stateMachine.ChangeAnimation(EState.Idle);
            }
            else
                stateMachine.ChangeAnimation(EState.Move);
            SetVelocity(MoveSpeed * moveDir, YVelocity, true);
        }

        protected override void InitializeStates()
        {
            states.Add(EState.Idle, new BossIdleState(this, stateMachine, EState.Idle));
            states.Add(EState.Move, new EnemyMoveState(this, stateMachine, EState.Move));
            states.Add(EState.Fall, new FallState(this, stateMachine, EState.Air));
            states.Add(EState.Battle, new ObservatoryBossBattleState(this, stateMachine, EState.Move));
            states.Add(EState.Attack, new EnemyAttackState(this, stateMachine, EState.Attack, comboWindow));
            states.Add(EState.Stunned, new EnemyStunnedState(this, stateMachine, EState.Stunned));
            states.Add(EObservatoryBossState.IdleBall, new IdleFireBallState(this, stateMachine, EObservatoryBossState.IdleBall));
            states.Add(EObservatoryBossState.FireBall, new FireBallState(this, stateMachine, EObservatoryBossState.FireBall));
            states.Add(EObservatoryBossState.LaserJumpLoad, new LaserJumpLoadState(this, stateMachine, EObservatoryBossState.LaserJumpLoad));
            states.Add(EObservatoryBossState.IdleLaserLoad, new IdleLaserLoadState(this, stateMachine, EObservatoryBossState.IdleLaserLoad));
            states.Add(EObservatoryBossState.Laser, new LaserState(this, stateMachine, EObservatoryBossState.Laser));

        }

        protected override void StateController()
        {
        }
    }
}
