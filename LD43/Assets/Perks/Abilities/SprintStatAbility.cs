using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintStatAbility : Ability
{
    private float multiplier = 1.0f;
    public SprintStatAbility(float multiplier)
    {
        this.multiplier = multiplier;
    }
    public override string GetAbilityString()
    {
        return ((multiplier - 1.0f) * 100.0f) + "% Sprint";
    }
    public override void ApplyAbility(Player p)
    {
        p.AddToSprintMultiplier(multiplier);
    }
}
