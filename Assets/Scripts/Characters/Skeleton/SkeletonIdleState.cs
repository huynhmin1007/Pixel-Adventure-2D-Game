using Assets.Scripts.Base;
using Assets.Scripts.Characters.Enemy;
using System;

namespace Assets.Scripts.Characters.Skeleton
{
    public class SkeletonIdleState : EnemyIdleState
    {
        private SkeletonCharacter character;

        public SkeletonIdleState(SkeletonCharacter characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            this.character = characterBase;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
