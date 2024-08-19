using System;
using UnityEngine;

namespace Assets.Scripts.Characters.Skills
{
    public class BlackHoleSkill : Skill
    {
        public GameObject blackHolePrefab;
        [SerializeField] private GameObject clonePrefab;

        [Space]
        [SerializeField] private float maxSize;
        [SerializeField] private float growSpeed;
        [SerializeField] private float shrinkSpeed;
        [SerializeField] private float cloneAttackCooldown;
        [SerializeField] private float cloneAttackDuration;

        public event Action OnExitBlackHoleAbility;

        public override void UseSkill()
        {
            state = AbilityState.Active;
            GameObject newBlackHole = Instantiate(blackHolePrefab, character.transform.position,
                Quaternion.identity);

            BlackHoleSkillController blackHoleScript = newBlackHole.GetComponent<BlackHoleSkillController>();
            blackHoleScript.Setup(clonePrefab, maxSize, growSpeed, cloneAttackCooldown, cloneAttackDuration, shrinkSpeed, this);

            base.UseSkill();
        }

        public override void TriggerExitSkill()
        {
            base.TriggerExitSkill();
            OnExitBlackHoleAbility?.Invoke();
        }

        public void Transparent(bool _transparent) => character.Transparent(_transparent);
    }
}
