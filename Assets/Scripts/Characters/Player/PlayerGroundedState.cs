using Assets.Scripts.Base;
using Assets.Scripts.Base.State;
using System;

namespace Assets.Scripts.Characters.Player
{
    public class PlayerGroundedState : GroundedState
    {
        public PlayerGroundedState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
        }
    }
}
