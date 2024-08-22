using Assets.Scripts.Base;
using Assets.Scripts.Characters.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Characters.ObservatoryBoss
{
    public class ObservatoryBossBattleState : EnemyBattleState
    {
        private ObservatoryBossCharacter character;
        public ObservatoryBossBattleState(EnemyCharacter characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            this.character = (ObservatoryBossCharacter)characterBase;
        }
    }
}
