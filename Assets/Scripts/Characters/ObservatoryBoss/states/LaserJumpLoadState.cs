using Assets.Scripts.Base;
using Assets.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Characters.ObservatoryBoss.states
{
    public class LaserJumpLoadState : ObservatoryBossBaseState
    {
        public LaserJumpLoadState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
        }

        public override void Update()
        {
            base.Update();
            if (triggerCalled)
            {
                // chuyen state
                stateMachine.ChangeState(boss.GetState(EObservatoryBossState.Laser));
            }
        }
    }
}
