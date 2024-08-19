using Assets.Scripts.Characters.Enemy;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Characters.Skills
{
    public class SwordSkillController : MonoBehaviour
    {
        private Animator animator;
        private Rigidbody2D rb;
        private CircleCollider2D cd;
        private Character character;

        private bool canRotate = true;
        private bool isReturning;

        private float freezeTimeDuration;
        private float returnSpeed = 12;

        [Header("Bounce info")]
        private float bounceSpeed;
        private bool isBouncing;
        private int bounceAmount;
        private List<Transform> enemyTarget;
        private int targetIndex;

        [Header("Piecer info")]
        [SerializeField] private int pierceAmount;

        [Header("Spin info")]
        private float maxTravelDistance;
        private float spinDuration;
        private float spinTimer;
        private bool wasStopped;
        private bool isSpinning;

        private float hitTimer;
        private float hitCooldown;

        private float spinDirection;

        private SwordSkill swordSkill;


        private void Awake()
        {
            animator = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody2D>();
            cd = GetComponent<CircleCollider2D>();
        }

        public void SetupSword(Vector2 direction, float gravityScale, Character _character, float _freezeTimeDuration, float _returnSpeed,
            SwordSkill _swordSkill)
        {
            swordSkill = _swordSkill;
            character = _character;
            rb.velocity = direction;
            rb.gravityScale = gravityScale;
            freezeTimeDuration = _freezeTimeDuration;
            returnSpeed = _returnSpeed;

            if (pierceAmount <= 0)
                animator.SetBool("Rotation", true);

            spinDirection = Mathf.Clamp(rb.velocity.x, -1, 1);

            Invoke("Destroy", 7);
        }

        private void Update()
        {
            if (canRotate)
                transform.right = rb.velocity;

            if (isReturning)
            {
                transform.position = Vector2.MoveTowards(transform.position, character.transform.position,
                    returnSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, character.transform.position) < 1)
                {
                    swordSkill.TriggleCatchSword();
                }
            }

            BounceLogic();

            SpinLogic();
        }

        private void SpinLogic()
        {
            if (isSpinning)
            {
                if (Vector2.Distance(character.transform.position, transform.position) > maxTravelDistance && !wasStopped)
                {
                    StopWhenSpinning();
                }

                if (wasStopped)
                {
                    spinTimer -= Time.deltaTime;

                    transform.position = Vector2.MoveTowards(transform.position,
                        new Vector2(transform.position.x + spinDirection, transform.position.y), 1.5f * Time.deltaTime);

                    if (spinTimer < 0)
                    {
                        isReturning = true;
                        isSpinning = false;
                    }

                    hitTimer -= Time.deltaTime;

                    if (hitTimer < 0)
                    {
                        hitTimer = hitCooldown;


                        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);

                        foreach (Collider2D hit in colliders)
                        {
                            EnemyCharacter enemy = hit.GetComponent<EnemyCharacter>();

                            if (enemy != null)
                            {
                                SwordSkillDamage(enemy);
                            }
                        }
                    }
                }
            }
        }

        private void StopWhenSpinning()
        {
            wasStopped = true;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            spinTimer = spinDuration;
        }

        private void BounceLogic()
        {
            if (isBouncing && enemyTarget.Count > 0)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    enemyTarget[targetIndex].position, bounceSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < .1f)
                {
                    SwordSkillDamage(enemyTarget[targetIndex].GetComponent<EnemyCharacter>());

                    targetIndex++;
                    bounceAmount--;

                    if (bounceAmount <= 0)
                    {
                        isBouncing = false;
                        isReturning = true;
                    }

                    if (targetIndex >= enemyTarget.Count)
                        targetIndex = 0;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isReturning) return;

            EnemyCharacter enemy = collision.GetComponent<EnemyCharacter>();

            if (enemy != null)
            {
                SwordSkillDamage(enemy);
                SetupTargetsForBounce(collision, enemy);
            }

            StuckInto(collision);
        }

        private void SwordSkillDamage(EnemyCharacter enemy)
        {
            enemy.Damage();
            enemy.StartCoroutine("FreezeTimeFor", freezeTimeDuration);
        }

        private void SetupTargetsForBounce(Collider2D collision, EnemyCharacter _enemy)
        {
            if (isBouncing && enemyTarget.Count <= 0)
            {
                if (_enemy != null)
                {
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);

                    foreach (Collider2D hit in colliders)
                    {
                        EnemyCharacter enemy = hit.GetComponent<EnemyCharacter>();

                        if (enemy != null)
                        {
                            enemyTarget.Add(enemy.transform);
                        }
                    }
                }
            }
        }

        private void StuckInto(Collider2D collision)
        {
            if (pierceAmount > 0 && collision.GetComponent<EnemyCharacter>() != null)
            {
                pierceAmount--;
                return;
            }

            if (isSpinning)
            {
                StopWhenSpinning();
                return;
            }

            canRotate = false;
            cd.enabled = false;

            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            if (isBouncing && enemyTarget.Count > 0)
                return;

            animator.SetBool("Rotation", false);
            transform.parent = collision.transform;
        }

        public void ReturnSword()
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            transform.parent = null;
            isReturning = true;
        }

        public void SetupBounce(bool _isBouncing, int _amountOfBounce, float _bounceSpeed)
        {
            isBouncing = _isBouncing;
            bounceAmount = _amountOfBounce;
            bounceSpeed = _bounceSpeed;

            enemyTarget = new List<Transform>();
        }

        public void SetupPierce(int _pierceAmount)
        {
            pierceAmount = _pierceAmount;
        }

        public void SetupSpin(bool _isSpinning, float _maxTravelDistance, float _spinDuration, float _hitCooldown)
        {
            isSpinning = _isSpinning;
            maxTravelDistance = _maxTravelDistance;
            spinDuration = _spinDuration;
            hitCooldown = _hitCooldown;
        }

        private void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
