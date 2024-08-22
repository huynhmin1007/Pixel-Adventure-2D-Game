using Assets.Scripts.Base;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
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

    private float ignitedTimer;
    private float chilledTimer;
    private float shockedTimer;

    private float igniteDamageCooldown = .3f;
    private float igniteDamageTimer;
    private int igniteDamage;

    [SerializeField] private int currentHealth;

    protected Character characterBase;

    protected virtual void Start()
    {
        critPower.SetDefaultValue(150);
        currentHealth = maxHealth.GetValue();
        characterBase = GetComponent<Character>();
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
            currentHealth -= igniteDamage;

            if (currentHealth < 0)
                Dead();

            igniteDamageTimer = igniteDamageCooldown;
        }
    }

    public virtual void TakeDamage(int _damage)
    {
        if (characterBase.IsImmune) return;

        currentHealth -= _damage;

        if (currentHealth <= 0)
        {
            Dead();
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

        //_targetStat.TakeDamage(totalDamage);
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
        if (isIgnited || isChiled || isShocked)
        {
            return;
        }

        if (_ignite)
        {
            isIgnited = _ignite;
            ignitedTimer = 2;
        }

        if (_chill)
        {
            isChiled = _chill;
            chilledTimer = 2;
        }

        if (_shock)
        {
            isShocked = _shock;
            shockedTimer = 2;
        }
    }

    public void SetupIgniteDamage(int _damage) => igniteDamage = _damage;

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
}
