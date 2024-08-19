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

        public void UseSkill(Transform clonePosition, Vector3 _offset)
        {
            GameObject newClone = Instantiate(clonePrefab);

            newClone.GetComponent<CloneSkillController>().SetupClone(clonePosition, cloneDuration, canAttack, _offset);

            base.UseSkill();
        }
    }
}
