using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    public GameObject m_testPrefab;

    private void Start()
    {
        if (Launcher.Instance)
        {
            Instantiate(m_testPrefab, this.transform);
            Debug.Log("Launcher Still Exists!");
        }
        else
            Debug.Log("Launcher doesn't Exist!");
    }



}
