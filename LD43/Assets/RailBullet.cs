using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailBullet : MonoBehaviour {
    Vector3[] poses = new Vector3[2];
    LineRenderer lr;
    float liveTime = 0.2f;
    [SerializeField] private GameObject soundEmmiter;
    [SerializeField] private AudioClip sound;
    public void setPositions(Vector3 start, Vector3 end)
    {
        poses[0] = start;
        poses[1] = end;
        GameObject go = Instantiate(soundEmmiter, start, Quaternion.identity);
        go.GetComponent<AudioSource>().PlayOneShot(sound);
        Destroy(go, 7f);
    }

    
	// Use this for initialization
	void Start () {
        
        lr = GetComponent<LineRenderer>();
        lr.SetPositions(poses);
        Transform killZone = transform.GetChild(0);
        killZone.position = poses[1];
    }
	
	// Update is called once per frame
	void Update () {
        if (liveTime < 0)
            Destroy(gameObject);
        else
        {
            liveTime -= Time.deltaTime;
        }
	}
}
