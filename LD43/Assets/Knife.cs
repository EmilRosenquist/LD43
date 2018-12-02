using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : Wepond {

    public float targetDistance;
    public float maxDistance;

    public override void Attack(Player player, Vector3 spawnPoint, Vector3 direction)
    {
        RaycastHit hit;

        if (Physics.Raycast(player.GetComponentInChildren<Camera>().transform.position, player.GetComponentInChildren<Camera>().transform.forward, out hit))
        {

            targetDistance = hit.distance;
//            Debug.Log("HIT SOMETHING : " + hit.transform.gameObject.name);
            if (targetDistance < maxDistance)
            {
                GameObject hitGameObject = hit.transform.gameObject;

                if (hitGameObject.GetComponent<Player>() != null)
                {
                    Player hitplayer = hitGameObject.GetComponent<Player>();

                    if (hitplayer != player)
                    {
                        hitplayer.TakeDamage(10);
                    }

                }

            }
        }
    }

    public override int CheckMagasine()
    {
        return -1;
    }

    public override int CheckTotalBullets()
    {
        return -1;
    }

    public override bool ReloadAmmo()
    {
        return true;
    }

    public override void Reset()
    {
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
