using UnityEngine;

namespace Assets.Scripts.Characters.Skills
{
    public class CloneSkill : Skill
    {
        [Header("Clone")]
        [SerializeField] private GameObject clonePrefab;
        [SerializeField] private float cloneDuration;

        [Space]
        [SerializeField] private bool canAttack;

        public override bool CanUseSkill()
        {
            return base.CanUseSkill();
        }

        public override void UseSkill()
        {
            base.UseSkill();
        }

        protected override void Update()
        {
            base.Update();
        }

        public void CreateClone(Transform clonePosition)
        {
            GameObject newClone = Instantiate(clonePrefab);

            newClone.GetComponent<CloneSkillController>().SetupClone(clonePosition, cloneDuration, canAttack);
        }
    }
}
