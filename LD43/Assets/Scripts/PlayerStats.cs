using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {
    //Basic stats
    public float healthMultiplier = 1.0f;
    public float damageMultiplierMultiplier = 1.0f;
    //MovementStats
    public float speedMultiplier = 1.0f;
    public float sprintMultiplier = 1.0f;
    public float jumpHeightMultiplier = 1.0f;
    public void Reset(){
        healthMultiplier = 1.0f;
        damageMultiplierMultiplier = 1.0f;
        speedMultiplier = 1.0f;
        sprintMultiplier = 1.0f;
        jumpHeightMultiplier = 1.0f;
    }
}
