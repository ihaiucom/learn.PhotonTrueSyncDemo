using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSyncDebug
{

    public class GameMenuRoomCreatePanel : MonoBehaviour
    {

        public InputField roomNameInputField;
        public InputField roomNumInputField;
        public InputField roomPluginsInputField;


        private void OnEnable()
        {
            roomNameInputField.text = GamInfo.Install.RoomName;
            roomNumInputField.text = GamInfo.Install.RoomNum.ToString();
            roomPluginsInputField.text = GamInfo.Install.RoomPlugins;

        }

        public void OnClickCreate()
        {

            string roomName         = roomNameInputField.text;
            string roomPlugins      = roomPluginsInputField.text;
            int roomNum             = roomNumInputField.text.ToInt32();

            GamInfo.Install.RoomName = roomName;
            GamInfo.Install.RoomPlugins = roomPlugins;
            GamInfo.Install.RoomNum = roomNum;


            RoomOptions roomOptions = new RoomOptions();
            roomOptions.MaxPlayers = (byte)roomNum;
            if(!string.IsNullOrEmpty(GamInfo.Install.RoomPlugins))
                roomOptions.Plugins = new string[] { GamInfo.Install.RoomPlugins };

            TypedLobby typedLobby = TypedLobby.Default;
            typedLobby.Name = GamInfo.Install.LobbyName;
            string[] expectedUsers = new string[] { };

            PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, typedLobby, expectedUsers);
            GameMenu.Open(PanelType.RoomInfo);

        }
    }
}
