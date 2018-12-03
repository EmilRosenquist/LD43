using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

    public Text healthText;
    public Text ammoText;

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

    public Image backgroundTint1Image;
    public Image backgroundTint2Image;
    public Image backgroundTint3Image;
    public Image backgroundTint4Image;
    public Image backgroundTint5Image;


    private GameManager gm;
    //private GameObject playerGameObject;
    private Player player;
    //private GameObject playerStatsGameObject;
    private PlayerStats playerStats;

    private List<Image> perksImages = new List<Image>();
    private List<Text> perksText = new List<Text>();
    private List<Image> backgroundTintImages = new List<Image>();

    private bool imageAdded1 = false;
    private bool imageAdded2 = false;
    private bool imageAdded3 = false;
    private bool imageAdded4 = false;
    private bool imageAdded5 = false;

    //Health
    private int health;
    //Ammo
    private int loadedAmmo;
    private int reserveAmmo;
    //Basic stats
    private float healthMultiplier = 1.0f;
    private float damageMultiplier = 1.0f;
    //MovementStats
    private float speedMultiplier = 1.0f;
    private float sprintMultiplier = 1.0f;
    private float jumpHeightMultiplier = 1.0f;


    void Start()
    {
        if (!gm)
        {
            gm = FindObjectOfType<GameManager>();
        }

        Player[] players = FindObjectsOfType<Player>();

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].isLocalPlayer)
            {
                player = players[i];
                playerStats = player.GetComponent<PlayerStats>();
            }
        }

        perksImages.Add(perk1Image);
        perksImages.Add(perk2Image);
        perksImages.Add(perk3Image);
        perksImages.Add(perk4Image);
        perksImages.Add(perk5Image);

        perksText.Add(MultiplierText1);
        perksText.Add(MultiplierText2);
        perksText.Add(MultiplierText3);
        perksText.Add(MultiplierText4);
        perksText.Add(MultiplierText5);

        backgroundTintImages.Add(backgroundTint1Image);
        backgroundTintImages.Add(backgroundTint2Image);
        backgroundTintImages.Add(backgroundTint3Image);
        backgroundTintImages.Add(backgroundTint4Image);
        backgroundTintImages.Add(backgroundTint5Image);
    }

	void Update ()
    {
        if (player)
        {
            loadedAmmo = player.transform.Find("Camera(Clone)/WeaponHolder").GetChild(player.weaponId).GetComponent<Wepond>().loadedAmmo;
            reserveAmmo = player.transform.Find("Camera(Clone)/WeaponHolder").GetChild(player.weaponId).GetComponent<Wepond>().reserveAmmo;

            if (player.transform.Find("Camera(Clone)/WeaponHolder").GetChild(player.weaponId).GetComponent<Wepond>().infiniteAmmo)
                ammoText.text = "∞/∞";
            else
                ammoText.text = loadedAmmo + "/" + reserveAmmo;

            health = player.health;
            healthText.text = health.ToString();



            PerksUpdate();

            ImageUpdate();
        }
        else
        {
            Player[] players = FindObjectsOfType<Player>();

            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].isLocalPlayer)
                {
                    player = players[i];
                    playerStats = player.GetComponent<PlayerStats>();
                }
            }
        }

    }

    public void Setgm(GameManager Gm)
    {
        gm = Gm;
    }
    public void SetPlayer(Player p)
    {
        player = p;
    }

    private void PerksUpdate()
    {
        CheckHealthMultiplier();
        CheckDamageMultiplier();
        CheckSpeedMultiplier();
        CheckSprintMultiplier();
        CheckJumpHeightMultiplier();
    }

    private void ImageUpdate()
    {
        if (CheckHealthMultiplier() != null)
        {
            for (int i = 0; i < perksImages.Count; i++)
            {
                if (perksImages[i].sprite == null && !imageAdded1)
                {
                    imageAdded1 = true;
                    perksImages[i].sprite = CheckHealthMultiplier();
                    //perksText[i].color = new Color(221, 120, 120, 255);

                    if (healthMultiplier >= 100)
                    {
                        perksText[i].color = Color.white;
                    }

                    perksText[i].text = healthMultiplier.ToString() + "%";
                    backgroundTintImages[i].enabled = true;
                }
            }
        }


        if (CheckDamageMultiplier() != null)
        {
            for (int i = 0; i < perksImages.Count; i++)
            {
                if (perksImages[i].sprite == null && !imageAdded2)
                {
                    imageAdded2 = true;
                    perksImages[i].sprite = CheckDamageMultiplier();

                    if (damageMultiplier >= 100)
                    {
                        perksText[i].color = Color.white;
                    }

                    perksText[i].text = damageMultiplier.ToString() + "%";
                    backgroundTintImages[i].enabled = true;
                }
            }
        }


        if (CheckSpeedMultiplier() != null)
        {
            for (int i = 0; i < perksImages.Count; i++)
            {
                if (perksImages[i].sprite == null && !imageAdded3)
                {
                    imageAdded3 = true;
                    perksImages[i].sprite = CheckSpeedMultiplier();

                    if (speedMultiplier >= 100)
                    {
                        perksText[i].color = Color.white;
                    }

                    perksText[i].text = speedMultiplier.ToString() + "%";
                    backgroundTintImages[i].enabled = true;
                }
            }
        }


        if (CheckSprintMultiplier() != null)
        {
            for (int i = 0; i < perksImages.Count; i++)
            {
                if (perksImages[i].sprite == null && !imageAdded4)
                {
                    imageAdded4 = true;
                    perksImages[i].sprite = CheckSprintMultiplier();

                    if (sprintMultiplier >= 100)
                    {
                        perksText[i].color = Color.white;
                    }

                    perksText[i].text = sprintMultiplier.ToString() + "%";
                    backgroundTintImages[i].enabled = true;
                }
            }
        }


        if (CheckJumpHeightMultiplier() != null)
        {
            for (int i = 0; i < perksImages.Count; i++)
            {
                if (perksImages[i].sprite == null && !imageAdded5)
                {
                    imageAdded5 = true;
                    perksImages[i].sprite = CheckJumpHeightMultiplier();

                    if (jumpHeightMultiplier >= 100)
                    {
                        perksText[i].color = Color.white;
                    }

                    perksText[i].text = jumpHeightMultiplier.ToString() + "%";
                    backgroundTintImages[i].enabled = true;
                }
            }
        }
    }


    private Sprite CheckHealthMultiplier()
    {
        if (playerStats.healthMultiplier != 1)
        {
            healthMultiplier = playerStats.healthMultiplier * 100;
            return healthSprite;
        }
        return null;
    }


    private Sprite CheckDamageMultiplier()
    {
        if (playerStats.damageMultiplierMultiplier != 1)
        {
            damageMultiplier = playerStats.damageMultiplierMultiplier * 100;
            return damageSprite;
        }
        return null;
    }

    private Sprite CheckSpeedMultiplier()
    {
        if (playerStats.speedMultiplier != 1)
        {
        Debug.Log("here");
            speedMultiplier = playerStats.speedMultiplier * 100;
            return speedSprite;
        }
        return null;
    }

    private Sprite CheckSprintMultiplier()
    {
        if (playerStats.sprintMultiplier != 1)
        {
            sprintMultiplier = playerStats.sprintMultiplier * 100;
            return sprintSprite;
        }
        return null;
    }

    private Sprite CheckJumpHeightMultiplier()
    {
        if (playerStats.jumpHeightMultiplier != 1)
        {
            jumpHeightMultiplier = playerStats.jumpHeightMultiplier * 100;
            return jumpHeightSprite;
        }
        return null;
    }
}
