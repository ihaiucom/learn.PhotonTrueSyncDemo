using Photon;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GameSyncDebug
{
    public class GameMenuLobblyPanel : PunBehaviour
    {
        public GameObject itemPrefab;
        public RectTransform content;
        private Vector2 contentDefaultSize;

        private List<GameMenuLobblyRoomItem> list = new List<GameMenuLobblyRoomItem>();

        private void OnEnable()
        {
            OnReceivedRoomListUpdate();
            contentDefaultSize = content.sizeDelta;
        }

        private void Awake()
        {
            itemPrefab.SetActive(false);
        }


        public override void OnReceivedRoomListUpdate()
        {
            RoomInfo[] roomList = PhotonNetwork.GetRoomList();

            int count = roomList.Length;
            float y = 0;
            for(int i = 0; i < count; i ++)
            {
                GameMenuLobblyRoomItem item = null;
                if (i < list.Count)
                {
                    item = list[i];
                }
                else
                {
                    GameObject go = GameObject.Instantiate(itemPrefab);
                    go.transform.SetParent(content, false);
                    item = go.GetComponent<GameMenuLobblyRoomItem>();
                    list.Add(item);
                }

                item.SetY(y);
                y -= item.height;

                item.SetData(i, roomList[i]);
                item.gameObject.SetActive(true);
            }

            for(int i = count; i < list.Count; i ++)
            {
                list[i].gameObject.SetActive(false);
            }


            this.content.sizeDelta = new Vector2(this.content.sizeDelta.x, y > contentDefaultSize.y ? y : contentDefaultSize.y);

        }


        public void OnClickExitRoom()
        {
            PhotonNetwork.LeaveLobby();

            GameMenu.Open(PanelType.PUN);
        }
    }
}
