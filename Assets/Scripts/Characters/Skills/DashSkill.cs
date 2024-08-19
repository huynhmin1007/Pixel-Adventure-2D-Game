using UnityEngine;

namespace Assets.Scripts.Characters.Skills
{
    public class DashSkill : Skill
    {
        [SerializeField] private float dashSpeed;
        [SerializeField] private float dashDuration;

        public override void UseSkill()
        {
            base.UseSkill();
        }

        public float DashSpeed { get => dashSpeed; set => dashSpeed = value; }
        public float DashDuration { get => dashDuration; set => dashDuration = value; }
    }
}
