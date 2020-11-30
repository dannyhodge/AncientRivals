using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if(PV.IsMine) CreateController();
    }

    void CreateController() {
        if(PhotonNetwork.IsMasterClient) {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Character"), new Vector3(-4.8f, -3.8f, 2f), Quaternion.identity);
        }
        else {
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Character"), new Vector3(4.8f, -3.8f, 2f), Quaternion.identity);
        }
    }
}
