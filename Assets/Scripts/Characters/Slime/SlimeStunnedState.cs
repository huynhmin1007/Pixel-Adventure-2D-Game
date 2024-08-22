using Assets.Scripts.Base;
using Assets.Scripts.Characters.Enemy;
using System;

namespace Assets.Scripts.Characters.Slime
{
    public class SlimeStunnedState : EnemyStunnedState
    {
        private SlimeCharacter slime;
        public SlimeStunnedState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
            slime = (SlimeCharacter)characterBase;
        }

        public override void Update()
        {
            if (slime.YVelocity < .1f && slime.IsGrounded())
            {
                slime.flashFX.Invoke("CancelRedBlink", 0);
                slime.animator.SetTrigger("StunFold");
                //slime.stats.MakeInvincible(true);
            }

            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
            //slime.stats.MakeInvincible(false);
        }
    }
}
