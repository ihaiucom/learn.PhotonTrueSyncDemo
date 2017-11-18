using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSyncDebug
{

    public class GamInfo : MonoBehaviour
    {
        private static GamInfo _Install;
        public static GamInfo Install
        {
            get
            {
                if(_Install == null)
                {
                    GameObject go = GameObject.Find("GameMenu");
                    if(go == null)
                    {
                        go = new GameObject("GameSyncDebug");
                    }
                    _Install = go.AddComponent<GamInfo>();
                }
                return _Install;
            }
        }

        private void Awake()
        {
            _Install = this;
        }

        [Header("User")]
        public string   userName;
        public int      userId;

        public string UserName
        {
            get
            {
                if(string.IsNullOrEmpty(userName))
                    userName = PlayerPrefs.GetString("GameSyncDebug.UserName");
                return userName;
            }

            set
            {
                userName = value;
                PlayerPrefs.SetString("GameSyncDebug.UserName", userName);
            }
        }


        public int UserId
        {
            get
            {
                if (userId <= 0)
                    userId = PlayerPrefs.GetInt("GameSyncDebug.userId");
                return userId;
            }

            set
            {
                userId = value;
                PlayerPrefs.SetInt("GameSyncDebug.userId", userId);
            }
        }

        [Header("PUN")]
        public string version = "v1.0";
        public ServerSettings _PhotonServerSettings;
        public ServerSettings PhotonServerSettings
        {
            get
            {
                if(_PhotonServerSettings == null)
                {
                    _PhotonServerSettings = PhotonNetwork.PhotonServerSettings;
                }
                return _PhotonServerSettings;
            }
        }

        public string lobbyName;
        public string LobbyName
        {
            get
            {
                if (string.IsNullOrEmpty(lobbyName))
                    lobbyName = PlayerPrefs.GetString("GameSyncDebug.LobbyName");
                return lobbyName;
            }

            set
            {
                lobbyName = value;
                PlayerPrefs.SetString("GameSyncDebug.LobbyName", lobbyName);
            }
        }


        public string roomName;
        public string RoomName
        {
            get
            {
                if (string.IsNullOrEmpty(roomName))
                    roomName = PlayerPrefs.GetString("GameSyncDebug.RoomName");
                return roomName;
            }

            set
            {
                roomName = value;
                PlayerPrefs.SetString("GameSyncDebug.RoomName", roomName);
            }
        }


        public int roomNum;
        public int RoomNum
        {
            get
            {
                if (roomNum == 0)
                    roomNum = PlayerPrefs.GetInt("GameSyncDebug.RoomNum");
                return roomNum;
            }

            set
            {
                roomNum = value;
                PlayerPrefs.SetInt("GameSyncDebug.RoomNum", roomNum);
            }
        }


        public string roomPlugins;
        public string RoomPlugins
        {
            get
            {
                if (string.IsNullOrEmpty(roomPlugins))
                    roomPlugins = PlayerPrefs.GetString("GameSyncDebug.RoomPlugins");
                return roomPlugins;
            }

            set
            {
                roomPlugins = value;
                PlayerPrefs.SetString("GameSyncDebug.RoomPlugins", roomPlugins);
            }
        }


    }
}