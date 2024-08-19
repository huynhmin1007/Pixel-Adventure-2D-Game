using Assets.Scripts.Characters.Enemy;
using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator animator;

    [SerializeField] private float colorLoosingSpeed;
    private float cloneTimer;

    [SerializeField] private Transform attackCheck;
    [SerializeField] private float attackCheckRadius = .8f;
    private Transform closestEnemy;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        cloneTimer -= Time.deltaTime;

        if (cloneTimer < 0)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - (Time.deltaTime * colorLoosingSpeed));

            if (sr.color.a <= 0)
                Destroy(gameObject);
        }
    }

    public void SetupClone(Transform newTransform, float cloneDuration, bool canAttack, Vector3 _offset)
    {
        if (canAttack)
            animator.SetInteger("AttackNumber", Random.Range(1, 3));

        transform.position = newTransform.position + _offset;
        cloneTimer = cloneDuration;

        FaceClosestTarget();
    }

    private void AnimationTrigger()
    {
        cloneTimer = -.1f;
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackCheck.position, attackCheckRadius);

        foreach (Collider2D hit in colliders)
        {
            EnemyCharacter enemy = hit.GetComponent<EnemyCharacter>();

            if (enemy != null)
            {
                enemy.Damage();
            }
        }
    }

    private void FaceClosestTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);

        float closestDistance = Mathf.Infinity;

        foreach (Collider2D hit in colliders)
        {
            EnemyCharacter enemy = hit.GetComponent<EnemyCharacter>();

            if (enemy != null)
            {
                float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                if (distanceToEnemy < closestDistance)
                {
                    closestDistance = distanceToEnemy;
                    closestEnemy = hit.transform;
                }
            }
        }

        if (closestEnemy != null)
        {
            if (transform.position.x > closestEnemy.position.x)
            {
                transform.Rotate(0, 180, 0);
            }
        }
    }
}
