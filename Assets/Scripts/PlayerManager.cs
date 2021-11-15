using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView m_pview;

    // Start is called before the first frame update
    void Awake()
    {
        m_pview = GetComponent<PhotonView>();
    }
    void Start()
    {
        if (m_pview.IsMine)
            CreatePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreatePlayer()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","PlayerController"), Vector3.zero, Quaternion.identity);
    }

}
