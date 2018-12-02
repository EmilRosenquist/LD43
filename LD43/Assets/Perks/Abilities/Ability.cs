using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability{
    public abstract string GetAbilityString();
    public abstract void ApplyAbility(Player p);
}
