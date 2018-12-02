using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedStatAbility : Ability{
    private float multiplier = 1.0f;
    public SpeedStatAbility(float multiplier){
        this.multiplier = multiplier;
    }
    public override string GetAbilityString(){
        return ((multiplier - 1.0f) * 100.0f) + "% Speed";
    }
    public override void ApplyAbility(Player p){
        p.AddToSpeedMultiplier(multiplier);
    }
}