using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSyncDebug
{

    public class GameMenuPunPanel : MonoBehaviour {

        public InputField serverIP;
        public InputField serverPort;
        public InputField appId;
        public InputField version;
        public InputField lobbyName;
        public Toggle autoJoinLobby;
        public Toggle enableLobbyStats;
        public Toggle runInBackGround;

        private void OnEnable()
        {
            ServerSettings setting = GamInfo.Install.PhotonServerSettings;
            serverIP.text = setting.ServerAddress;
            serverPort.text = setting.ServerPort.ToString();
            appId.text = setting.AppID.ToString();
            version.text = GamInfo.Install.version;
            lobbyName.text = GamInfo.Install.LobbyName;
            autoJoinLobby.isOn = PhotonNetwork.autoJoinLobby;
            enableLobbyStats.isOn = PhotonNetwork.EnableLobbyStatistics;
            runInBackGround.isOn = Application.runInBackground;
        }

        public void OnClickSetting()
        {
            ServerSettings setting = GamInfo.Install.PhotonServerSettings;
            setting.ServerAddress = serverIP.text;
            setting.ServerPort = serverPort.text.ToInt32();
            setting.AppID = appId.text;
            GamInfo.Install.version = version.text;
            GamInfo.Install.LobbyName = lobbyName.text;
            PhotonNetwork.lobby.Name = lobbyName.text;

            PhotonNetwork.autoJoinLobby = autoJoinLobby.isOn;
            PhotonNetwork.EnableLobbyStatistics = enableLobbyStats.isOn;
            Application.runInBackground = runInBackGround.isOn;

        }

        public void OnClickConnect()
        {
            OnClickSetting();
            ServerSettings setting = GamInfo.Install.PhotonServerSettings;
            PhotonNetwork.ConnectToMaster(setting.ServerAddress, setting.ServerPort, setting.AppID, GamInfo.Install.version);
        }

        public void OnClickJoinLobby()
        {
            PhotonNetwork.JoinLobby(PhotonNetwork.lobby);
            GameMenu.Open(PanelType.RoomCreate);
        }
    }
}