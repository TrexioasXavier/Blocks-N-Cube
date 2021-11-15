using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
    public static float Get2DPerlin(Vector2 position, float offset, float scale)
    {
        return Mathf.PerlinNoise((position.x + 0.1f) / VoxelData.m_chunkWidth * scale + offset, (position.y + 0.1f) / VoxelData.m_chunkWidth * scale + offset);
    }


}
