using Assets.Scripts.Base;
using Assets.Scripts.Characters.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Characters.AnchorBoy
{
    internal class AnchorBoyBattleState : EnemyBattleState
    {

        private AnChorBoyCharacter character;
        public AnchorBoyBattleState(EnemyCharacter characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            this.character = (AnChorBoyCharacter)characterBase;
        }
    }
}
