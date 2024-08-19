using System;

namespace Assets.Scripts.Base.State
{
    public class SkillState : BaseState
    {
        protected bool isCasting;

        public SkillState(Character characterBase, StateMachine stateMachine, Enum animBoolName) : base(characterBase, stateMachine, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            IsCasting = true;
        }

        public override void Exit()
        {
            base.Exit();
            IsCasting = false;
        }

        public override void Update()
        {
            base.Update();
        }

        public bool IsCasting { get => isCasting; set => isCasting = value; }
    }
}
