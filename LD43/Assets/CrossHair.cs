using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour {
    [SerializeField] private GameManager gm;
    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject hitMarker;
    float showTime = 0f;
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
	public void ShowHit()
    {
        showTime = 0.2f;
        hitMarker.SetActive(true);
    }
    void Update()
    {
        if (!hud.activeInHierarchy)
        {
            if (gm.gameObject.activeInHierarchy)
            {
                hud.GetComponent<HUD>().Setgm(gm);
                hud.SetActive(true);
            }
        }
        if (showTime > 0)
        {
            showTime -= Time.deltaTime;
        }
        else
        {
            hitMarker.SetActive(false);
        }
    }
}
