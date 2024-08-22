using Assets.Scripts.Characters.Player;
using Assets.Scripts.Common;
using System.Collections;
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

        [SerializeField] private bool createCloneOnDashStart;
        [SerializeField] private bool createCloneOnDashOver;
        [SerializeField] private bool createCloneOnCounterAttack;

        [Header("Clone can duplicate")]
        [SerializeField] private bool canDuplicateClone;
        [SerializeField] private float chanceToDuplicate;

        [Header("Crystal instead of clone")]
        [SerializeField] private bool crystalInsteadOfClone;

        public void UseSkill(Transform clonePosition, Vector3 _offset)
        {
            CreateClone(clonePosition, _offset);

            base.UseSkill();
        }

        public void CreateClone(Transform clonePosition, Vector3 _offset)
        {
            if (crystalInsteadOfClone)
            {
                PlayerManager.instance.player.crystalSkill.CreateCrystal();
                return;
            }

            GameObject newClone = Instantiate(clonePrefab);

            newClone.GetComponent<CloneSkillController>().SetupClone(clonePosition, cloneDuration, canAttack, _offset,
                FindClosestEnemy(clonePosition), canDuplicateClone, chanceToDuplicate);
        }

        public void CreateCloneOnDashStart()
        {
            if (createCloneOnDashStart)
            {
                CreateClone(character.transform, Vector3.zero);
            }
        }

        public void CreateCloneOnDashOver()
        {
            if (createCloneOnDashOver)
            {
                CreateClone(character.transform, Vector3.zero);
            }
        }

        public void CreateCloneOnCounterAttack(Transform _enemyTransform)
        {
            if (createCloneOnCounterAttack)
            {
                StartCoroutine(CreateCloneWithDelay(_enemyTransform.transform, new Vector3(2 * character.Direction.XValue(), 0)));
            }
        }

        private IEnumerator CreateCloneWithDelay(Transform _transform, Vector3 offset)
        {
            yield return new WaitForSeconds(.3f);
            CreateClone(_transform, offset);
        }
    }
}
