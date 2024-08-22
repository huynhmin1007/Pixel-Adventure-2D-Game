using Assets.Scripts.Characters.Enemy;

public class EnemyStats : CharacterStats
{
    private EnemyCharacter enemy;

    protected override void Start()
    {
        base.Start();
        enemy = (EnemyCharacter)characterBase;
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        characterBase.DamageEffect();
    }

    protected override void Dead()
    {
        base.Dead();

        enemy.LastAnimBoolName = enemy.StateMachine.GetCurrentAnimation().ToString();
        enemy.Dead();
    }
}
