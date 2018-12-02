using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour {
    [SerializeField] private GameManager gm;
    [SerializeField] private GameObject crosshair;

    // Use this for initialization
    void Start () {
        if(!gm)
            gm = FindObjectOfType<GameManager>();
        crosshair.SetActive(false);
    }
	
	void Update () {
        if (!crosshair.activeInHierarchy)
        {
            if (gm.gameObject.activeInHierarchy)
            {
                crosshair.SetActive(true);
            }
        }
    }
}
