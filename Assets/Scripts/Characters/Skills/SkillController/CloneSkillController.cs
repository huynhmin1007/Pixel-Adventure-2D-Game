using Assets.Scripts.Characters.Enemy;
using Assets.Scripts.Characters.Player;
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

    private bool canDuplicateClone;
    private float chanceToDuplicate;
    private int facingDir = 1;

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

    public void SetupClone(Transform newTransform, float cloneDuration, bool canAttack, Vector3 _offset, Transform _closestEnemy,
        bool _canDuplicateClone = false, float _chanceToDuplicate = 0)
    {
        if (canAttack)
            animator.SetInteger("AttackNumber", Random.Range(1, 3));

        transform.position = newTransform.position + _offset;
        cloneTimer = cloneDuration;

        closestEnemy = _closestEnemy;
        chanceToDuplicate = _chanceToDuplicate;
        canDuplicateClone = _canDuplicateClone;
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
                enemy.DamageEffect();
            }
        }

        if (canDuplicateClone)
        {
            if (Random.Range(0, 100) < chanceToDuplicate)
            {
                PlayerManager.instance.player.cloneSkill.CreateClone(colliders[0].transform, new Vector3(.5f * facingDir, 0));
            }
        }
    }

    private void FaceClosestTarget()
    {
        if (closestEnemy != null)
        {
            if (transform.position.x > closestEnemy.position.x)
            {
                facingDir = -1;
                transform.Rotate(0, 180, 0);
            }
        }
    }
}
