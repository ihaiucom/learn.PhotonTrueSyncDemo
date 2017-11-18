using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSyncDebug
{
    public class GameMenuRoomPlayerItem : MonoBehaviour
    {
        public Text indexText;
        public Text nameText;
        public Text stateText;

        public PhotonPlayer data;

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

        public void SetData(int index, PhotonPlayer player)
        {
            data = player;
            indexText.text = index.ToString();
            nameText.text = player.NickName;
            stateText.text = string.Format("IsLocal={0}, IsInactive={1}, IsMasterClient={2}", player.IsLocal, player.IsInactive, player.IsMasterClient);
        }

    }
}