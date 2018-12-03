using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour {
    [SerializeField] private GameObject winScreen;
    [SerializeField] private Text bestPlayerText;
    GameManager gm;
	// Use this for initialization
	void Start () {
        gm = FindObjectOfType<GameManager>();

    }
	
	// Update is called once per frame
	void Update () {
        if (!gm) { gm = FindObjectOfType<GameManager>(); }
        else
        {
            if(gm.currentState == GameManager.states.WINGAME)
            {
                winScreen.SetActive(true);
                bestPlayerText.text = gm.aliveList[0].name;
            }
            else
            {
                winScreen.SetActive(false);
                bestPlayerText.text = "";
            }
        }
	}
}
