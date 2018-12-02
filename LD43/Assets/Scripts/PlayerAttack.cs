using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public float radius = 1.5f;

    //private Camera camera;

    // Use this for initialization
    //void Start ()
    //   {
    //       camera = Camera.main;

    //       if (camera == null)
    //       {
    //           Debug.Log("NULL");
    //       }
    //}

    //// Update is called once per frame
    //void Update ()
    //   {
    //       CheckNearbyObjects();
    //}

    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                    Vector3 playerPosition = transform.position;

                    Collider[] hitColliders = Physics.OverlapSphere(playerPosition, radius);
                    GameObject kill = hitColliders[0].gameObject;

                Collider enemyCollider = kill.GetComponent<Collider>();

                if (hit.point == enemyCollider.bounds.size)
                {

                    if (hitColliders.Length > 2)
                    {
                        Debug.Log("Found something!");
                        Destroy(kill);
                        hitColliders = null;
                    }

                }
                //draw invisible ray cast/vector
                Debug.DrawLine(ray.origin, hit.point);
                //log hit area to the console
                Debug.Log("hit.point " + hit.point);
            }
        }





    }

}
