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
    private bool isHit = false;
    private bool hasHit = false;

    public Vector3 MoveDir
    {
        set
        {
            Vector3 delta = value.normalized;
            transform.LookAt(transform.position + delta);
            moveDir = value.normalized;
        }
    }
    // Use this for initialization
    void Start() {
        timer = new Timer(liveTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Spela upp ljud som indikerar träff
        if (!hasHit && other.gameObject.GetComponentInParent<Player>() != null && other.gameObject.GetComponentInParent<Player>() != shooter)
        {
            hasHit = true;
            other.gameObject.GetComponentInParent<Player>().TakeDamage((int)(dmg * shooter.damageMultiplier));
            shooter.DidDamage((int)(dmg * shooter.damageMultiplier));
        }
        if (other.gameObject.GetComponentInParent<Player>() == null){
            Destroy(gameObject);
        }else if (other.gameObject.GetComponentInParent<Player>() != null && other.gameObject.GetComponentInParent<Player>() != shooter){
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void FixedUpdate () {
        if (timer.tick(Time.fixedDeltaTime) < 0)
        {
            Destroy(gameObject);
        }
        //transform.position = transform.position + moveDir * Time.deltaTime * velocity;
        if (!isHit){
            Ray ray = new Ray(transform.position, moveDir);
            RaycastHit hit;
            string[] layers = new string[1];
            layers[0] = "Default";
            if (Physics.Raycast(ray, out hit, velocity * Time.fixedDeltaTime, LayerMask.GetMask(layers))){
                transform.position = hit.point;
                isHit = true;
            }else{
                transform.position = transform.position + moveDir * velocity * Time.fixedDeltaTime;
            }
        }
	}
}
