using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeProjectile : MonoBehaviour {
    [SerializeField] private ParticleSystem grenadeExplosionPrefab;
    public Player shooter;


    private void OnCollisionEnter(Collision collision)
    {

        if(collision.gameObject.GetComponent<Player>() && collision.gameObject.GetComponent<Player>() == shooter)
        {
            return;
        }
        ParticleSystem explosion;
        explosion = Instantiate(grenadeExplosionPrefab, transform.position, transform.rotation) as ParticleSystem;
        explosion.gameObject.GetComponent<GrenadeExplosion>().SetShooter(shooter);

        Destroy(gameObject);
    }

}
