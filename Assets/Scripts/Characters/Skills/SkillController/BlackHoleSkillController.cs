using Assets.Scripts.Characters.Enemy;
using Assets.Scripts.Characters.Skills;
using System.Collections.Generic;
using UnityEngine;

public class BlackHoleSkillController : MonoBehaviour
{
    private float maxSize;
    private float growSpeed;
    private float shrinkSpeed;
    private bool isShrink;

    private bool cloneAttackRelease;
    private float cloneAttackCooldown;
    private float cloneAttackDeltaTime;
    private float cloneAttackDuration;
    private float cloneAttackTimer;

    private List<Transform> targets = new List<Transform>();
    private GameObject clonePrefab;

    private BlackHoleSkill blackHoleSkill;

    private void Update()
    {
        cloneAttackDeltaTime -= Time.deltaTime;

        if (isShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale,
               new Vector2(-1, -1), shrinkSpeed * Time.deltaTime);

            if (transform.localScale.x <= 0)
            {
                blackHoleSkill.TriggerExitSkill();
                Destroy(gameObject);
            }

            return;
        }
        else if (cloneAttackRelease)
        {
            cloneAttackTimer -= Time.deltaTime;

            if (cloneAttackDeltaTime < 0)
                CloneAttackLogic();
        }
        else if (IsMaxSize())
        {
            blackHoleSkill.Transparent(true);

            if (targets.Count == 0)
            {
                FinishBlackHoleAbility();
                return;
            }

            cloneAttackTimer = cloneAttackDuration;
            cloneAttackRelease = true;

            return;
        }

        transform.localScale = Vector2.Lerp(transform.localScale,
                new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
    }

    private void CloneAttackLogic()
    {
        cloneAttackDeltaTime = cloneAttackCooldown;

        int randomIndex = Random.Range(0, targets.Count);
        float xOffset = Random.Range(0, 100) > 50 ? 2 : -2;

        GameObject newClone = Instantiate(clonePrefab);

        newClone.GetComponent<CloneSkillController>().SetupClone(
            targets[randomIndex].transform, 1.5f, true, new Vector3(xOffset, 0), targets[randomIndex].transform);

        if (cloneAttackTimer <= 0)
        {
            FinishBlackHoleAbility();
        }
    }

    private void FinishBlackHoleAbility()
    {

        cloneAttackRelease = false;
        isShrink = true;
    }

    private bool IsMaxSize()
    {
        return Mathf.Abs(maxSize - transform.localScale.x) <= 1f
            && Mathf.Abs(maxSize - transform.localScale.y) <= 1f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!IsMaxSize())
        {
            AddTarget(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) => collision.GetComponent<EnemyCharacter>()?.FrozenTime(false);

    private void AddTarget(Collider2D collision)
    {
        EnemyCharacter enemy = collision.GetComponent<EnemyCharacter>();

        if (enemy != null)
        {
            enemy.FrozenTime(true);

            targets.Add(enemy.transform);
        }
    }

    public void Setup(GameObject _clonePrefab, float _maxSize, float _growSpeed, float _cloneAttackCooldown,
        float _cloneAttackDuration, float _shrinkSpeed, BlackHoleSkill _blackHoleSkill)
    {
        clonePrefab = _clonePrefab;
        maxSize = _maxSize;
        growSpeed = _growSpeed;
        cloneAttackCooldown = _cloneAttackCooldown;
        cloneAttackDuration = _cloneAttackDuration;
        shrinkSpeed = _shrinkSpeed;
        blackHoleSkill = _blackHoleSkill;
    }
}