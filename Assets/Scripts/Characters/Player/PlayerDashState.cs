using Assets.Scripts.Base;
using Assets.Scripts.Base.State;
using System;

namespace Assets.Scripts.Characters.Player
{
    public class PlayerDashState : DashState
    {
        private PlayerCharacter character;

        public PlayerDashState(Character characterBase, StateMachine stateMachine, Enum animBoolName, float dashSpeed, float dashDuration) : base(characterBase, stateMachine, animBoolName, dashSpeed, dashDuration)
        {
            character = (PlayerCharacter)characterBase;
        }

        public override void Enter()
        {
            base.Enter();

            character.skill.clone.CreateClone(characterBase.transform);
        }
    }
}
