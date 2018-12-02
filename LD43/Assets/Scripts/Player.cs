using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : NetworkBehaviour{
    public GameObject playerCameraPrefab;
    public List<GameObject> weaponPrefabs;
    public List<GameObject> bulletPrefabs;
    public List<Texture> skins;
    public Transform weaponHolder;
    private Camera playerCamera;
    private SkinnedMeshRenderer smr;
    private PlayerStats playerStats;

    [SyncVar]
    public int health;
    public int baseHealth = 100;
    private int maxHealth;
    [SyncVar]
    public float speed;
    public float baseSpeed = 5f;
    [SyncVar]
    public float sprintMultiplier;
    public float baseSprintMultiplier = 1.3f;
    [SyncVar]
    public float jumpHeight;
    public float baseJumpHeight = 5f;
    [SyncVar]
    public float damageMultiplier = 1.0f;
    [SyncVar]
    public int money = 0;
    public float moneyMultiplier = 1.0f;

    [SyncVar(hook = "OnChangeSkin")]
    public int skinIndex = 0;
    [SyncVar]
    public string playerName;

    [SyncVar]
    public int damageDone = 0;
    [SyncVar(hook = "OnChangeWeapon")]
    public int weaponId = 0;
    [SyncVar]
    public bool isAlive = true;
    List<int> ids;

    private void Awake()
    {
        smr = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    void Start() { 
        ids = new List<int>();
         
        CmdChangeName(FindObjectOfType<CharacterSelect>().PlayerName);
        if (!isLocalPlayer){
            OnChangeSkin(skinIndex);
            return;
        }
        maxHealth = baseHealth;
        SkinnedMeshRenderer[] tmp = gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer s in tmp)
        {
            if (s.gameObject.CompareTag("char_mesh"))
            {
                s.gameObject.SetActive(false);
            }
        }
        CmdChangeSkin(FindObjectOfType<CharacterSelect>().SelectedSkin);
        GameObject cameraObject = Instantiate(playerCameraPrefab, transform) as GameObject;
        playerCamera = cameraObject.GetComponent<Camera>();
        playerStats = gameObject.AddComponent<PlayerStats>();

        Perk p = Perks.GeneratePerk(1);
        ApplyPerk(p);

        for (int i = 0; i < weaponPrefabs.Count; i++){
            GameObject g = Instantiate(weaponPrefabs[i], weaponHolder);
            g.SetActive(false);
        }
        weaponHolder.GetChild(0).gameObject.SetActive(true);
        OnChangeWeapon(weaponId);
        CmdResetStats();

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
        }
    }
    void Attack(){
        weaponHolder.GetComponentInChildren<Wepond>().Attack(this, weaponHolder.GetChild(weaponId).transform.position, playerCamera.transform.forward);
    }
    public void TakeDamage(int damageAmount) {
        if (!isServer) {
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
    public void DidDamage(int amount){
        if (!isServer)
            return;
        damageDone += amount;
        money += (int)(amount * moneyMultiplier);
    }
    [Command]
    public void CmdSpawnBullet(int bulletId, Vector3 spawnPos, Vector3 direction){
        RpcSpawnBullet(bulletId, spawnPos, direction);
    }

    [ClientRpc]
    void RpcSpawnBullet(int bulletId, Vector3 spawnPos, Vector3 direction){
        GameObject b = Instantiate(bulletPrefabs[bulletId], spawnPos, Quaternion.identity) as GameObject;
        b.GetComponentInChildren<Bullet>().MoveDir = direction;
        b.GetComponentInChildren<Bullet>().shooter = this;
        Destroy(b, 3.0f);
    }
    [Command]
    public void CmdSpawnRail(int bulletId, Vector3 spawnPos, Vector3 endPos)
    {
        RpcSpawnRail( bulletId,  spawnPos,  endPos);
    }
    [ClientRpc]
    void RpcSpawnRail(int bulletId, Vector3 spawnPos, Vector3 endPos)
    {
        GameObject b = Instantiate(bulletPrefabs[bulletId], spawnPos, Quaternion.identity) as GameObject;
        b.GetComponent<RailBullet>().setPositions(spawnPos, endPos);
        b.GetComponentInChildren<Bullet>().shooter = this;
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
    void CmdChangeName(string newPlayerName)
    {
        if(newPlayerName == "" || newPlayerName == null)
        {
            playerName = "Player: " + netId.ToString();
        }
        playerName = newPlayerName;
    }
    [Command]
    void CmdChangeSkin(int newSkinIndex)
    {
        skinIndex = newSkinIndex;
    }
    void OnChangeSkin(int skinIndex)
    {
       smr.material.mainTexture = skins[skinIndex];
    }

    [Command]
    public void CmdToggleSpectatorMode(bool toggle){
        RpcToggleSpectatorMode(toggle);
    }
    [ClientRpc]
    void RpcToggleSpectatorMode(bool toggle){
        isAlive = toggle;
        transform.GetChild(0).gameObject.SetActive(toggle);
        GetComponent<CharacterController>().enabled = toggle;
        smr.enabled = toggle;
    }
    [Command]
    public void CmdResetStats(){
        health = maxHealth;
        RpcResetStats();
    }
    [ClientRpc]
    void RpcResetStats(){
        for (int i = 0; i < weaponHolder.childCount; i++){
            weaponHolder.GetChild(i).GetComponent<Wepond>().Reset();
        }
    }

    /*-----------------------Perks-----------------------*/
    public void ApplyPerk(Perk p){
        p.good.ApplyAbility(this);
        p.bad.ApplyAbility(this);
    }
    //Health
    public void AddToHealthMultiplier(float multiplier){
        playerStats.healthMultiplier += multiplier - 1.0f;
        CmdUpdateMaxHealth(multiplier);
    }
    [Command]void CmdUpdateMaxHealth(float multiplier){
        maxHealth = (int)(baseHealth * multiplier);
    }
    //Damage
    public void AddToDamageMultiplierMultiplier(float multiplier){
        playerStats.damageMultiplierMultiplier += multiplier - 1.0f;
        CmdUpdateDamageMultiplier(playerStats.damageMultiplierMultiplier);
    }
    [Command]void CmdUpdateDamageMultiplier(float multiplier){
        damageMultiplier = 1.0f * multiplier;
    }
    //Speed
    public void AddToSpeedMultiplier(float multiplier){
        playerStats.speedMultiplier += multiplier - 1.0f;
        CmdUpdateSpeedMultiplier(multiplier);
    }
    [Command]void CmdUpdateSpeedMultiplier(float multiplier){
        speed = baseSpeed * multiplier;
    }
    //Sprint
    public void AddToSprintMultiplier(float multiplier){
        playerStats.sprintMultiplier += multiplier - 1.0f;
        CmdUpdateSprintMultiplier(playerStats.sprintMultiplier);
    }
    [Command]void CmdUpdateSprintMultiplier(float multiplier){
        sprintMultiplier = baseSprintMultiplier * multiplier;
    }
    //Jump
    public void AddToJumpHeightMultiplier(float multiplier){
        playerStats.jumpHeightMultiplier += multiplier - 1.0f;
        CmdUpdateJumpHeightMultiplier(playerStats.jumpHeightMultiplier);
    }
    [Command]void CmdUpdateJumpHeightMultiplier(float multiplier){
        jumpHeight = baseJumpHeight * multiplier;
    }
}