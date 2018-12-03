using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerkShop : MonoBehaviour {
    public List<PerkStruct> currentPerks = new List<PerkStruct>();
    Perks perksHelper;
    [SerializeField] private GameObject perkCardsUI;
    [SerializeField] private Text[] cardsOne;
    [SerializeField] private Text[] cardsTwo;
    [SerializeField] private Text timerText;
    [SerializeField] private Text moneyText;
    [SerializeField] private List<Image> overlays;
    bool boughtThisRound = false;

    Color c = new Color(0, 0.8301887f, 0.01823294f, 0.5f);

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
        perksHelper = perksClass;
        currentPerks.Add(t1);
        currentPerks.Add(tierR);
        currentPerks.Add(t2);
        //set texts
        cardsOne[0].text = perksClass.GetPerkFromStruct(t1).good.GetAbilityString();
        cardsOne[1].text = perksClass.GetPerkFromStruct(t1).bad.GetAbilityString();
        cardsTwo[0].text = perksClass.GetPerkFromStruct(t2).good.GetAbilityString();
        cardsTwo[1].text = perksClass.GetPerkFromStruct(t2).bad.GetAbilityString();
        perkCardsUI.SetActive(true);
    }

    
    public void HideShop()
    {
        boughtThisRound = false;
        currentPerks.Clear();
        overlays[0].color = Color.clear;
        overlays[1].color = Color.clear;
        overlays[2].color = Color.clear;
        timerText.text = "";
        moneyText.text = "";
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
                    if (cardIndex == 0 && p.money >= 100)
                    {
                        p.money -= 100;
                        moneyText.text = "Money: " + p.money;
                        p.ApplyPerk(perksHelper.GetPerkFromStruct(currentPerks[cardIndex]));
                        overlays[i].color = c;
                        boughtThisRound = true;
                    }
                    else if (cardIndex == 1 &&p.money >= 50)
                    {
                        p.money -= 50;
                        moneyText.text = "Money: " + p.money;
                        p.ApplyPerk(perksHelper.GetPerkFromStruct(currentPerks[cardIndex]));
                        overlays[i].color = c;
                        boughtThisRound = true;
                    }
                    else if (cardIndex == 2 && p.money >= 200)
                    {
                        p.money -= 200;
                        moneyText.text = "Money: " + p.money;
                        p.ApplyPerk(perksHelper.GetPerkFromStruct(currentPerks[cardIndex]));
                        overlays[i].color = c;
                        boughtThisRound = true;
                    }
                    
                }
            }
            
        }
    }
    public void UpdateTimerText(int timeLeft)
    {
        timerText.text = timeLeft.ToString();
    }
}
