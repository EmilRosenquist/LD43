using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelect : MonoBehaviour {
    [SerializeField] private GameObject[] skinButtons;
    [SerializeField] private GameObject CharacterSelectUI;
    [SerializeField] private GameManager gm;
    [SerializeField] private InputField nameSelector;
    [SerializeField] private string playerName = "";
    private int selectedSkin = 0;
    public int SelectedSkin
    {
        get { return selectedSkin; }
    }
    public string PlayerName
    {
        get { return playerName; }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (CharacterSelectUI.activeInHierarchy)
        {
            if (gm.gameObject.activeInHierarchy)
            {
                CharacterSelectUI.SetActive(false);
            }
        }
        playerName = nameSelector.text;

    }
    public void SelectSkin(int skinIndex)
    {
        selectedSkin = skinIndex;
        for(int i = 0; i < skinButtons.Length; i++)
        {

            skinButtons[i].transform.Find("Highlighter").GetComponent<Image>().color = Color.clear;
        }
        skinButtons[skinIndex].transform.Find("Highlighter").GetComponent<Image>().color = new Color(0.3224286f, 1f, 0.240566f, 0.3490196f);
    }

}
