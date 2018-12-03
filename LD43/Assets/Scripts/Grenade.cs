using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Wepond {

    [SerializeField] private ParticleSystem grenadeExplosionPrefab;
    [SerializeField] private float force = 500;
    [SerializeField] private float torque = 1000;

    private bool grenadeReleased = false;
    public int numberOfGrenades = 3;
    private int grenadesThrown = 0;

    void OnEnable()
    {
        reserveAmmo = numberOfGrenades - grenadesThrown;
        loadedAmmo = (numberOfGrenades - grenadesThrown > 0) ? 1 : 0; 
        GetComponent<Animator>().SetInteger("numberOfNades", numberOfGrenades - grenadesThrown);
    }

    public override void Attack(Player player, Vector3 spawnPos, Vector3 direction)
    {
        Animator anim = GetComponent<Animator>();
        if (!grenadeReleased && grenadesThrown < numberOfGrenades)
        {
            if(anim.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
            {
                anim.SetTrigger("Throw");
                //temp direction, Should be based on camera direction
                StartCoroutine(SpawnNade(player, spawnPos, direction));
                //temp code for disabling child meshes
                foreach (MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
                {
                    mr.enabled = false;
                }
                grenadeReleased = false;
                grenadesThrown++;
                reserveAmmo = numberOfGrenades - grenadesThrown;
                anim.SetInteger("numberOfNades", numberOfGrenades - grenadesThrown);
            }
        }
      
    }
    IEnumerator SpawnNade(Player player, Vector3 spawnPos, Vector3 direction){
        yield return new WaitForSeconds(1f);
        player.CmdSpawnGranade(2, transform.GetChild(0).transform.position, transform.forward * force, transform.right * torque, transform.rotation);
        yield return new WaitForSeconds(0.2f);
        loadedAmmo = (numberOfGrenades - grenadesThrown > 0) ? 1 : 0;
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
        grenadesThrown = 0;
        GetComponent<Animator>().SetInteger("numberOfNades", 1);
    }
}
