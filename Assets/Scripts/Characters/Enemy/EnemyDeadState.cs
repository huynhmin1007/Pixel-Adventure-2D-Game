using Assets.Scripts.Base;
using System;

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

            stateTimer = .15f;
        }

        public override void Update()
        {
            base.Update();

            if (stateTimer > 0)
                enemy.SetVelocity(0, 10);
        }
    }
}
