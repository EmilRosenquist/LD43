using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Wepond {

    [SerializeField] private ParticleSystem grenadeExplosionPrefab;
    [SerializeField] private float force = 500;
    [SerializeField] private float torque = 1000;

    private bool grenadeReleased = false;


    public override void Attack(Player player, Vector3 spawnPos, Vector3 direction)
    {

        if (!grenadeReleased)
        {
            player.CmdSpawnGranade(2, transform.position, transform.forward * force, transform.right * torque, transform.rotation);
            //temp code for disabling child meshes
            foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
            {
                mr.enabled = false;
            }
            grenadeReleased = true;
        }
      
    }

    public override int CheckMagasine()
    {
        throw new System.NotImplementedException();
    }

    public override int CheckTotalBullets()
    {
        throw new System.NotImplementedException();
    }

    public override bool ReloadAmmo()
    {
        throw new System.NotImplementedException();
    }

    void Start ()
    {
    }
	
	void Update ()
    {

    }

    //private void OnCollisionEnter(Collision collision)
    //{

    //    if (grenadeReleased == true && timer > 0.1f)
    //    {
    //        ParticleSystem explosion;
    //        explosion = Instantiate(grenadeExplosionPrefab, transform.position, transform.rotation) as ParticleSystem;

    //        gameObject.SetActive(false);
    //    }
    //}

    public override void Reset()
    {
        foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
        {
            mr.enabled = true;
        }
        grenadeReleased = false;
    }
}
