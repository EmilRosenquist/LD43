using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Perks : NetworkBehaviour{
    private List<List<Ability>> goodAbilities;
    private List<List<Ability>> badAbilities;
    private List<Ability> tierOneGoodAbilites = new List<Ability>();
    private List<Ability> tierOneBadAbilites = new List<Ability>();

    private List<Ability> tierTwoGoodAbilites = new List<Ability>();
    private List<Ability> tierTwoBadAbilites = new List<Ability>();

    private List<Ability> tierThreeGoodAbilites = new List<Ability>();
    private List<Ability> tierThreeBadAbilites = new List<Ability>();
    void Awake(){
        FillTierOneGoodAbilities();
        FillTierOneBadAbilities();
        FillTierTwoGoodAbilities();
        FillTierTwoBadAbilities();
        FillTierThreeGoodAbilities();
        FillTierThreeBadAbilities();
        goodAbilities.Add(tierOneGoodAbilites);
        goodAbilities.Add(tierTwoGoodAbilites);
        goodAbilities.Add(tierThreeGoodAbilites);
        badAbilities.Add(tierOneBadAbilites);
        badAbilities.Add(tierTwoBadAbilites);
        badAbilities.Add(tierThreeBadAbilites);
    }
    public PerkStruct GeneratePerk(int tier){
        PerkStruct ps = new PerkStruct();
        if (tier == -1){
            int goodTier = Random.Range(0, 3);
            int badTier = Random.Range(0, 3);
            ps.goodTier = goodTier;
            ps.badTier = badTier;
            ps.goodIndex = Random.Range(0, goodAbilities[goodTier].Count);
            ps.badIndex = Random.Range(0, badAbilities[badTier].Count);
            return ps;
        }
        if(tier == 1){
            ps.goodTier = 1;
            ps.badTier = 1;
            ps.goodIndex = Random.Range(0, tierOneGoodAbilites.Count);
            ps.badIndex = Random.Range(0, tierOneBadAbilites.Count);
            return ps;
        }
        if(tier == 2){
            ps.goodTier = 2;
            ps.badTier = 2;
            ps.goodIndex = Random.Range(0, tierTwoGoodAbilites.Count);
            ps.badIndex = Random.Range(0, tierTwoBadAbilites.Count);
            return ps;
        }
        return ps;
    }
    public Perk GetPerkFromStruct(PerkStruct ps){
        Ability good;
        Ability bad;
        good = goodAbilities[ps.goodTier][ps.goodIndex];
        bad = badAbilities[ps.badTier][ps.badIndex];
        return new Perk(good, bad);
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
    private void FillTierThreeGoodAbilities()
    {
        tierThreeGoodAbilites.Add(new HealthStatAbility(2.0f));
        tierThreeGoodAbilites.Add(new SpeedStatAbility(2.0f));
        tierThreeGoodAbilites.Add(new SprintStatAbility(2.0f));
        tierThreeGoodAbilites.Add(new JumpHeightStatAbility(5.0f));
        tierThreeGoodAbilites.Add(new DamageMultiplierAbility(2.0f));
    }
    private void FillTierThreeBadAbilities()
    {
        tierThreeBadAbilites.Add(new HealthStatAbility(0.2f));
        tierThreeBadAbilites.Add(new SpeedStatAbility(0.2f));
        tierThreeBadAbilites.Add(new SprintStatAbility(0.2f));
        tierThreeBadAbilites.Add(new JumpHeightStatAbility(0.0f));
        tierThreeBadAbilites.Add(new DamageMultiplierAbility(0.2f));
    }
}
public struct PerkStruct{
    public int goodTier;
    public int goodIndex;
    public int badTier;
    public int badIndex;
};
