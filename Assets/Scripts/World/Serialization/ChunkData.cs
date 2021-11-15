using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChunkData : MonoBehaviour
{
    int m_x;
    int m_y;

    public Vector2Int position
    {
        get { return new Vector2Int(m_x, m_y); }
        set 
        {
            m_x = value.x;
            m_y = value.y;
        }
    }




}
