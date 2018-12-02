using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    float liveTime = 10f;
    Timer timer;
    [SerializeField] private float velocity = 50f;
    [SerializeField] private int dmg = 25;
    Vector3 moveDir = Vector3.up;
    public Player shooter;
    private Rigidbody rb;

    public Vector3 MoveDir
    {
        set
        {
            moveDir = value.normalized;
            rb = GetComponent<Rigidbody>();
            rb.velocity = moveDir * velocity;
        }
    }
    // Use this for initialization
    void Start() {
        timer = new Timer(liveTime);
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Spela upp ljud som indikerar träff
        Debug.Log(other.gameObject.name);
        if (other.gameObject.GetComponent<Player>() != null && other.gameObject.GetComponent<Player>() != shooter)
        {
            other.gameObject.GetComponent<Player>().TakeDamage(dmg);
            shooter.DidDamage(dmg);
        }
        if (other.gameObject.GetComponent<Player>() == null){
            Destroy(gameObject);
        }else if (other.gameObject.GetComponent<Player>() != null && other.gameObject.GetComponent<Player>() != shooter){
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update () {
        if (timer.tick(Time.deltaTime) < 0)
        {
            Destroy(gameObject);
        }
        //transform.position = transform.position + moveDir * Time.deltaTime * velocity;

	}
}
