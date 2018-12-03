using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public Text healthText;

    public Text MultiplierText1;
    public Text MultiplierText2;
    public Text MultiplierText3;
    public Text MultiplierText4;
    public Text MultiplierText5;

    public Sprite healthSprite;
    public Sprite damageSprite;
    public Sprite speedSprite;
    public Sprite sprintSprite;
    public Sprite jumpHeightSprite;

    public Image perk1Image;
    public Image perk2Image;
    public Image perk3Image;
    public Image perk4Image;
    public Image perk5Image;


    private GameObject playerGameObject;
    private Player player;
    private GameObject playerStatsGameObject;
    private PlayerStats playerStats;

    private List<Image> perksImages = new List<Image>();

    private bool imageAdded = false;

    //Health
    private int health;
    //Basic stats
    private float healthMultiplier = 1.0f;
    private float damageMultiplierMultiplier = 1.0f;
    //MovementStats
    private float speedMultiplier = 1.0f;
    private float sprintMultiplier = 1.0f;
    private float jumpHeightMultiplier = 1.0f;


    void Start()
    {
        player = gameObject.AddComponent<Player>();
        playerStats = gameObject.AddComponent<PlayerStats>();

        perksImages.Add(perk1Image);
        perksImages.Add(perk2Image);
        perksImages.Add(perk3Image);
        perksImages.Add(perk4Image);
        perksImages.Add(perk5Image);
    }

	void Update ()
    {
        //TURN OFF ACTIVE PERKS WHEN NO PERKS ACTIVE

        health = player.health;
        healthText.text = "Health " + health;

        //healthMultiplier = playerStats.healthMultiplier;

        //perk1.sprite = sprite1;
        //perksText.text = "health" + healthMultiplier + "\ndamage" + damageMultiplierMultiplier;

        PerksUpdate();

        //if (CheckHealthMultiplier() != null)
        //{                    
        //    for (int i = 0; i < perksImages.Count; i++)
        //    {
        //        if (perksImages[i].sprite == null)
        //        {
        //            Debug.Log("here I am");
        //            perksImages[i].sprite = CheckHealthMultiplier();
        //        }
        //    }
        //}


        if (CheckHealthMultiplier() != null)
        {
            for (int i = 0; i < perksImages.Count; i++)
            {
                if (perksImages[i].sprite == null && !imageAdded)
                {
                    imageAdded = true;
                    Debug.Log("here I am");
                    perksImages[i].sprite = CheckHealthMultiplier();
                }
            }
        }



        //if (CheckDamageMultiplier() != null)
        //{
        //    if (perk1Image.sprite == null)
        //    {
        //        perk1Image.sprite = CheckDamageMultiplier();
        //    }
        //}

    }

    private void PerksUpdate()
    {
        CheckHealthMultiplier();
        CheckDamageMultiplier();
        CheckSpeedMultiplier();
        CheckSprintMultiplier();
        CheckJumpHeightMultiplier();
    }


    private Sprite CheckHealthMultiplier()
    {
        if (playerStats.healthMultiplier != 1)
        {
            //Debug.Log("checking health");
            healthMultiplier = playerStats.healthMultiplier;
            return healthSprite;
        }
        return null;
    }

    private Sprite CheckDamageMultiplier()
    {
        if (playerStats.damageMultiplierMultiplier != 1)
        {
            damageMultiplierMultiplier = playerStats.damageMultiplierMultiplier;
            Debug.Log("checking damage");
            return damageSprite;
        }
        return null;
    }

    private Sprite CheckSpeedMultiplier()
    {
        if (speedMultiplier != 1)
        {
            speedMultiplier = playerStats.speedMultiplier;
            return speedSprite;
        }
        return null;
    }

    private Sprite CheckSprintMultiplier()
    {
        if (sprintMultiplier != 1)
        {
            sprintMultiplier = playerStats.sprintMultiplier;
            return sprintSprite;
        }
        return null;
    }

    private Sprite CheckJumpHeightMultiplier()
    {
        if (jumpHeightMultiplier != 1)
        {
            jumpHeightMultiplier = playerStats.jumpHeightMultiplier;
            return jumpHeightSprite;
        }
        return null;
    }
}
