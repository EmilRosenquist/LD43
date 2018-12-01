using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Wepond : MonoBehaviour {
    [SerializeField] public GameObject bulletPrefab;
    protected int reserveAmmo;
    protected bool infiniteAmmo = false;
    protected int loadedAmmo;

    public abstract void Attack(Player player, Vector3 spawnPoint);
    public abstract bool ReloadAmmo();
    public abstract int CheckMagasine();
    public abstract int CheckTotalBullets();
}
