using Assets.Scripts.Base;
using Assets.Scripts.Base.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Characters.Player
{
    public class PlayerFallState : FallState
    {
        public PlayerCharacter player;
        public PlayerFallState(PlayerCharacter characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            this.player = characterBase;
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
