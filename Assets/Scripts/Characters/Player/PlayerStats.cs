using Assets.Scripts.Characters.Player;

public class PlayerStats : CharacterStats
{

    private PlayerCharacter player;

    protected override void Start()
    {
        base.Start();

        player = (PlayerCharacter)characterBase;
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        player.DamageEffect();
    }

    protected override void Dead()
    {
        base.Dead();
        player.Dead();
    }
}
