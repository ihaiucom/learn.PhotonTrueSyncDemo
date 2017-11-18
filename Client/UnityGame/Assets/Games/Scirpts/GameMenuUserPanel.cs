using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSyncDebug
{

    public class GameMenuUserPanel : MonoBehaviour {

        public InputField userNameText;
        public InputField userIdText;

        private void OnEnable()
        {
            userNameText.text = GamInfo.Install.UserName;
            if(string.IsNullOrEmpty(userIdText.text))
            {
                userIdText.text = (GamInfo.Install.UserId + 1).ToString();
            }
        }

        public void OnClickOk()
        {
            GamInfo.Install.UserName = userNameText.text;
            GamInfo.Install.UserId = userIdText.text.ToInt32();
            PhotonNetwork.playerName = GamInfo.Install.UserName;

            GameMenu.Open(PanelType.PUN);
        }
    }
}