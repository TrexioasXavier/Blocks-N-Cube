using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon;
using UnityEngine.UI;
using TMPro;

    public class Room : MonoBehaviour
    {
        public static List<Room> m_rooms;

        public string m_name;
        public int m_playerCount;
        public int m_maxPlayerCount;
        public int m_ping;

        [SerializeField] private Text m_nameText;

        private RoomInfo m_info;

    private void Awake()
    {
        m_rooms = new List<Room>();
    }

    public Room(RoomInfo info)
        {
            Debug.Log("Room Name: " + info.Name);
            m_info = info;
            Setup(info);
        }

    public void OnClick()
    {
        Launcher.Instance.JoinRoom(m_info);
    }

        public void Setup(RoomInfo info)
        {
            
            m_name = info.Name;
            m_nameText.text = m_name;
            m_playerCount = 0;
            m_maxPlayerCount = info.MaxPlayers;
            m_ping = 0;

            Room.m_rooms.Add(this);

        }

    }
