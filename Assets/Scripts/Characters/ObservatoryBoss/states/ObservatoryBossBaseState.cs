using Assets.Scripts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Characters.ObservatoryBoss.states
{
    public class ObservatoryBossBaseState : BaseState
    {
        protected ObservatoryBossCharacter boss;

        public ObservatoryBossBaseState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            boss = (ObservatoryBossCharacter)characterBase;
        }
    }
}
