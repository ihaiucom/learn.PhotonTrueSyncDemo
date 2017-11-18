using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameSyncDebug
{
    public enum PanelType { Lobby, User, PUN, Sync, RoomCreate, RoomInfo, Log };

    public class GameMenu : MonoBehaviour
    {
        public static GameMenu Install;

        private void Awake()
        {
            Install = this;
        }



        [Header("Panel")]
        public GameObject lobbyPanel;
        public GameObject userPanel;
        public GameObject punPanel;
        public GameObject syncPanel;
        public GameObject roomCreatePanel;
        public GameObject roomInfoPanel;
        public GameObject logPanel;

        private Dictionary<PanelType, GameObject> _panelDict;
        private Dictionary<PanelType, GameObject> panelDict
        {
            get
            {
                if(_panelDict == null)
                {
                    _panelDict = new Dictionary<PanelType, GameObject>();
                    _panelDict.Add(PanelType.Lobby, lobbyPanel);
                    _panelDict.Add(PanelType.User, userPanel);
                    _panelDict.Add(PanelType.PUN, punPanel);
                    _panelDict.Add(PanelType.Sync, syncPanel);
                    _panelDict.Add(PanelType.RoomCreate, roomCreatePanel);
                    _panelDict.Add(PanelType.RoomInfo, roomInfoPanel);
                    _panelDict.Add(PanelType.Log, logPanel);
                }
                return _panelDict;
            }
        }

        void Start ()
        {
            OpenMenu(PanelType.User);
	    }
	


        public void OpenMenu(string panelName)
        {
            foreach (var kvp in panelDict)
            {
                kvp.Value.SetActive(kvp.Value.name == panelName);
            }
        }

        public void OpenMenu(PanelType panelType)
        {
            foreach (var kvp in panelDict)
            {
                kvp.Value.SetActive(kvp.Key == panelType);
            }
        }

        public static void Open(PanelType panelType)
        {
            Install.OpenMenu(panelType);
        }



        public static void Log(string msg)
        {
            GameMenuLogPanel.Log(msg);
        }


        public static void LogClear()
        {
            GameMenuLogPanel.Clear();
        }

        public Text stateText;
        private void Update()
        {
            stateText.text = string.Format("{0}, {1},     Lobby:{2},   Room:{3},    NickName:{4}", 
                PhotonNetwork.connectionState,
                PhotonNetwork.connectionStateDetailed,
                PhotonNetwork.lobby.Name,
                PhotonNetwork.room != null ? PhotonNetwork.room.Name : "",
                PhotonNetwork.player.NickName
                );
        }
    }

}