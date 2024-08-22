using Assets.Scripts.Base;
using Assets.Scripts.Base.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Characters.Player
{

    internal class PlayerJumpState : JumpState
    {
        
        public PlayerJumpState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {

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
