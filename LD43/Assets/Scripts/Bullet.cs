using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    float liveTime = 10f;
    Timer timer;
    [SerializeField] private float velocity = 50f;
    [SerializeField] private int dmg = 25;
    Vector3 moveDir = Vector3.up;

    public Vector3 MoveDir
    {
        set
        {
            moveDir = value.normalized;
        }
    }
    // Use this for initialization
    void Start() {
        timer = new Timer(liveTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "GUNS")
        {
            //Spela upp ljud som indikerar träff
            Debug.Log(other.tag);
            if (other.gameObject.GetComponent<Player>() != null)
            {
                other.gameObject.GetComponent<Player>().TakeDamage(dmg);
            }
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update () {
        if (timer.tick(Time.deltaTime) < 0)
        {
            Destroy(gameObject);
        }
        transform.position = transform.position + moveDir * Time.deltaTime * velocity;
	}
}
