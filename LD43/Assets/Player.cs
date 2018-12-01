using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour{
    public GameObject playerCamera;
    void Start(){
        if (!isLocalPlayer){
            return;
        }
        Instantiate(playerCamera, transform);
    }
}
