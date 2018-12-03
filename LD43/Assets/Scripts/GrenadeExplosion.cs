using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour {


    private Player shooter;
    public ParticleSystem explosion;
    public float radius;
    public int damage = 30;

    [SerializeField] private GameObject soundEmmiter;
    [SerializeField] private AudioClip sound;

    private int nrRepeat = 1;

    List<Player> players = new List<Player>();

	// Use this for initialization
	void Start ()
    {
		
	}
	
    public void SetShooter(Player shooter)
    {
        GameObject go = Instantiate(soundEmmiter, transform.position, Quaternion.identity);
        go.GetComponent<AudioSource>().PlayOneShot(sound);
        Destroy(go, 7f);
        this.shooter = shooter;
    }
	// Update is called once per frame
	void Update ()
    {
        if(this.shooter)
            ExplosionDamage();

        if (explosion.isPlaying)
            gameObject.SetActive(true);
        else
            Destroy(gameObject);
	}

    public void ExplosionDamage()
    {
        //if (nrRepeat > 0) //do following once only
        //{
        Collider[] hitColliders;
        hitColliders = Physics.OverlapSphere(transform.position, radius);

        for (int i = 0; i < hitColliders.Length; i++)
        {

//            Debug.Log(hitColliders[i].gameObject.name);
            GameObject hitGameObject = hitColliders[i].transform.gameObject;
            if (hitGameObject.GetComponent<Player>())
            {
                Player p = hitGameObject.GetComponent<Player>();
                if (!players.Contains(p))
                {
                    p.TakeDamage(damage);
                    shooter.DidDamage(damage);
                    players.Add(p);
                }
            }
            //if (hitGameObject.GetComponent<MyPlayer>() != null)
            //{
            //    MyPlayer hitPlayer = hitGameObject.GetComponent<MyPlayer>();

            //    hitPlayer.TakeDamage(damage);
            //}

            //    nrRepeat = -10; //never do above again
            //}
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
