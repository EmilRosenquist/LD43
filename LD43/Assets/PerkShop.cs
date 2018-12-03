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
    List<bool> boughtThisRound = new List<bool>();

    GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        boughtThisRound.Add(false);
        boughtThisRound.Add(false);
        boughtThisRound.Add(false);
    }
    public void ShowShop(Perks perksClass, PerkStruct t1, PerkStruct tierR, PerkStruct t2)
    {
        gm = FindObjectOfType<GameManager>();
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
        boughtThisRound[0] = false;
        boughtThisRound[1] = false;
        boughtThisRound[2] = false;
        currentPerks.Clear();
        perkCardsUI.SetActive(false);
    }
    public void BuyCard(int cardIndex)
    {
        if (!boughtThisRound[cardIndex])
        {
            for(int i = 0; i < gm.playerList.Count; i++)
            {
                if (gm.playerList[i].GetComponent<Player>().isLocalPlayer)
                {
            //Buy Card

                    gm.playerList[i].GetComponent<Player>().ApplyPerk(perksHelper.GetPerkFromStruct(currentPerks[cardIndex]));
                }
            }
            boughtThisRound[cardIndex] = true;
        }
    }
}
