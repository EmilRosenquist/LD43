using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour{
    public GameObject playerCameraPrefab;
    public List<GameObject> weaponPrefabs;
    public List<GameObject> bulletPrefabs;
    public Transform weaponHolder;
    private Camera playerCamera;
    private GameObject weaponGameObject;

    [SyncVar]
    public float speed = 5f;
    [SyncVar]
    public float sprintMultiplier = 1.3f;
    [SyncVar]
    public float jumpHeight = 5f;

    [SyncVar]//Add hook to update various stuff.
    public int health = 100;
    [SyncVar]
    public int damageDone = 0;
    [SyncVar(hook = "OnChangeWeapon")]
    public int weaponId = 0;
    [SyncVar]
    public bool isAlive = true;
    List<int> ids;
    void Start() { 
        ids = new List<int>();
        if (!isLocalPlayer){
            return;
        }
        GameObject cameraObject = Instantiate(playerCameraPrefab, transform) as GameObject;
        playerCamera = cameraObject.GetComponent<Camera>();
        for(int i = 0; i < weaponPrefabs.Count; i++){
            GameObject g = Instantiate(weaponPrefabs[i], weaponHolder);
            g.SetActive(false);
        }
        weaponHolder.GetChild(0).gameObject.SetActive(true);
        OnChangeWeapon(weaponId);
    }
    void Update(){
        if (!isLocalPlayer){
            return;
        }
        if (isAlive){
            if (Input.GetMouseButton(0)){
                Attack();
            }
            else if (Input.GetMouseButtonDown(1)){
                CmdChangeWeapon((weaponId == 0) ? 1 : 0);
            }
        }else{
            if (Input.GetMouseButtonDown(1)){
                NetworkInstanceId test = new NetworkInstanceId((uint)ids[0]);

                GameObject te = NetworkServer.FindLocalObject(test);
                transform.position = te.transform.position;
                transform.rotation = te.transform.rotation;
            }
        }
    }
    void Attack(){
        weaponHolder.GetComponentInChildren<Wepond>().Attack(this, weaponHolder.GetChild(weaponId).transform.position, playerCamera.transform.forward);
    }
    public void TakeDamage(int damageAmount) {
        if (!isServer) {
            CmdTakeDamage(damageAmount);
            return;
        }
        health -= damageAmount;
        if(health <= 0){
            isAlive = false;
            CmdToggleSpectatorMode(false);
            Debug.Log("Dead!");
        }
    }
    [Command]
    public void CmdTakeDamage(int damageAmount){
        this.health -= damageAmount;
    }
    [Command]
    public void CmdSpawnBullet(int bulletId, Vector3 spawnPos, Vector3 direction){
        RpcSpawnBullet(bulletId, spawnPos, direction);
    }
    [ClientRpc]
    void RpcSpawnBullet(int bulletId, Vector3 spawnPos, Vector3 direction){
        GameObject b = Instantiate(bulletPrefabs[bulletId], spawnPos, Quaternion.identity) as GameObject;
        b.GetComponentInChildren<Bullet>().MoveDir = direction;
        Destroy(b, 3.0f);
    }
    [Command]
    void CmdChangeWeapon(int newId){
        weaponId = newId;
    }
    void OnChangeWeapon(int weaponId){
        weaponHolder.GetChild(this.weaponId).gameObject.SetActive(false);
        this.weaponId = weaponId;
        weaponHolder.GetChild(weaponId).gameObject.SetActive(true);
    }
    [Command]
    void CmdToggleSpectatorMode(bool toggle){
        RpcToggleSpectatorMode(toggle);
    }
    [ClientRpc]
    void RpcToggleSpectatorMode(bool toggle){
        isAlive = toggle;
        transform.GetChild(0).gameObject.SetActive(toggle);
        GetComponent<CharacterController>().enabled = toggle;
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = toggle;
    }
}