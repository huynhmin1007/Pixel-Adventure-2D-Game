using Assets.Scripts.Base;
using Assets.Scripts.Base.UI;
using Assets.Scripts.Characters.Enemy;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private CharacterFlashFX fx;    


    [Header("Major stats")]
    public Stat strength; // 1 point increase damage by 1 and crit.power by 1%
    public Stat agility; // 1 point increase evasion by 1 and crit.power by 1%
    public Stat intelgence; // 1 point increase magic damage by 1 and magic resistance by 3
    public Stat vitality; // 1 point increase health by 3 or 5 points

    [Header("Offensive stats")]
    public Stat damage;
    public Stat critChance;
    public Stat critPower; // default value 150%

    [Header("Defensive stats")]
    public Stat maxHealth;
    public Stat armor;
    public Stat evasion;
    public Stat magicResistance;

    [Header("Magic stats")]
    public Stat fireDamage;
    public Stat iceDamage;
    public Stat lightningDamage;

    public bool isIgnited; // does damage over time
    public bool isChiled; // reduce armor by 20%
    public bool isShocked; // reduce accuracy by 20%

    [SerializeField] private float ailmentsDuration = 4;
    private float ignitedTimer;
    private float chilledTimer;
    private float shockedTimer;

    private float igniteDamageCooldown = .3f;
    private float igniteDamageTimer;
    private int igniteDamage;
    [SerializeField] private GameObject shockStrikePrefab;
    private int shockDamage;

    public int currentHealth;

    public System.Action onHealthChanged;


    protected Character characterBase;

    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);
        currentHealth = GetMaxHealthValue();
        characterBase = GetComponent<Character>();
        fx = GetComponent<CharacterFlashFX>();

        //onHealthChanged();
    }

    protected virtual void Update()
    {
        ignitedTimer -= Time.deltaTime;
        chilledTimer -= Time.deltaTime;
        shockedTimer -= Time.deltaTime;

        igniteDamageTimer -= Time.deltaTime;

        if (ignitedTimer < 0)
            isIgnited = false;

        if (chilledTimer < 0)
            isChiled = false;

        if (shockedTimer < 0)
            isShocked = false;

        if (igniteDamageTimer < 0 && isIgnited)
        {
            DecreaceHealthBy(igniteDamage);

            if (currentHealth < 0)
                Dead();

            igniteDamageTimer = igniteDamageCooldown;
        }
    }

    public virtual void TakeDamage(int _damage)
    {
        if (characterBase.IsImmune) return;

        DecreaceHealthBy(_damage);

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    protected virtual void DecreaceHealthBy(int _damge)
    {
        currentHealth -= _damge;

        if(onHealthChanged != null)
        {
            onHealthChanged();

        }
    }

    public virtual void DoDamage(CharacterStats _targetStat)
    {
        if (TargetCanAvoidAttack(_targetStat)) return;

        int totalDamage = damage.GetValue() + strength.GetValue();

        if (CanCrit())
        {
            totalDamage = CalculateCriticalDamage(totalDamage);
        }

        totalDamage = CheckTargetArmor(_targetStat, totalDamage);

        _targetStat.TakeDamage(totalDamage);
        DoMagicalDamage(_targetStat);
    }

    public virtual void DoMagicalDamage(CharacterStats _targetStat)
    {
        int _fireDmg = fireDamage.GetValue();
        int _iceDmg = iceDamage.GetValue();
        int _lightningDmg = lightningDamage.GetValue();

        int totalMagicalDmg = _fireDmg + _iceDmg + _lightningDmg + intelgence.GetValue();
        totalMagicalDmg = CheckTargetResistance(_targetStat, totalMagicalDmg);

        _targetStat.TakeDamage(totalMagicalDmg);

        if (Mathf.Max(_fireDmg, _iceDmg, _lightningDmg) <= 0) return;

        bool canApplyIgnite = _fireDmg > _iceDmg && _fireDmg > _lightningDmg;
        bool canApplyChill = _iceDmg > _fireDmg && _iceDmg > _lightningDmg;
        bool canApplyShock = _lightningDmg > _fireDmg && _lightningDmg > _iceDmg;

        while (!canApplyIgnite && !canApplyChill && !canApplyShock)
        {
            if (Random.value < .3f && _fireDmg > 0)
            {
                canApplyIgnite = true;
                _targetStat.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }

            if (Random.value < .5f && _iceDmg > 0)
            {
                canApplyChill = true;
                _targetStat.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }

            if (Random.value < .5f && _lightningDmg > 0)
            {
                canApplyShock = true;
                _targetStat.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
                return;
            }
        }

        if (canApplyIgnite)
            _targetStat.SetupIgniteDamage(Mathf.RoundToInt(_fireDmg * .2f));

        _targetStat.ApplyAilments(canApplyIgnite, canApplyChill, canApplyShock);
    }

    private static int CheckTargetResistance(CharacterStats _targetStat, int totalMagicalDmg)
    {
        totalMagicalDmg -= _targetStat.magicResistance.GetValue() + (_targetStat.intelgence.GetValue() * 3);
        totalMagicalDmg = Mathf.Clamp(totalMagicalDmg, 0, int.MaxValue);
        return totalMagicalDmg;
    }

    public void ApplyAilments(bool _ignite, bool _chill, bool _shock)
    {
        bool canApplyIgnite = !isIgnited && !isChiled && !isShocked;
        bool canApplyChill = !isIgnited && !isChiled && !isShocked;
        bool canApplyShock = !isIgnited && !isChiled;

        if (_ignite && canApplyIgnite)
        {
            isIgnited = _ignite;
            ignitedTimer = ailmentsDuration;
            fx.IgniteFxFor(ailmentsDuration);
        }

        if (_chill && canApplyChill)
        {
            isChiled = _chill;
            chilledTimer = ailmentsDuration;

            float slowPercenteger = .2f;

            GetComponent<Character>().SlowEntityBy(slowPercenteger, ailmentsDuration);
            fx.ChillFxFor(ailmentsDuration);
        }

        if (_shock && canApplyShock)
        {
            if (!isShocked)
            {
            isShocked = _shock;
            shockedTimer = ailmentsDuration;
            fx.ShockFxFor(ailmentsDuration);

            }
            else
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 25);

                float closestDistance = Mathf.Infinity;
                Transform closestEnemy = null;
                ;
                foreach (Collider2D hit in colliders)
                {
                    EnemyCharacter enemy = hit.GetComponent<EnemyCharacter>();

                    if (enemy != null && Vector2.Distance(transform.position, hit.transform.position)> 1)
                    {
                        float distanceToEnemy = Vector2.Distance(transform.position, hit.transform.position);

                        if (distanceToEnemy < closestDistance)
                        {

                            closestDistance = distanceToEnemy;
                            closestEnemy = hit.transform;
                        }
                    }

                    if(closestEnemy == null)
                    {
                        closestEnemy = transform;
                    }
                }
                if (closestEnemy != null)
                {
                    GameObject newShockStrike = Instantiate(shockStrikePrefab, transform.position, Quaternion.identity);
                    newShockStrike.GetComponent<ThunkderStrikeController>().Setup(shockDamage, closestEnemy.GetComponent<CharacterStats>());
                }
            }
          
        }
    }

    public void SetupIgniteDamage(int _damage) => igniteDamage = _damage;

    public void SetupShockStrikeDamage(int _damage) => shockDamage = _damage;
    protected virtual void Dead()
    {

    }

    private bool TargetCanAvoidAttack(CharacterStats _targetStat)
    {
        int totalEvasion = _targetStat.evasion.GetValue() + _targetStat.agility.GetValue();

        if (isShocked)
            totalEvasion += 20;

        if (Random.Range(0, 100) < totalEvasion)
        {
            return true;
        }
        return false;
    }

    private int CheckTargetArmor(CharacterStats _targetStat, int totalDamage)
    {
        if (_targetStat.isChiled)
            totalDamage -= Mathf.RoundToInt(_targetStat.armor.GetValue() * .8f);
        else
            totalDamage -= _targetStat.armor.GetValue();

        totalDamage = Mathf.Clamp(totalDamage, 0, int.MaxValue);
        return totalDamage;
    }

    private bool CanCrit()
    {
        int totalCriticalChance = critChance.GetValue() + agility.GetValue();

        if (Random.Range(0, 100) <= totalCriticalChance) return true;

        return false;
    }

    private int CalculateCriticalDamage(int _damage)
    {
        float totalCriticalPower = (critPower.GetValue() + strength.GetValue()) * .01f;

        float critDamage = _damage * totalCriticalPower;

        return Mathf.RoundToInt(critDamage);
    }

    public int GetMaxHealthValue()
    {
        return maxHealth.GetValue() + vitality.GetValue() * 5;
    }
}
