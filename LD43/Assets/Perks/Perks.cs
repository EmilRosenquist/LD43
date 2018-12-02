using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perks : MonoBehaviour{
    private static List<Ability> tierOneGoodAbilites = new List<Ability>();
    private static List<Ability> tierOneBadAbilites = new List<Ability>();
    void Awake(){
        FillTierOneGoodAbilities();
        FillTierOneBadAbilities();
    }
    public static Perk GeneratePerk(int tier){
        Ability good;
        Ability bad;
        if(tier == 1){
            good = tierOneGoodAbilites[4/*Random.Range(0, tierOneGoodAbilites.Count)*/];
            bad = tierOneBadAbilites[0/*Random.Range(0, tierOneBadAbilites.Count)*/];
            return new Perk(good, bad);
        }
        return null;
    }
    private void FillTierOneGoodAbilities(){
        tierOneGoodAbilites.Add(new HealthStatAbility(1.2f));
        tierOneGoodAbilites.Add(new SpeedStatAbility(1.2f));
        tierOneGoodAbilites.Add(new SprintStatAbility(1.2f));
        tierOneGoodAbilites.Add(new JumpHeightStatAbility(1.5f));
        tierOneGoodAbilites.Add(new DamageMultiplierAbility(1.2f));
    }
    private void FillTierOneBadAbilities(){
        tierOneBadAbilites.Add(new HealthStatAbility(0.8f));
        tierOneBadAbilites.Add(new SpeedStatAbility(0.8f));
        tierOneBadAbilites.Add(new SprintStatAbility(0.8f));
        tierOneBadAbilites.Add(new JumpHeightStatAbility(0.5f));
        tierOneBadAbilites.Add(new DamageMultiplierAbility(0.8f));
    }
}
