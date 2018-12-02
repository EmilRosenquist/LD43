using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHeightStatAbility : Ability
{
    private float multiplier = 1.0f;
    public JumpHeightStatAbility(float multiplier)
    {
        this.multiplier = multiplier;
    }
    public override string GetAbilityString()
    {
        return ((multiplier - 1.0f) * 100.0f) + "% Jump Height";
    }
    public override void ApplyAbility(Player p)
    {
        p.AddToJumpHeightMultiplier(multiplier);
    }
}