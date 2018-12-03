using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour {
    [SerializeField] private GameManager gm;
    [SerializeField] private GameObject hud;
    private void Awake()
    {
        hud.SetActive(false);
    }
    // Use this for initialization
    void Start () {
        if(!gm)
            gm = FindObjectOfType<GameManager>();
        hud.SetActive(false);
    }
	
	void Update () {
        if (!hud.activeInHierarchy)
        {
            if (gm.gameObject.activeInHierarchy)
            {
                hud.GetComponent<HUD>().Setgm(gm);
                hud.SetActive(true);
            }
        }
    }
}
