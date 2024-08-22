using Assets.Scripts.Characters.Enemy;
using UnityEngine;

namespace Assets.Scripts.Characters.Skills
{
    public enum AbilityState
    {
        Ready, Active, Cooldown
    }

    public abstract class Skill : MonoBehaviour
    {
        [SerializeField] protected float cooldown;
        protected float cooldownTimer;
        protected Character character;

        public AbilityState state;

        protected virtual void Start()
        {
            GameObject skillManagerObject = GameObject.Find("SkillManager");

            character = skillManagerObject.GetComponentInParent<Character>();
        }

        protected virtual void Update()
        {
            if (cooldownTimer >= 0 && !IsActive())
            {
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer < 0)
                {
                    state = AbilityState.Ready;
                }
            }
        }

        public virtual bool CanUseSkill()
        {
            return state == AbilityState.Ready && cooldownTimer <= 0;
        }

        public virtual void UseSkill()
        {
            state = AbilityState.Active;
        }

        public bool IsCooldown() => state == AbilityState.Cooldown;

        public bool IsActive() => state == AbilityState.Active;
        public virtual void TriggerExitSkill()
        {
            state = AbilityState.Cooldown;
            cooldownTimer = cooldown;
        }

        protected virtual Transform FindClosestEnemy(Transform _checkTransform, float radius = 25f)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_checkTransform.position, radius);

            float closestDistance = Mathf.Infinity;
            Transform closestEnemy = null;
            ;
            foreach (Collider2D hit in colliders)
            {
                EnemyCharacter enemy = hit.GetComponent<EnemyCharacter>();

                if (enemy != null)
                {
                    float distanceToEnemy = Vector2.Distance(_checkTransform.position, hit.transform.position);

                    if (distanceToEnemy < closestDistance)
                    {

                        closestDistance = distanceToEnemy;
                        closestEnemy = hit.transform;
                    }
                }
            }

            return closestEnemy;
        }
    }
}
