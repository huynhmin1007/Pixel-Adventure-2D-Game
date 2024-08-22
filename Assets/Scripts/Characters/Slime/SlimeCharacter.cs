using Assets.Scripts.Base.State;
using Assets.Scripts.Characters.Enemy;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Characters.Slime
{
    public enum SlimeType
    {
        big, medium, small
    }

    public class SlimeCharacter : EnemyCharacter
    {
        [Header("Slime spesific")]
        [SerializeField] private SlimeType slimeType;
        [SerializeField] private int slimesToCreate;
        [SerializeField] private GameObject slimePrefab;
        [SerializeField] private Vector2 minCreationVelocity;
        [SerializeField] private Vector2 maxCreationVelocity;
        private int count;

        public override bool CanBattle()
        {
            bool isPlayerDetected = IsPlayerDetected();

            if (isPlayerDetected)
            {
                if (IsPlayerInAttackRange())
                {
                    if (CanPrimaryAttack())
                    {
                        stateMachine.ChangeState(states[EState.Attack]);
                    }
                    else
                        stateMachine.ChangeAnimation(EState.Idle);
                    return true;
                }
                else
                {
                    stateMachine.ChangeAnimation(EState.Move);
                    SetVelocity(Direction.XValue() * MoveSpeed, YVelocity);
                }
            }
            return isPlayerDetected;
        }

        protected override void InitializeStates()
        {
            states.Add(EState.Idle, new EnemyIdleState(this, stateMachine, EState.Idle));
            states.Add(EState.Move, new EnemyMoveState(this, stateMachine, EState.Move));
            states.Add(EState.Fall, new FallState(this, stateMachine, EState.Air));
            states.Add(EState.Battle, new EnemyBattleState(this, stateMachine, EState.Move));
            states.Add(EState.Attack, new EnemyAttackState(this, stateMachine, EState.Attack, comboWindow));
            states.Add(EState.Stunned, new SlimeStunnedState(this, stateMachine, EState.Stunned));
            states.Add(EState.Dead, new EnemyDeadState(this, stateMachine, EState.Idle));
        }

        protected override void StateController()
        {

        }

        public override void Stun()
        {
            base.Stun();
            stateMachine.ChangeState(states[EState.Stunned]);
        }

        private void CreateSlimes(int _amountOfSlimes, GameObject _slimePrefab)
        {
            for (int i = 0; i < _amountOfSlimes; i++)
            {
                GameObject newSlime = Instantiate(_slimePrefab, transform.position, Quaternion.identity);

                newSlime.GetComponent<SlimeCharacter>().SetupSlime(Direction.XValue());
                count++;
            }
        }

        public override void Dead()
        {
            base.Dead();

            if (slimeType == SlimeType.small)
                return;

            if (count >= slimesToCreate) return;

            CreateSlimes(slimesToCreate, slimePrefab);
        }

        public void SetupSlime(int dir)
        {
            if (dir != Direction.XValue())
            {
                Flip();
            }

            float xVelocity = Random.Range(minCreationVelocity.x, maxCreationVelocity.x);
            float yVelocity = Random.Range(minCreationVelocity.y, maxCreationVelocity.y);

            isKnocked = true;

            GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity * -Direction.XValue(), yVelocity);

            Invoke("CancelKnockBack", 1.5f);
        }

        private void CancelKnockBack() => isKnocked = false;
    }
}
