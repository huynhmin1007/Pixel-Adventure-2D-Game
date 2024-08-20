﻿using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Characters.Skills
{
    public class CrystalSkill : Skill
    {
        [SerializeField] private GameObject crystalPrefab;
        [SerializeField] private float crystalDuration;
        private GameObject currentCrystal;

        [Header("Explosive crystal")]
        [SerializeField] private bool canExplode;

        [Header("Moving Crystal")]
        [SerializeField] private bool canMoveToEnemy;
        [SerializeField] private float moveSpeed;

        [Header("Multi stacking crystal")]
        [SerializeField] private bool canUseMultiStacks;
        [SerializeField] private int amountOfStacks;
        [SerializeField] private float multiStackCooldown;
        [SerializeField] private float useTimeWindow;
        [SerializeField] private List<GameObject> crystalLeft = new List<GameObject>();

        public override void UseSkill()
        {
            if (CanUseMultiCrystal())
            {
                return;
            }

            if (currentCrystal == null)
            {
                currentCrystal = Instantiate(crystalPrefab, character.transform.position, Quaternion.identity);
                CrystalSkillController currentCrystalScript = currentCrystal.GetComponent<CrystalSkillController>();

                currentCrystalScript.SetupCrystal(crystalDuration, canExplode, canMoveToEnemy, moveSpeed,
                    FindClosestEnemy(currentCrystal.transform));
            }
            else
            {
                if (canMoveToEnemy)
                    return;

                Vector2 playerPos = character.transform.position;
                character.transform.position = currentCrystal.transform.position;

                currentCrystal.transform.position = playerPos;
                currentCrystal.GetComponent<CrystalSkillController>()?.ExplodeCrystal();
            }
        }

        private bool CanUseMultiCrystal()
        {
            if (canUseMultiStacks)
            {
                if (crystalLeft.Count > 0)
                {
                    if (crystalLeft.Count == amountOfStacks)
                    {
                        Invoke("RefilCrystal", useTimeWindow);
                    }

                    GameObject crystalToSpawn = crystalLeft[crystalLeft.Count - 1];
                    GameObject newCrystal = Instantiate(crystalToSpawn, character.transform.position, Quaternion.identity);

                    crystalLeft.Remove(crystalToSpawn);

                    newCrystal.GetComponent<CrystalSkillController>()
                        .SetupCrystal(crystalDuration, canExplode, canMoveToEnemy, moveSpeed, FindClosestEnemy(newCrystal.transform));

                    if (crystalLeft.Count <= 0)
                    {
                        cooldown = multiStackCooldown;
                        cooldownTimer = cooldown;
                        RefilCrystal();
                    }
                    return true;
                }
            }
            return false;
        }

        private void RefilCrystal()
        {
            int amountToAdd = amountOfStacks - crystalLeft.Count;

            for (int i = 0; i < amountToAdd; i++)
            {
                crystalLeft.Add(crystalPrefab);
            }
        }
    }
}
