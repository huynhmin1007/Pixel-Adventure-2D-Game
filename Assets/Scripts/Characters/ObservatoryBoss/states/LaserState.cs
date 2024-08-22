using Assets.Scripts.Base;
using Assets.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Characters.ObservatoryBoss.states
{
    public class LaserState : ObservatoryBossBaseState
    {
        public LaserState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
        }

        public override void Update()
        {
            base.Update();
            stateMachine.ChangeState(boss.GetState(EState.Idle));
        }
    }
}
