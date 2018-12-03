using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRifle : Wepond
{
    [SerializeField] private int maxLoadedAmmo = 30;
    [SerializeField] private int extraAmmo = 90;
    [SerializeField] private float timeBetweenShots = 0.05f;
    [SerializeField] private float timeToReload = 2.5f;

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

    private void OnEnable()
    {
        reloading = false;
        if (loadedAmmo == 0)
        {
            ReloadAmmo();
        }
    }

    private void Update()
    {
        if(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("MiddleState"))
        {
            //reserveAmmo -= (maxLoadedAmmo - loadedAmmo);
            //if (reserveAmmo >= maxLoadedAmmo)
            //{
            //    loadedAmmo = maxLoadedAmmo;
            //}
            //else
            //{
            //    loadedAmmo = reserveAmmo;
            //}

            //reserveAmmo = Mathf.Clamp(reserveAmmo, 0, extraAmmo);
            //Debug.Log(reserveAmmo);

            if (reserveAmmo == 0) return;
            int ammoToReload = maxLoadedAmmo - loadedAmmo;
            if (ammoToReload > reserveAmmo) ammoToReload = reserveAmmo;

            loadedAmmo += ammoToReload;
            reserveAmmo -= ammoToReload;
            reserveAmmo = Mathf.Clamp(reserveAmmo, 0, extraAmmo);


            //if (extraAmmo == 0) return;
            //fireNext = Time.time + ws.reloadTime;
            //int ammoToReload = ws.ammoCapacity - magAmmo;
            //if (ammoToReload > extraAmmo) ammoToReload = extraAmmo;


            //magAmmo += ammoToReload;
            //extraAmmo -= ammoToReload;
            //extraAmmo = Mathf.Clamp(extraAmmo, 0, ws.extraAmmoCapacity);


            GetComponent<Animator>().SetInteger("Bullets", maxLoadedAmmo);
            GetComponent<Animator>().SetInteger("ReserveAmmo", reserveAmmo);
            reloading = false;
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
                    GetComponent<Animator>().SetTrigger("Fire");
                    loadedAmmo--;
                    GetComponent<Animator>().SetInteger("Bullets", loadedAmmo);
                    player.CmdSpawnBullet(3, spawnPos, direction);
                    shootTimer.reset();
                }
                else
                {
                    ReloadAmmo();
                }
            }
        }
    }

    public override bool ReloadAmmo()
    {
        if(reserveAmmo > 0 && !reloading)
        {
            reloading = true;
            GetComponent<Animator>().SetTrigger("Reload");
            GetComponent<Animator>().ResetTrigger("Fire");
            return true;
        }
        else
        {
            GetComponent<Animator>().SetTrigger("OutOfAmmo");
            return false;
        }

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
