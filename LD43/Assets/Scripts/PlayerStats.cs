using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour {
    [SyncVar]//Add hook to update various stuff.
    public int health = 100;
}
