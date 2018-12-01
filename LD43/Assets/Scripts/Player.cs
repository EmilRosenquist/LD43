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

    void Start() {
        OnChangeWeapon(weaponId);
        if (!isLocalPlayer){
            return;
        }
        GameObject cameraObject = Instantiate(playerCameraPrefab, transform) as GameObject;
        Debug.Log(cameraObject);
        playerCamera = cameraObject.GetComponent<Camera>();
    }
    void Update(){
        if (!isLocalPlayer){
            return;
        }
        if (Input.GetMouseButtonDown(0)){
            Attack();
        }else if (Input.GetMouseButtonDown(1)){
            CmdChangeWeapon((weaponId == 0) ? 1 : 0);
        }
    }
    void Attack(){
        weaponHolder.GetComponentInChildren<Wepond>().Attack(this, playerCamera.transform.position, playerCamera.transform.forward);
    }
    public void TakeDamage(int damageAmount){
        if (!isServer)
            return;
        this.health -= damageAmount;
    }
    [Command]
    public void CmdSpawnBullet(int bulletId, Vector3 spawnPos, Vector3 direction){
        GameObject b = Instantiate(bulletPrefabs[0], spawnPos, Quaternion.identity) as GameObject;
        b.GetComponentInChildren<Bullet>().MoveDir = direction;
        NetworkServer.Spawn(b);
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
}
