using Assets.Scripts.Base;
using Assets.Scripts.Base.State;
using System;

namespace Assets.Scripts.Characters.Player
{
    public class PlayerBlackHoleState : SkillState
    {
        private float flyTime = .4f;
        private PlayerCharacter character;
        private float defaultGravity;

        public PlayerBlackHoleState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            character = (PlayerCharacter)characterBase;
        }

        public override void Enter()
        {
            base.Enter();

            stateTimer = flyTime;
            defaultGravity = character.Rb.gravityScale;
            character.IsImmune = true;
            character.Rb.gravityScale = 0;
        }

        public override void Exit()
        {
            base.Exit();
            character.IsImmune = false;
            character.Transparent(false);
            character.Rb.gravityScale = defaultGravity;
        }

        public override void Update()
        {
            base.Update();

            if (stateTimer > 0)
            {
                character.SetVelocity(0, 6);
            }
            else
            {
                character.SetVelocity(0, -.3f);

                if (!character.blackHoleSkill.IsActive() && character.blackHoleSkill.CanUseSkill())
                {
                    character.blackHoleSkill.UseSkill();
                }
            }
        }
    }
}
