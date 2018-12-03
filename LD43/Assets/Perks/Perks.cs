using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perks : MonoBehaviour{
    private static List<Ability> tierOneGoodAbilites = new List<Ability>();
    private static List<Ability> tierOneBadAbilites = new List<Ability>();

    private static List<Ability> tierTwoGoodAbilites = new List<Ability>();
    private static List<Ability> tierTwoBadAbilites = new List<Ability>();
    void Awake(){
        FillTierOneGoodAbilities();
        FillTierOneBadAbilities();
        FillTierTwoGoodAbilities();
        FillTierTwoBadAbilities();
    }
    public static Perk GeneratePerk(int tier){
        Ability good;
        Ability bad;
        if(tier == 1){
            good = tierOneGoodAbilites[4/*Random.Range(0, tierOneGoodAbilites.Count)*/];
            bad = tierOneBadAbilites[0/*Random.Range(0, tierOneBadAbilites.Count)*/];
            return new Perk(good, bad);
        }
        if(tier == 2){
            good = tierTwoGoodAbilites[Random.Range(0, tierTwoGoodAbilites.Count)];
            bad = tierTwoBadAbilites[Random.Range(0, tierTwoBadAbilites.Count)];
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
    private void FillTierTwoGoodAbilities()
    {
        tierTwoGoodAbilites.Add(new HealthStatAbility(1.5f));
        tierTwoGoodAbilites.Add(new SpeedStatAbility(1.5f));
        tierTwoGoodAbilites.Add(new SprintStatAbility(1.5f));
        tierTwoGoodAbilites.Add(new JumpHeightStatAbility(2.0f));
        tierTwoGoodAbilites.Add(new DamageMultiplierAbility(1.5f));
    }
    private void FillTierTwoBadAbilities()
    {
        tierTwoBadAbilites.Add(new HealthStatAbility(0.5f));
        tierTwoBadAbilites.Add(new SpeedStatAbility(0.5f));
        tierTwoBadAbilites.Add(new SprintStatAbility(0.5f));
        tierTwoBadAbilites.Add(new JumpHeightStatAbility(0.2f));
        tierTwoBadAbilites.Add(new DamageMultiplierAbility(0.5f));
    }
}
