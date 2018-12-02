using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class firstGrenade : MonoBehaviour {

    [SerializeField] private ParticleSystem grenadeExplosionPrefab;
    [SerializeField] private float force = 500;
    [SerializeField] private float torque = 1000;

    private Rigidbody rb;
    private bool grenadeReleased = false;
    private float timer;

	// Use this for initialization
	void Start ()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0)) //right mouse button
        {
            transform.parent = null;

            rb.isKinematic = false;

            rb.AddForce(transform.forward * force);
            rb.AddTorque(transform.right * torque);

            grenadeReleased = true; //ready to explode at next collision

            timer = 0;

        }
            timer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision) //redo to explode at next collision
    {
        if (grenadeReleased == true && timer > 0.1)
        {
            ParticleSystem explosion;
            explosion = Instantiate(grenadeExplosionPrefab, transform.position, transform.rotation) as ParticleSystem;

            gameObject.SetActive(false);
        }
    }
}
