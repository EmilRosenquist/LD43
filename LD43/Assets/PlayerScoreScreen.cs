﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerScoreScreen : MonoBehaviour {
    public NetworkInstanceId IDtoTrack;
    public Text nameText;
    public Text scoreText;
    public GameManager gm;

    private void Start()
    {
        if (!gm)
            gm = FindObjectOfType<GameManager>();

    }
    private void Update()
    {
        
        if (gm)
        {
            nameText.text = "spelare: " + IDtoTrack;
            for(int i = 0; i < gm.playerList.Count; i++)
            {
                if(IDtoTrack == gm.playerList[i].GetComponent<Player>().netId)
                {
                    scoreText.text = gm.playerList[i].GetComponent<Player>().damageDone.ToString();
                }
            }
        }

    }
}
