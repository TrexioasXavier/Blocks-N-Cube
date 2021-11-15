using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{

    [HideInInspector]
    public string m_objectID;

    private void Awake()
    {
        m_objectID = name + ", " + transform.position.ToString() + ", " + transform.eulerAngles.ToString();
    }
    void Start()
    {
        for (int i = 0; i < Object.FindObjectsOfType<DontDestroyOnLoad>().Length; i++)
        {
            if(Object.FindObjectsOfType<DontDestroyOnLoad>()[i] != this)
                if (Object.FindObjectsOfType<DontDestroyOnLoad>()[i].name == gameObject.name)
                    Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);        
    }

}
