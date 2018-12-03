using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class GameManager : NetworkBehaviour
{
    public enum states
    {
        PREGAME = 0,
        BUYTIME = 1,
        INGAME = 2,
        ENDGAME = 3

    };

    [SerializeField] private float buyTime;
    public List<GameObject> playerList = new List<GameObject>();
    public List<GameObject> aliveList = new List<GameObject>();
    private Timer buyTimer = new Timer(0);



    [SyncVar]
    public states currentState = states.PREGAME;





    private void Start()
    {
        StartCoroutine(updateState());
        
    }
    private void Update()
    {
        
    }


    public IEnumerator updateState()
    {
        if (!isServer)
            yield return null;

        float prevTime = 0.0f;

        while (true)
        {
            updateLists();

            if (playerList.Count <= 1)
            {
                currentState = states.PREGAME;
                if (buyTimer != null) buyTimer.reset();
            }

            if (currentState == states.BUYTIME)
            {
                if (buyTimer != null)
                    buyTimer.tick(Time.time - prevTime);
            }

            if (aliveList.Count < 2 && currentState == states.INGAME)
            {
                currentState = states.ENDGAME;
                //nextround
            }


            if (playerList.Count > 1 && currentState == states.PREGAME)
            {
                CmdRespawnPlayers();

                currentState = states.INGAME;

            }

            if (currentState == states.BUYTIME && buyTimer.Time <= 0)
            {
                CmdHideBuyWindow();
                currentState = states.PREGAME;
                buyTimer.reset();

            }

            if (currentState == states.ENDGAME)
            {
                CmdGenerateCards();
                currentState = states.BUYTIME;
                buyTimer = new Timer(buyTime);
            }

            prevTime = Time.time;

            yield return new WaitForSeconds(1);
        }
    }



    public void updateLists()
    {
        playerList.Clear();
        aliveList.Clear();
        playerList.AddRange(GameObject.FindGameObjectsWithTag("Player"));

        foreach (GameObject p in playerList)
        {
            if (p.GetComponent<Player>().isAlive)
            {
                aliveList.Add(p);
            }
        }

    }
    [Command]
    public void CmdGenerateCards()
    {
        PerkStruct[] perks = new PerkStruct[3];
        perks[0] = GetComponent<Perks>().GeneratePerk(1);
        perks[1] = GetComponent<Perks>().GeneratePerk(-1);
        perks[2] = GetComponent<Perks>().GeneratePerk(2);
        RpcDistributeCards(perks);
    }

    [ClientRpc]
    public void RpcDistributeCards(PerkStruct[] perks)
    {

        PerkShop ps = FindObjectOfType<PerkShop>();

        ps.ShowShop(GetComponent<Perks>(), perks[0], perks[1], perks[2]);

    }

    [Command]
    public void CmdHideBuyWindow()
    {
        RpcHideBuyWindow();
    }
    [ClientRpc]
    public void RpcHideBuyWindow()
    {
        FindObjectOfType<PerkShop>().HideShop() ;
    }

    [Command]
    public void CmdRespawnPlayers()
    {
        RpcrespawnPlayers();
    }
    
    [ClientRpc]
    public void RpcrespawnPlayers()
    {
        GameObject[] spawnpoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        Vector3 newPos = Vector3.zero;
        for (int i = 0; i < playerList.Count; i++)
        {
            newPos = spawnpoints[Random.Range(0, spawnpoints.Length)].transform.position;
            playerList[i].transform.position = newPos;
            playerList[i].GetComponent<Player>().CmdToggleSpectatorMode(true);
            playerList[i].GetComponent<Player>().CmdResetStats();
        }
    }


}
