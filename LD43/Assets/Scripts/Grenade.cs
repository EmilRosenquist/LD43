using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Wepond {

    [SerializeField] private ParticleSystem grenadeExplosionPrefab;
    [SerializeField] private float force = 500;
    [SerializeField] private float torque = 1000;

    private Rigidbody rb;
    private bool grenadeReleased = false;
    private float timer = 0;

    public override void Attack(Player player, Vector3 spawnPos, Vector3 direction)
    {
        transform.parent = null;

        rb.isKinematic = false;

        rb.AddForce(transform.forward * force);
        rb.AddTorque(transform.right * torque);

        grenadeReleased = true; //redo to explode at next collision

        timer = 0;        
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
        rb = gameObject.GetComponent<Rigidbody>();
    }
	
	void Update ()
    {
        timer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (grenadeReleased == true && timer > 0.1)
        {
            ParticleSystem explosion;
            explosion = Instantiate(grenadeExplosionPrefab, transform.position, transform.rotation) as ParticleSystem;

            gameObject.SetActive(false);
        }
    }
}
