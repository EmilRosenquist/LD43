using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Wepond : MonoBehaviour {
    public int reserveAmmo;
    public bool infiniteAmmo = false;
    public int loadedAmmo;

    public abstract void Attack(Player player, Vector3 spawnPos, Vector3 direction);
    public abstract bool ReloadAmmo();
    public abstract int CheckMagasine();
    public abstract int CheckTotalBullets();
    public abstract void Reset();
}
