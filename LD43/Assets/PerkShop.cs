using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkShop : MonoBehaviour {
    public List<PerkStruct> currentPerks = new List<PerkStruct>();
    Perks perksHelper;
    [SerializeField] private GameObject perkCardsUI;
    [SerializeField] private Text[] cardZero;
    [SerializeField] private Text[] cardRandom;
    [SerializeField] private Text[] cardTwo;
    [SerializeField] private Text timerText;
    [SerializeField] private Text moneyText;
    [SerializeField] private List<Image> overlays;
    bool boughtThisRound = false;
    int time = 10;

    Color c = new Color(1f, 0.5155591f, 0.5058824f, 0.5f);

    GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }
    public void ShowShop(Perks perksClass, PerkStruct t1, PerkStruct tierR, PerkStruct t2)
    {
        gm = FindObjectOfType<GameManager>();
        for (int i = 0; i < gm.playerList.Count; i++)
        {
            if (gm.playerList[i].GetComponent<Player>().isLocalPlayer)
            {
                moneyText.text ="Money: " + gm.playerList[i].GetComponent<Player>().money;
            }
        }
        timerText.text = "WAIT";
        perksHelper = perksClass;
        currentPerks.Add(t1);
        currentPerks.Add(tierR);
        currentPerks.Add(t2);
        //set texts
        cardZero[0].text = perksClass.GetPerkFromStruct(t1).good.GetAbilityString();
        cardZero[1].text = perksClass.GetPerkFromStruct(t1).bad.GetAbilityString();
        cardTwo[0].text = perksClass.GetPerkFromStruct(t2).good.GetAbilityString();
        cardTwo[1].text = perksClass.GetPerkFromStruct(t2).bad.GetAbilityString();
        perkCardsUI.SetActive(true);
    }

    
    public void HideShop()
    {
        time = 10;
        boughtThisRound = false;
        currentPerks.Clear();
        overlays[0].color = Color.clear;
        overlays[1].color = Color.clear;
        overlays[2].color = Color.clear;
        timerText.text = "";
        moneyText.text = "";
        cardRandom[0].text = "";
        cardRandom[1].text = "";
        perkCardsUI.SetActive(false);
    }
    public void BuyCard(int cardIndex)
    {
        if (!boughtThisRound)
        {
            
            for (int i = 0; i < gm.playerList.Count; i++)
            {
                if (gm.playerList[i].GetComponent<Player>().isLocalPlayer)
                {
                    Player p = gm.playerList[i].GetComponent<Player>();
                    //Buy Card
                    if (time < 5)
                    {
                        if (cardIndex == 0 && p.money >= 100)
                        {
                            p.money -= 100;
                            moneyText.text = "Money: " + p.money;
                            p.ApplyPerk(perksHelper.GetPerkFromStruct(currentPerks[0]));
                            overlays[0].color = c;
                            boughtThisRound = true;
                        }
                        if (cardIndex == 1 && p.money >= 50)
                        {
                            p.money -= 50;
                            moneyText.text = "Money: " + p.money;
                            p.ApplyPerk(perksHelper.GetPerkFromStruct(currentPerks[1]));
                            overlays[1].color = c;
                            boughtThisRound = true;
                            cardRandom[0].text = FindObjectOfType<Perks>().GetPerkFromStruct(currentPerks[1]).good.GetAbilityString();
                            cardRandom[1].text = FindObjectOfType<Perks>().GetPerkFromStruct(currentPerks[1]).bad.GetAbilityString();

                        }
                        if (cardIndex == 2 && p.money >= 200)
                        {
                            p.money -= 200;
                            moneyText.text = "Money: " + p.money;
                            p.ApplyPerk(perksHelper.GetPerkFromStruct(currentPerks[2]));
                            overlays[2].color = c;
                            boughtThisRound = true;
                        }
                    } 
                }
            }
            
        }
    }
    public void UpdateTimerText(int timeLeft)
    {
        time = timeLeft;
        timerText.text = timeLeft.ToString();
    }

    public void SwapText( int cardIndex)
    {
        if (cardIndex == 0)
        {
            string text;
            text = cardZero[0].text;
            cardZero[0].text = cardZero[1].text;
            cardZero[1].text = text;
        }else if(cardIndex == 2)
        {
            string text;
            text = cardTwo[0].text;
            cardTwo[0].text = cardTwo[1].text;
            cardTwo[1].text = text;
        }

    }
}
