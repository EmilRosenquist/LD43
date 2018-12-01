using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    float liveTime = 20;
    float velocity = 10f;
    Vector3 moveDir = Vector3.forward;

    public Vector3 MoveDir
    {
        set
        {
            moveDir = value.normalized;
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = transform.position + moveDir * Time.deltaTime * velocity;
	}
}
