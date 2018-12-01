using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour{
    public GameObject playerCameraPrefab;
    public List<GameObject> weaponPrefabs;
    public Transform weaponHolder;
    private Camera playerCamera;
    private GameObject weaponGameObject;

    [SyncVar]//Add hook to update various stuff.
    public int health = 100;
    [SyncVar]
    public int damageDone = 0;
    [SyncVar(hook = "OnChangeWeapon")]
    public int weaponId = 0;

    void Start() {
        OnChangeWeapon(weaponId);
        if (!isLocalPlayer){
            return;
        }
        GameObject cameraObject = Instantiate(playerCameraPrefab, transform) as GameObject;
        playerCamera = cameraObject.GetComponent<Camera>();
    }
    void Update(){
        if (!isLocalPlayer){
            return;
        }
        if (Input.GetMouseButton(0)){
            Attack();
        }else if (Input.GetMouseButtonDown(1)){
            CmdChangeWeapon((weaponId == 0) ? 1 : 0);
        }
    }
    void Attack(){
        weaponHolder.GetComponentInChildren<Wepond>().Attack(this, playerCamera.transform.position);
    }
    [Command]
    public void CmdSpawnBullet(GameObject bullet){
        //GameObject b = Instantiate(bullet, playerCamera.transform.position, Quaternion.identity) as GameObject;
        //Bullet force needs to be added here. aka rigidbody.addforce.
        NetworkServer.Spawn(bullet);
    }
    [Command]
    void CmdChangeWeapon(int newId){
        weaponId = newId;
    }
    void OnChangeWeapon(int weaponId){
        this.weaponId = weaponId;
        if (weaponHolder.childCount > 0)
            Destroy(weaponHolder.GetChild(0).gameObject);
        Instantiate(weaponPrefabs[weaponId], weaponHolder);
    }
}
