using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGun : Wepond {
    [SerializeField] private int maxLoadedAmmo = 1;
    [SerializeField] private int extraAmmo = 5;
    [SerializeField] private bool unlimitedAmmo = false;
    [SerializeField] private float shootSpeed = 3f;
    private bool reloading = false;
    private Timer timer;

    public override void Attack(Player player, Vector3 spawnPos, Vector3 direction)
    {
    }

    public override int CheckMagasine()
    {
        return loadedAmmo;
    }

    public override int CheckTotalBullets()
    {
        return loadedAmmo + reserveAmmo;
    }

    public override bool ReloadAmmo()
    {
        return true;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
