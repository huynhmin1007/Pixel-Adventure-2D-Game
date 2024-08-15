using Assets.Scripts.Base;
using Assets.Scripts.Characters.Enemy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Characters.HeroTracker
{
    public class HeroTrackerBattleState : EnemyBattleState
    {

        private HeroTrackerCharacter character;
        public HeroTrackerBattleState(EnemyCharacter characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            this.character = (HeroTrackerCharacter)characterBase;
        }


    }
}
