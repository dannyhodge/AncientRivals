using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviourPunCallbacks
{
    PhotonView PV;
    GameManager GM;
    gameState GS;
    public Transform SpawnPoint;
    public float time = 0.1f;
    public float time2 = 0.1f;
    public string characterPath;
    public int order;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        GM = GameObject.Find("_GameManager").GetComponent<GameManager>();
        GS = GameObject.Find("_GameManager").GetComponent<gameState>();
        time = Random.Range(0, 1f);
        time2 = Random.Range(0, 1f);
    }

    void Start()
    {
        if(PV.IsMine) CreateController();
    }

    void CreateController() {
        PV.RPC("GetSpawnPoint", RpcTarget.All);
        PV.RPC("GetCharacter", RpcTarget.All);
        PV.RPC("AddCharacterToGameState", RpcTarget.All);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", characterPath), SpawnPoint.position, SpawnPoint.rotation);
    }

    [PunRPC]
    void GetSpawnPoint() {
        // while (time > 0f) {
        //   time -= Time.deltaTime;
        // } 
        SpawnPoint = GM.GetSpawnPoint(order);       
    }

    [PunRPC]
    void GetCharacter() {
        // while (time2 > 0f) {
        //   time2 -= Time.deltaTime;
        // } 
        characterPath = GM.GetCharacterPrefabPath(order);    
        Debug.Log("charpath: " + characterPath);
    }

    [PunRPC]
    void AddCharacterToGameState() {
       GS.scores.Add(new PlayerScore { playerName = PhotonNetwork.NickName, score = 0, alive = true });  
    }


}
