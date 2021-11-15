using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.IO;

public class Launcher : MonoBehaviourPunCallbacks
{

    public static Launcher Instance;

    [SerializeField] private Text m_statusText;


    public GameObject m_settingsPanel;
    public GameObject m_menuPanel;
    public GameObject m_roomCreationPanel;
    public GameObject m_lobbySelectionPanel;

    public InputField m_roomName;
    public InputField m_maxPlayersInRoom;

    private string m_status;

    PlayerController m_player;


    public Transform m_roomList;
    public GameObject m_room;

    public GameObject m_inGameMenu;

    public GameObject m_chatWindow;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ConnectionToServer();
        m_roomName.text = "Awesome Room!";
        m_maxPlayersInRoom.text = "10";
            
    }

    private void Update()
    {
        //Updates the status text...
        if(m_statusText != null) 
            m_statusText.text = m_status;

        ToggleInGameMenu();

    }


    public void ConnectionToServer()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log(m_status = "Connecting to Master Server...");
    }

    public void CreateServer()
    {
            
        if (string.IsNullOrEmpty(m_roomName.text))
            return;

        if(m_roomName != null)
            PhotonNetwork.CreateRoom(m_roomName.text);
        StartGame();
    }

    

    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
        //Instantiate(m_inGameMenu);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LeaveRoom()
    {
        if (PhotonNetwork.LeaveRoom())
            Debug.Log(m_status = "Successfully left the current lobby!");
    }

    public void JoinRoom(RoomInfo info)
    {
        Debug.Log(m_status = "Joining Room...");
        
        PhotonNetwork.JoinRoom(info.Name);
                

    }

    public void SpawnEntities()
    {
        //PhotonNetwork.Instantiate(this.m_player.name, );
    }



    public override void OnConnectedToMaster()
    {
        Debug.Log(m_status = "Connected to Master Server!");
        Debug.Log(m_status = "Joining Lobby!");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = "Trexioas Xavier";
        CreateChatWindow();

    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        string err = "Room Creation Failed: " + message;
        Debug.Log(err);

        Debug.Log("Failed to create a room!");
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        string err = "Failed to join room: " + message;
        Debug.Log(err);

        base.OnJoinRoomFailed(returnCode, message);
    }
    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        Debug.Log("Successfully created a room!");
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("Player Entered the room!");

    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (Transform trans in m_roomList)
        {
            Destroy(trans.gameObject);
        }
        for (int i = 0; i < roomList.Count; i++)
        {
            Instantiate(m_room, m_roomList).GetComponent<Room>().Setup(roomList[i]);
        }


    }
    public override void OnLeftRoom()
    {

    }

    public string GetPlayerName()
    {
        return PhotonNetwork.NickName;
    }
    public override void OnJoinedLobby()
    {
        Debug.Log(m_status = "Successfully Joined the lobby!");
        PhotonNetwork.NickName = "Player " + Random.Range(0, 10000).ToString("00000");
    }
    public override void OnJoinedRoom()
    {
        if(m_roomName != null)
            m_roomName.text = PhotonNetwork.CurrentRoom.Name;

        Debug.Log(m_status = "Successfully joined the room!");
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        //startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }



    public void ToggleInGameMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            m_inGameMenu.SetActive(!m_inGameMenu.activeSelf);
    }


    private void CreateChatWindow()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "MultiplayerChat"), new Vector3(-266, -216, 0), Quaternion.identity);
    }











    #region MainMenu
    public void OpenSettings()
    {
        m_menuPanel.SetActive(false);
        m_settingsPanel.SetActive(true);
    }

    public void GotoMenu()
    {
        m_menuPanel.SetActive(true);
        m_roomCreationPanel.SetActive(false);
        m_settingsPanel.SetActive(false);
        m_lobbySelectionPanel.SetActive(false);
    }

    public void OpenRoomCreation()
    {
        m_menuPanel.SetActive(false);
        m_roomCreationPanel.SetActive(true);
    }

    public void OpenLobbySelection()
    {
        m_menuPanel.SetActive(false);
        m_lobbySelectionPanel.SetActive(true);
    }
    #endregion
}

