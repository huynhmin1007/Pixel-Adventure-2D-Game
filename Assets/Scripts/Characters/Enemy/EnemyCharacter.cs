using Assets.Scripts.Base.UI;
using Assets.Scripts.Common;
using UnityEngine;

namespace Assets.Scripts.Characters.Enemy
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CapsuleCollider2D))]
    [RequireComponent(typeof(EnemyStats))]
    [RequireComponent(typeof(CharacterFlashFX))]
    //[RequireComponent(typeof(ItemDrop))]
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
        protected string lastAnimBoolName;

        [HideInInspector] public CapsuleCollider2D cd { get; private set; }

        public float IdleTime { get => idleTime; set => idleTime = value; }
        protected override void Start()
        {
            base.Start();
            cd = GetComponent<CapsuleCollider2D>();
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

        public override void DamageEffect()
        {
            base.DamageEffect();
        }

        public override void Flip()
        {
            base.Flip();
        }

        public abstract bool CanBattle();

        public virtual void AnimationSpecialAttackTrigger()
        {

        }

        public float PlayerCheckDistance { get => playerCheckDistance; set => playerCheckDistance = value; }
        public float LastTimeAttacked { get => lastTimeAttacked; set => lastTimeAttacked = value; }
        public float AttackCooldown { get => attackCooldown; set => attackCooldown = value; }
        public float BattleTime { get => battleTime; set => battleTime = value; }
        public float MaxFollowDistance { get => maxFollowDistance; set => maxFollowDistance = value; }
        public float BackPlayerCheckDistance { get => backPlayerCheckDistance; set => backPlayerCheckDistance = value; }
        public float StunDuration { get => stunDuration; set => stunDuration = value; }
        public Vector2 StunDirection { get => stunDirection; set => stunDirection = value; }
        public bool CanBeStunned { get => canBeStunned; set => canBeStunned = value; }
        public string LastAnimBoolName { get => lastAnimBoolName; set => lastAnimBoolName = value; }

        public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(transform.position,
           Direction.ToVector2(), PlayerCheckDistance, playerLayer);

        public virtual Collider2D IsPlayerInAttackRange()
            => Physics2D.OverlapBox(Hitbox.bounds.center, Hitbox.bounds.size, 0, playerLayer);

        public virtual void AssignLastAnimBoolName(string _animBoolName) => lastAnimBoolName = _animBoolName;

        public override void Dead()
        {
            base.Dead();

            stateMachine.ChangeState(states[EState.Dead]);
        }

        public void DestroyEnemy()
        {
            Destroy(gameObject);
        }
    }
}