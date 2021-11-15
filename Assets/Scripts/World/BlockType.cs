using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BlockType
{
    public string m_blockName;
    public bool m_isSolid;
    public int m_durability;
    public int m_explosiveResistence;

    [Header("Texture Values")]
    public FaceTextureID m_facetextureID;

    public int GetTextureID(int faceIndex)
    {
        switch (faceIndex)
        {
            case 0:
                return m_facetextureID.m_backFaceTexture;
            case 1:
                return m_facetextureID.m_frontFaceTexture;
            case 2:
                return m_facetextureID.m_topFaceTexture;
            case 3:
                return m_facetextureID.m_bottomFaceTexture;
            case 4:
                return m_facetextureID.m_leftFaceTexture;
            case 5:
                return m_facetextureID.m_rightFaceTexture;

            default:
                Debug.Log("Error in textureID; invalid texture index given!!!");
                return 0;
        }
    }
}

[System.Serializable]
public struct FaceTextureID
{
    public int m_frontFaceTexture;
    public int m_backFaceTexture;
    public int m_topFaceTexture;
    public int m_bottomFaceTexture;
    public int m_leftFaceTexture;
    public int m_rightFaceTexture;
}
