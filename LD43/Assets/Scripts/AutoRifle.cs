using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRifle : Wepond
{
    [SerializeField] private int maxLoadedAmmo = 30;
    [SerializeField] private int extraAmmo = 90;
    [SerializeField] private float timeBetweenShots = 0.05f;
    [SerializeField] private float timeToReload = 2.5f;
    [SerializeField] private bool unlimitedAmmo = false;


    private bool reloading = false;
    private Timer shootTimer;
    private Timer reloadTimer;

    private void Start()
    {
        reserveAmmo = extraAmmo;
        loadedAmmo = maxLoadedAmmo;
        shootTimer = new Timer(timeBetweenShots);
        reloadTimer = new Timer(timeToReload);
    }

    private void Update()
    {
        if (reloading) reloadTimer.tick(Time.deltaTime);
        if (reloadTimer.Time < 0)
        {
            reloading = false;
            reloadTimer.reset();
            ReloadAmmo();
        }
        shootTimer.tick(Time.deltaTime);
    }

    public override void Attack(Player player, Vector3 spawnPos, Vector3 direction)
    {
        if(!reloading)
        {
            if (shootTimer.Time <= 0)
            {
                if (loadedAmmo > 0)
                {
                    loadedAmmo--;
                    player.CmdSpawnBullet(0, spawnPos, direction);
                    shootTimer.reset();
                }
                else
                {
                    StartReload();
                }
            }
        }
    }

    public override bool ReloadAmmo()
    {
        if (!unlimitedAmmo)
        {
            if (reserveAmmo > 0)
            {
                //play anim and reload gun after x time
                if (reserveAmmo > maxLoadedAmmo)
                {
                    reserveAmmo -= maxLoadedAmmo;
                    loadedAmmo = maxLoadedAmmo;
                    Debug.Log("Reserve Ammo: " + reserveAmmo);
                }
                else
                {
                    loadedAmmo = reserveAmmo;
                    reserveAmmo = 0;
                    Debug.Log("Reserve Ammo: " + reserveAmmo);
                }
                return true;
            }
            else
            {
                //out of ammo
                return false;
            }

        }
        loadedAmmo = maxLoadedAmmo;
        return true;
    }

    public void StartReload()
    {
        reloadTimer.reset();
        reloading = true;
    }

    public override int CheckMagasine()
    {
        return loadedAmmo;
    }
    public override int CheckTotalBullets()
    {
        return loadedAmmo + reserveAmmo;
    }

    public override void Reset()
    {
        reserveAmmo = extraAmmo;
        loadedAmmo = maxLoadedAmmo;
    }
}
