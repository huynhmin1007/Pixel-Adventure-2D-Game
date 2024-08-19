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
            if (cooldownTimer >= 0)
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
            return state == AbilityState.Ready;
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
    }
}
