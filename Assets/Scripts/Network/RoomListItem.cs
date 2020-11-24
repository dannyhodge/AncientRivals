using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    public RoomInfo roomInfo;
    public void SetUp(RoomInfo _roomInfo) {
        text.text = _roomInfo.Name;
        roomInfo = _roomInfo;
    }

    public void OnClick() {
        Launcher.Instance.JoinRoom(roomInfo);
    }
}
