using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon;
using Photon.Pun;


namespace BNC
{
    public class ServerSection : Photon.Pun.MonoBehaviourPun
    {
        public InputField m_serverName;


        private void Start()
        {

        }

        public void CreateRoom()
        {
            PhotonNetwork.CreateRoom(m_serverName.text);
        }


    } 
}
