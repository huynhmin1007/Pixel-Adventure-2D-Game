using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Base
{
    public abstract class BaseState
    {
        protected Character characterBase;
        protected StateMachine stateMachine;
        protected Enum animBoolName;
        protected float stateTimer;
        protected bool triggerCalled;

        public Enum AnimBoolName { get => animBoolName; set => animBoolName = value; }

        protected BaseState(Character characterBase, StateMachine stateMachine, Enum animBoolName)
        {
            this.characterBase = characterBase;
            this.stateMachine = stateMachine;
            AnimBoolName = animBoolName;
        }

        public virtual void Enter()
        {
            characterBase.animator.SetBool(AnimBoolName.ToString(), true);
            triggerCalled = false;
        }

        public virtual void Update()
        {
            stateTimer -= Time.deltaTime;
        }

        public virtual void Exit()
        {
            characterBase.animator.SetBool(AnimBoolName.ToString(), false);
        }

        public virtual void AnimationFinishTrigger()
        {
            triggerCalled = true;
        }

        public void ChangeAnimation(Enum _animBoolName)
        {
            characterBase.animator.SetBool(AnimBoolName.ToString(), false);
            characterBase.animator.SetBool(_animBoolName.ToString(), true);
            AnimBoolName = _animBoolName;
        }

        public override bool Equals(object obj)
        {
            return obj is BaseState state &&
                   EqualityComparer<Character>.Default.Equals(characterBase, state.characterBase) &&
                   EqualityComparer<Enum>.Default.Equals(animBoolName, state.animBoolName) &&
                   stateTimer == state.stateTimer;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(characterBase, animBoolName, stateTimer);
        }
    }
}
