using Assets.Scripts.Base;
using System;
using UnityEngine;

namespace Assets.Scripts.Characters.Enemy
{
    public class EnemyDeadState : BaseState
    {
        private EnemyCharacter enemy;

        public EnemyDeadState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            enemy = (EnemyCharacter)characterBase;
        }

        public override void Enter()
        {
            base.Enter();

            enemy.animator.SetBool(enemy.LastAnimBoolName, true);
            enemy.animator.speed = 0;
            enemy.cd.enabled = false;
            enemy.Rb.velocity = new Vector2(0, 10);
        }

        public override void Update()
        {
            base.Update();

            if (enemy.YVelocity == 0)
                enemy.DestroyEnemy();
        }
    }
}
