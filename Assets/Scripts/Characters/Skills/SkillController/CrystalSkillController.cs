using Assets.Scripts.Characters.Enemy;
using UnityEngine;

public class CrystalSkillController : MonoBehaviour
{
    private Animator animator => GetComponent<Animator>();
    private CircleCollider2D cd => GetComponent<CircleCollider2D>();

    private float crystalExisTimer;
    private bool canExplode;

    private bool canMoveToEnemy;
    private float moveSpeed;

    private bool canGrow;
    private float growSpeed = 5;

    private Transform closestTarget;

    public void SetupCrystal(float _crystalDuration, bool _canExplode, bool _canMoveToEnemy, float _moveSpeed, Transform _closestTarget)
    {
        crystalExisTimer = _crystalDuration;
        canExplode = _canExplode;
        canMoveToEnemy = _canMoveToEnemy;
        moveSpeed = _moveSpeed;
        closestTarget = _closestTarget;
    }

    private void Update()
    {
        crystalExisTimer -= Time.deltaTime;

        if (crystalExisTimer < 0)
        {
            ExplodeCrystal();
        }

        if (canMoveToEnemy)
        {
            transform.position = Vector2.MoveTowards(transform.position, closestTarget.position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, closestTarget.position) < 1)
            {
                ExplodeCrystal();
                canMoveToEnemy = false;
            }
        }

        if (canGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(3, 3), growSpeed * Time.deltaTime);
        }
    }

    public void ExplodeCrystal()
    {
        if (canExplode)
        {
            canGrow = true;
            animator.SetTrigger("Explode");
        }
        else
            SelfDestroy();
    }

    public void SelfDestroy() => Destroy(gameObject);

    private void AnimationExplodeEvent()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cd.radius);

        foreach (Collider2D hit in colliders)
        {
            EnemyCharacter enemy = hit.GetComponent<EnemyCharacter>();

            if (enemy != null)
            {
                enemy.Damage();
            }
        }
    }
}
