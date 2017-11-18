using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSyncDebug
{
    public class GameMenuLobblyRoomItem : MonoBehaviour
    {
        public Text indexText;
        public Text nameText;
        public Text maxPlayerText;
        public Text stateText;

        public RoomInfo data;

        private RectTransform rectTransform;

        public float height
        {
            get
            {
                if (rectTransform == null)
                    rectTransform = (RectTransform)transform;
                return rectTransform.sizeDelta.y;
            }
        }

        public void SetY(float y)
        {
            if (rectTransform == null)
                rectTransform = (RectTransform) transform;
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, y);
        }

        public void SetData(int index, RoomInfo room)
        {
            data = room;
            indexText.text = index.ToString();
            nameText.text = room.Name;
            maxPlayerText.text = room.PlayerCount + " / " + room.MaxPlayers;
            stateText.text = (room.IsOpen ? "开发" : " ") +
                (room.IsVisible ? " 可见" : " ") +
                (room.IsLocalClientInside ? " 以加入" : "")
                ;
        }

        public void OnClickJoin()
        {
            PhotonNetwork.JoinRoom(data.Name);

            GameMenu.Open(PanelType.RoomInfo);
        }
    }
}