using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour{
    public GameObject playerCameraPrefab;
    private Camera playerCamera;
    void Start(){
        if (!isLocalPlayer){
            return;
        }
        GameObject cameraObject = Instantiate(playerCameraPrefab, transform) as GameObject;
        playerCamera = cameraObject.GetComponent<Camera>();
    }
    void Update(){
        if (Input.GetMouseButtonDown(0)){
            Attack();
        }
    }
    void Attack(){
        if (true){//Has melee weapon or something special for every weapon maybe
            //Raycast
            //If hit,
            //Get PlayerStats.
            //Health - Weapon Damage.
        }else if (true){//Ranged shoot
            //CmdSpawnBullet(weapon.bulletTypeOfThisWeapon);
        }
    }
    [Command]
    void CmdSpawnBullet(GameObject bullet){
        GameObject b = Instantiate(bullet, playerCamera.transform.position, Quaternion.identity) as GameObject;
        //Bullet force needs to be added here. aka rigidbody.addforce.
        NetworkServer.Spawn(b);
    }
}
