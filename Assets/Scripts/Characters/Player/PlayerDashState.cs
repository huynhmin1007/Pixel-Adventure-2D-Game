using Assets.Scripts.Base;
using Assets.Scripts.Base.State;
using System;
using UnityEngine;

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

            character.IsImmune = true;
            character.cloneSkill.UseSkill(characterBase.transform, Vector3.zero);
        }

        public override void Exit()
        {
            base.Exit();
            character.dashSkill.TriggerExitSkill();
            character.IsImmune = false;
        }
    }
}
