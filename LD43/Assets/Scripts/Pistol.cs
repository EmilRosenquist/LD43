using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Pistol : Wepond {
    [SerializeField] private int maxLoadedAmmo = 7;
    [SerializeField] private int extraAmmo = 21;
    [SerializeField] private bool unlimitedAmmo = false;
    [SerializeField] private float shootSpeed = 0.3f;
    [SerializeField] private float reloadSpeed = 1f;
    private bool reloading = false;
    private Timer timer;
    private Timer reloadTimer;
    Animator am;
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
        if (!unlimitedAmmo)
        {
            if (reserveAmmo > 0)
            {
                //play anim and reload gun after x time
                if (reserveAmmo > maxLoadedAmmo)
                {
                    reserveAmmo -= maxLoadedAmmo;
                    loadedAmmo = maxLoadedAmmo;
                }
                else
                {
                    loadedAmmo = reserveAmmo;
                    reserveAmmo = 0;
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
        if (am)
        {
            am.SetTrigger("Reload");
        }
    }


    public override void Attack(Player player, Vector3 spawnPos, Vector3 direction)
    {
        if (!reloading)
        {
            if (timer.Time < 0)
            {
                if (loadedAmmo > 0)
                {
                    loadedAmmo--;
                    player.CmdSpawnBullet(0, spawnPos, direction);
                    timer.reset();
                }
                else
                {
                    StartReload();
                }
            }
        }
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
        timer.tick(Time.deltaTime);

        if (am)
        {
            am.SetInteger("Bullets", loadedAmmo);
        }
    }
    void Start () {
        reserveAmmo = extraAmmo;
        loadedAmmo = maxLoadedAmmo;
        timer = new Timer(shootSpeed);
        timer.Time = 0f;
        reloadTimer = new Timer(reloadSpeed);
        am = GetComponent<Animator>();
    }

    public override void Reset()
    {
        reserveAmmo = extraAmmo;
        loadedAmmo = maxLoadedAmmo;
        if(am)
            am.SetInteger("Bullets", maxLoadedAmmo);
        timer = new Timer(shootSpeed);
        timer.Time = 0f;
        reloadTimer = new Timer(reloadSpeed);
    }
}
