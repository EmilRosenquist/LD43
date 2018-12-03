using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ConnectUI : MonoBehaviour {
    NetworkManager nm;
    [SerializeField] private InputField ipf;
    // Use this for initialization
    void Start () {
        nm = FindObjectOfType<NetworkManager>();

    }
	
    public void Host()
    {
        Debug.Log("host");
        NetworkManager.singleton.StartHost();
    }
    public void Join()
    {
        NetworkManager.singleton.networkPort = 7777;
        if (ipf.text != "")
            NetworkManager.singleton.networkAddress = ipf.text;
        else
            NetworkManager.singleton.networkAddress = "localhost";
        NetworkManager.singleton.StartClient();
    }
}
