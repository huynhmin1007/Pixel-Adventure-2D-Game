using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Characters.Enemy
{
    public abstract class EnemyCharacter : Character
    {
        [Header("Attack")]
        [SerializeField] protected float playerCheckDistance;
        [SerializeField] private float backPlayerCheckDistance;
        [SerializeField] protected float attackCooldown;
        [SerializeField] protected float battleTime;
        [SerializeField] protected float maxFollowDistance;
        [SerializeField] protected LayerMask playerLayer;
        protected float lastTimeAttacked;

        [Header("Auto Movement")]
        [SerializeField] protected float idleTime = 1f;

        [Header("Stunned")]
        [SerializeField] protected float stunDuration;
        [SerializeField] protected Vector2 stunDirection;
        protected bool canBeStunned;
        [SerializeField] protected GameObject warningImage;

        public float IdleTime { get => idleTime; set => idleTime = value; }
        protected override void Start()
        {
            base.Start();
        }

        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position,
                transform.position + (Vector3)Direction.ToVector2() * PlayerCheckDistance);
        }

        public virtual void OpenCounterAttackWindow()
        {
            CanBeStunned = true;
        }

        public virtual void CloseCounterAttackWindow()
        {
            CanBeStunned = false;
            warningImage.SetActive(false);
        }

        public virtual void OpenWarningAttackWindow()
        {
            warningImage.SetActive(true);
        }


        public virtual void Stun()
        {
            CloseCounterAttackWindow();
        }

        public bool CanPrimaryAttack()
        {
            if (Time.time >= LastTimeAttacked + AttackCooldown)
            {
                return true;
            }
            return false;
        }

        public override void Damage()
        {
            base.Damage();
        }

        public abstract bool CanBattle();

        public float PlayerCheckDistance { get => playerCheckDistance; set => playerCheckDistance = value; }
        public float LastTimeAttacked { get => lastTimeAttacked; set => lastTimeAttacked = value; }
        public float AttackCooldown { get => attackCooldown; set => attackCooldown = value; }
        public float BattleTime { get => battleTime; set => battleTime = value; }
        public float MaxFollowDistance { get => maxFollowDistance; set => maxFollowDistance = value; }
        public float BackPlayerCheckDistance { get => backPlayerCheckDistance; set => backPlayerCheckDistance = value; }
        public float StunDuration { get => stunDuration; set => stunDuration = value; }
        public Vector2 StunDirection { get => stunDirection; set => stunDirection = value; }
        public bool CanBeStunned { get => canBeStunned; set => canBeStunned = value; }

        public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(transform.position,
           Direction.ToVector2(), PlayerCheckDistance, playerLayer);

        public virtual Collider2D IsPlayerInAttackRange()
            => Physics2D.OverlapBox(Hitbox.bounds.center, Hitbox.bounds.size, 0, playerLayer);
    }
}