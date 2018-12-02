using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMelee : MonoBehaviour {


    public float targetDistance;
    public float maxDistance;

    private GameObject knife;
    private Rigidbody knifeRb;

	// Use this for initialization
	void Start ()
    {
        
        knife = transform.Find("Knife").gameObject;

        if (knife == null)
        {
            Debug.Log("Knife not found");
        }

        knifeRb = knife.GetComponent<Rigidbody>();

        if (knifeRb == null)
        {
            Debug.Log("KnifeRb not found");
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            targetDistance = hit.distance;

            Vector3 hitPoint = hit.point;

            if (Input.GetMouseButtonDown(0)) //left mouse button
            {
                if (targetDistance < maxDistance)
                {
                    GameObject hitGameObject = hit.transform.gameObject;
                    Destroy(hitGameObject);
                }
            }

            if (Input.GetMouseButtonDown(1)) //right mouse button
            {
                knife.transform.parent = null;
                Vector3 playerPosition = transform.position;
                knifeRb.AddRelativeForce(Vector3.forward * 100);
            }

        }

    }
}
