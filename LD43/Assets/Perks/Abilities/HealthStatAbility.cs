using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthStatAbility : Ability {
    private float multiplier = 1.0f;
    public HealthStatAbility(float multiplier){
        this.multiplier = multiplier;
    }
    public override string GetAbilityString(){
        return ((multiplier - 1.0f) * 100.0f) + "% HP";
    }
    public override void ApplyAbility(Player p){
        p.AddToHealthMultiplier(multiplier);
    }
}
