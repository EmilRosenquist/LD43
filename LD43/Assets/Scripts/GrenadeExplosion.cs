using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour {


    public ParticleSystem explosion;
    public float radius;
    public int damage = 30;

    private int nrRepeat = 1;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        ExplosionDamage();
        Debug.Log("past ExplosionDamage()");

        if (explosion.isPlaying)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
	}

    public void ExplosionDamage()
    {
        if (nrRepeat > 0) //do following once only
        {
            Collider[] hitColliders;
            hitColliders = Physics.OverlapSphere(transform.position, radius);

            for (int i = 0; i < hitColliders.Length; i++)
            {
                Debug.Log(hitColliders[i].gameObject.name);
                GameObject hitGameObject = hitColliders[i].transform.gameObject;

                if (hitGameObject.GetComponent<MyPlayer>() != null)
                {
                    MyPlayer hitPlayer = hitGameObject.GetComponent<MyPlayer>();

                    hitPlayer.TakeDamage(damage);
                }

                nrRepeat = -10; //never do above again
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
