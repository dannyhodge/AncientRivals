using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    public int playerIndex = 0;

    void Awake() {
        if(Instance) {
            Destroy(gameObject);
            return; 
        }
        DontDestroyOnLoad(gameObject);
        Instance = this;
        
        PhotonNetwork.SendRate = 30;
    }
    
    public override void OnEnable() {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;    
    }

    public override void OnDisable() {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;    
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        foreach(var player in PhotonNetwork.PlayerList) {
            if(player.NickName == PhotonNetwork.NickName) {
                playerIndex = Array.IndexOf(PhotonNetwork.PlayerList, player);
                Debug.Log(playerIndex);
            }
        }
        if(scene.buildIndex == 1) {
            var PM = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
            PM.GetComponent<PlayerManager>().order = playerIndex;
        }
    }

}
