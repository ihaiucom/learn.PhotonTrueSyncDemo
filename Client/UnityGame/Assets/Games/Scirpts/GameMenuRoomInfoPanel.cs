using Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSyncDebug
{
    public class GameMenuRoomInfoPanel : PunBehaviour
    {
        public Text roomInfoText;
        public GameObject itemPrefab;
        public RectTransform content;
        private Vector2 contentDefaultSize;
        public GameObject leaveButton;

        private List<GameMenuRoomPlayerItem> list = new List<GameMenuRoomPlayerItem>();

        private void OnEnable()
        {
            UpdatePlayerList();
            contentDefaultSize = content.sizeDelta;
        }

        private void Awake()
        {
            itemPrefab.SetActive(false);
        }

        public override void OnJoinedRoom()
        {
            UpdatePlayerList();
        }

        public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
        {
            UpdatePlayerList();
        }


        public override void OnPhotonPlayerDisconnected(PhotonPlayer disconnetedPlayer)
        {
            UpdatePlayerList();
        }

        // updates players position and plane on gui
        public void UpdatePlayerList()
        {
            leaveButton.SetActive(PhotonNetwork.inRoom);
            if (PhotonNetwork.room == null)
            {
                roomInfoText.text = "没有进入房间";
            }
            else
            {
                roomInfoText.text = string.Format("{0}, {1}/{2}, IsLocalClientInside={3}, AutoCleanUp", 
                    PhotonNetwork.room.Name, 
                    PhotonNetwork.room.PlayerCount,
                    PhotonNetwork.room.MaxPlayers,
                    PhotonNetwork.room.IsLocalClientInside,
                    PhotonNetwork.room.AutoCleanUp);
            }

            PhotonPlayer[] playerList = PhotonNetwork.playerList;

            int count = playerList.Length;
            float y = 0;
            for (int i = 0; i < count; i++)
            {
                GameMenuRoomPlayerItem item = null;
                if (i < list.Count)
                {
                    item = list[i];
                }
                else
                {
                    GameObject go = GameObject.Instantiate(itemPrefab);
                    go.transform.SetParent(content, false);
                    item = go.GetComponent<GameMenuRoomPlayerItem>();
                    list.Add(item);
                }

                item.SetY(y);
                y -= item.height;

                item.SetData(i, playerList[i]);
                item.gameObject.SetActive(true);
            }

            for (int i = count; i < list.Count; i++)
            {
                list[i].gameObject.SetActive(false);
            }


            this.content.sizeDelta = new Vector2(this.content.sizeDelta.x, y > contentDefaultSize.y ? y : contentDefaultSize.y);

        }

        public void OnClickExitRoom()
        {
            PhotonNetwork.LeaveRoom();

            GameMenu.Open(PanelType.Lobby);
        }

    }
}
