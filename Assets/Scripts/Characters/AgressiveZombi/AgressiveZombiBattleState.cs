using Assets.Scripts.Base;
using Assets.Scripts.Characters.Enemy;
using System;

namespace Assets.Scripts.Characters.AgressiveZombi
{
    public class AgressiveZombiBattleState : EnemyBattleState
    {
        private AgressiveZombiCharacter character;

        public AgressiveZombiBattleState(EnemyCharacter characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            this.character = (AgressiveZombiCharacter)characterBase;
        }
    }
}
