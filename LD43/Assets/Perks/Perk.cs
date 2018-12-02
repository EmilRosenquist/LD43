using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perk {
    public Ability good;
    public Ability bad;
    public Perk(Ability good, Ability bad){
        this.good = good;
        this.bad = bad;
    }
}
