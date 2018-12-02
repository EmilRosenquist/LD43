public class DamageMultiplierAbility : Ability
{
    private float multiplier = 1.0f;
    public DamageMultiplierAbility(float multiplier)
    {
        this.multiplier = multiplier;
    }
    public override string GetAbilityString()
    {
        return ((multiplier - 1.0f) * 100.0f) + "% Damage";
    }
    public override void ApplyAbility(Player p)
    {
        p.AddToDamageMultiplierMultiplier(multiplier);
    }
}

