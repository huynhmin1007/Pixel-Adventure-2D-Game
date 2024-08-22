using Assets.Scripts.Base;
using Assets.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Characters.ObservatoryBoss.states
{
    public class IdleLaserLoadState : ObservatoryBossBaseState
    {
        public IdleLaserLoadState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {

        }

        public override void Update()
        {
            base.Update();
            if (triggerCalled) { 
                stateMachine.ChangeState(boss.GetState(EObservatoryBossState.LaserJumpLoad));
            }
        }
    }
}
