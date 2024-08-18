using Assets.Scripts.Characters.Player;
using UnityEngine;

namespace Assets.Scripts.Characters.Skills
{
    public abstract class Skill : MonoBehaviour
    {
        [SerializeField] protected float cooldown;
        protected float cooldownTimer;
        protected PlayerCharacter player;

        protected virtual void Start()
        {
            player = PlayerManager.instance.player;
        }

        protected virtual void Update()
        {
            cooldownTimer -= Time.deltaTime;
        }

        public virtual bool CanUseSkill()
        {
            if (cooldownTimer < 0)
            {
                UseSkill();
                cooldownTimer = cooldown;
                return true;
            }
            return false;
        }

        public virtual void UseSkill()
        {

        }
    }
}
