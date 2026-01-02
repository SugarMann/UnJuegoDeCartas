public class AttackHeroGA : GameAction, IHaveCaster
{
    public AttackHeroGA(EnemyView attacker)
    {
        Attacker = attacker;
        Caster = Attacker;
    }

    public EnemyView Attacker { get; }

    public CombatantView Caster { get; }
}