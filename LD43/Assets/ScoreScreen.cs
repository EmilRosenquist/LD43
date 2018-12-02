using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour {
    [SerializeField] private GameManager gm;
    [SerializeField] private GameObject scoreScreen;
    [SerializeField] private GameObject playerSlot;
    int players = 0;
    // Use this for initialization
    void Start () {
        if (!gm)
        {
            gm = FindObjectOfType<GameManager>();
            //if(gm)
                //StartCoroutine(UpdateScoreScreen());
        }
        scoreScreen = GameObject.Find("ScoreScreen");
        if (scoreScreen)
        {
            scoreScreen.SetActive(false);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        if (gm)
        {
            
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                for (int i = 0; i < gm.playerList.Count; i++)
                {

                    GameObject go = Instantiate(playerSlot, scoreScreen.transform);
                    go.GetComponent<PlayerScoreScreen>().IDtoTrack = gm.playerList[i].GetComponent<Player>().netId;
                }
                scoreScreen.SetActive(true);
            }
            else if (Input.GetKeyUp(KeyCode.Tab))
            {
                scoreScreen.SetActive(false);
                for (int i = scoreScreen.transform.childCount; i > 0; i--)
                {
                    Destroy(scoreScreen.transform.GetChild(i-1).gameObject);
                }
            }
        }
	}

}
