using System;

namespace Assets.Scripts.Base
{
    public class StateMachine
    {
        private BaseState currentState;

        public BaseState CurrentState { get => currentState; set => currentState = value; }

        public void Initialize(BaseState startState)
        {
            CurrentState = startState;
            CurrentState.Enter();
        }

        public void Update()
        {
            CurrentState?.Update();
        }

        public void ChangeState(BaseState newState, bool preventDuplicateState = false)
        {
            if (preventDuplicateState && currentState.AnimBoolName.Equals(newState.AnimBoolName)) return;

            if (newState != null)
            {
                CurrentState?.Exit();
                CurrentState = newState;
                CurrentState.Enter();
            }
        }

        public bool IsAnimation(Enum _animBoolName)
        {
            return CurrentState.AnimBoolName.Equals(_animBoolName);
        }

        public Enum GetCurrentAnimation()
        {
            return CurrentState.AnimBoolName;
        }

        public void ChangeAnimation(Enum _animBoolName)
        {
            if (!IsAnimation(_animBoolName))
                currentState.ChangeAnimation(_animBoolName);
        }
    }
}
