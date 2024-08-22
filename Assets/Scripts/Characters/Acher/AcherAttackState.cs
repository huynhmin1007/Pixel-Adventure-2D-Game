using Assets.Scripts.Base;
using Assets.Scripts.Characters.Enemy;
using System;

namespace Assets.Scripts.Characters.Acher
{
    public class AcherAttackState : EnemyAttackState
    {
        private AcherCharacter acher;

        public AcherAttackState(EnemyCharacter characterBase, StateMachine stateMachine, Enum animBoolName, int comboWindow) : base(characterBase, stateMachine, animBoolName, comboWindow)
        {
            acher = (AcherCharacter)characterBase;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();


        }
    }
}
