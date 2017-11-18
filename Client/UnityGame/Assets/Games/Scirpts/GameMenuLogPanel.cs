using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace GameSyncDebug
{

    public class GameMenuLogPanel : MonoBehaviour
    {
        public enum LogInfoType
        {
            None,
            Networking,
        }

        static GameMenuLogPanel Install;

        private void Awake()
        {
            Install = this;
            defaultSize = content.sizeDelta;
        }

        static StringBuilder sb = new StringBuilder();
        static bool needUpdate = false;
        public static void Log(string msg)
        {
            needUpdate = true;
            sb.AppendLine(msg);
        }

        public static void LogFormat(string msg, params object[] args)
        {
            needUpdate = true;
            sb.AppendLine(string.Format(msg, args));
        }

        public static void Clear()
        {
            needUpdate = true;
            sb.Length = 0;
        }


        private LogInfoType logInfoType;
        public Toggle refresh;
        public InputField inputField;
        public RectTransform content;
        public Text text;
        private Vector2 defaultSize;

        private void Update()
        {
            if(needUpdate)
            {
                needUpdate = false;
                inputField.text = sb.ToString();
                if(text.preferredHeight > defaultSize.y)
                {
                    content.sizeDelta = new Vector2(content.sizeDelta.x, text.preferredHeight);
                }
                else
                {
                    content.sizeDelta = defaultSize;
                }
            }

            if(refresh.isOn)
            {

                switch (logInfoType)
                {
                    case LogInfoType.Networking:
                        Clear();
                        Networking();
                        break;
                }
            }
        }

        public void OnClickClear()
        {
            Clear();
        }

        public void OpenLogNetworking()
        {
            logInfoType = LogInfoType.Networking;
            Networking();
            gameObject.SetActive(true);
        }

        private void Networking()
        {
            LogFormat("PhotonNetwork.connectionState= {0}", PhotonNetwork.connectionState);
            LogFormat("PhotonNetwork.connecting= {0}", PhotonNetwork.connecting);
            LogFormat("PhotonNetwork.connected= {0}", PhotonNetwork.connected);
            LogFormat("PhotonNetwork.connectedAndReady= {0}", PhotonNetwork.connectedAndReady);
            LogFormat("PhotonNetwork.connectionStateDetailed= {0}", PhotonNetwork.connectionStateDetailed);


            LogFormat("PhotonNetwork.CrcCheckEnabled= {0}", PhotonNetwork.CrcCheckEnabled);
            LogFormat("PhotonNetwork.inRoom= {0}", PhotonNetwork.inRoom);
            LogFormat("PhotonNetwork.isNonMasterClientInRoom= {0}", PhotonNetwork.isNonMasterClientInRoom);

            LogFormat("PhotonNetwork.isMasterClient= {0}", PhotonNetwork.isMasterClient);
            LogFormat("PhotonNetwork.insideLobby= {0}", PhotonNetwork.insideLobby);
            LogFormat("PhotonNetwork.playerName= {0}", PhotonNetwork.playerName);
            



            LogFormat("");
            if (PhotonNetwork.lobby == null)
            {
                LogFormat("PhotonNetwork.lobby= {0}", PhotonNetwork.lobby);
            }
            else
            {
                LogFormat("lobby.Name= {0}", PhotonNetwork.lobby.Name);
                LogFormat("lobby.Type= {0}", PhotonNetwork.lobby.Type);
            }


            LogFormat("");
            if (PhotonNetwork.LobbyStatistics == null)
            {
                LogFormat("PhotonNetwork.LobbyStatistics= {0}", PhotonNetwork.LobbyStatistics);
            }
            else
            {
                LogFormat("LobbyStatistics.Count= {0}", PhotonNetwork.LobbyStatistics.Count);
                for(int i = 0; i < PhotonNetwork.LobbyStatistics.Count; i ++)
                {
                    LogFormat("-----");
                    TypedLobbyInfo item = PhotonNetwork.LobbyStatistics[i];
                    LogFormat("LobbyStatistics[{0}].Name= {1}", i, item.Name);
                    LogFormat("LobbyStatistics[{0}].Name= {1}", i, item.Type);
                    LogFormat("LobbyStatistics[{0}].PlayerCount= {1}", i, item.PlayerCount);
                    LogFormat("LobbyStatistics[{0}].RoomCount= {1}", i, item.RoomCount);
                }
            }


            LogFormat("");
            if (PhotonNetwork.room == null)
            {
                LogFormat("PhotonNetwork.room= {0}", PhotonNetwork.room);
            }
            else
            {
                LogFormat("room.Name= {0}", PhotonNetwork.room.Name);
                LogFormat("room.IsOpen= {0}", PhotonNetwork.room.IsOpen);
                LogFormat("room.IsVisible= {0}", PhotonNetwork.room.IsVisible);
                LogFormat("room.MaxPlayers= {0}", PhotonNetwork.room.MaxPlayers);
                LogFormat("room.PlayerCount= {0}", PhotonNetwork.room.PlayerCount);
                LogFormat("room.IsLocalClientInside= {0}", PhotonNetwork.room.IsLocalClientInside);
                LogFormat("room.MasterClientId= {0}", PhotonNetwork.room.MasterClientId);
                LogFormat("room.AutoCleanUp= {0}", PhotonNetwork.room.AutoCleanUp);
            }


            LogFormat("");
            RoomInfo[] roomList = PhotonNetwork.GetRoomList();
            LogFormat("PhotonNetwork.GetRoomList().Length= {0}", roomList.Length);
            for (int i = 0; i < roomList.Length; i++)
            {
                LogFormat("-----");
                RoomInfo item = roomList[i];

                LogFormat("roomList[{0}] Name= {1}, IsOpen={1}, IsVisible={2}, MaxPlayers={3}, PlayerCount={4}, isLocalClientInside={5}", 
                    i, 
                    item.Name, 
                    item.IsOpen, 
                    item.IsVisible, 
                    item.MaxPlayers,
                    item.PlayerCount,
                    item.IsLocalClientInside
                    );
            }

            LogFormat("");
            if (PhotonNetwork.player == null)
            {
                LogFormat("PhotonNetwork.player= {0}", PhotonNetwork.player);
            }
            else
            {
                LogFormat("player.ID= {0}", PhotonNetwork.player.ID);
                LogFormat("player.NickName= {0}", PhotonNetwork.player.NickName);
                LogFormat("player.IsLocal= {0}", PhotonNetwork.player.IsLocal);
                LogFormat("player.IsMasterClient= {0}", PhotonNetwork.player.IsMasterClient);
                LogFormat("player.IsInactive= {0}", PhotonNetwork.player.IsInactive);
            }



            LogFormat("");
            LogFormat("PhotonNetwork.playerList.Length= {0}", PhotonNetwork.playerList.Length);
            for (int i = 0; i < PhotonNetwork.playerList.Length; i++)
            {
                LogFormat("-----");
                PhotonPlayer item = PhotonNetwork.playerList[i];
                LogFormat("playerList[{0}].ID= {1}", i, item.ID);
                LogFormat("playerList[{0}].NickName= {1}", i, item.NickName);
                LogFormat("playerList[{0}].IsLocal= {1}", i, item.IsLocal);
                LogFormat("playerList[{0}].IsMasterClient= {1}", i, item.IsMasterClient);
                LogFormat("playerList[{0}].IsInactive= {1}", i, item.IsInactive);
            }
        }

    }
}