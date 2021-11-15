using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VoxelData
{
    public static readonly int m_numOfChunksInWorld = 5;
    public static readonly int m_chunkWidth = 10;
    public static readonly int m_chunkHeight = 10;
    public static readonly int m_textureSizeInBlocks = 4; // Number of Block textures in a imagine file

    public static int m_worldSizeInVoxels
    {
        get { return m_numOfChunksInWorld * m_chunkWidth; }
    }

    public static float m_normalizedBlockTextureSize {
        get { return 1.0f / (float)m_textureSizeInBlocks; }
    }



    
     public static readonly Vector3[] m_vertices = new Vector3[8] {
        new Vector3(0.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 1.0f, 0.0f),
        new Vector3(0.0f, 1.0f, 0.0f),
        new Vector3(0.0f, 0.0f, 1.0f),
        new Vector3(1.0f, 0.0f, 1.0f),
        new Vector3(1.0f, 1.0f, 1.0f),
        new Vector3(0.0f, 1.0f, 1.0f)
    };
    

    public static readonly Vector3[] m_faceChecks = new Vector3[6]
    {
        new Vector3(0.0f, 0.0f, -1.0f),
        new Vector3(0.0f, 0.0f, 1.0f),
        new Vector3(0.0f, 1.0f, 0.0f),
        new Vector3(0.0f, -1.0f, 0.0f),
        new Vector3(-1.0f, 0.0f, 0.0f),
        new Vector3(1.0f, 0.0f, 0.0f),

    };

    public static readonly int[,] m_voxelTriangles = new int[6, 4] {

        // 0 1 2 2 1 3
		{0, 3, 1, 2}, // Back Face
		{5, 6, 4, 7}, // Front Face
		{3, 7, 2, 6}, // Top Face
		{1, 5, 0, 4}, // Bottom Face
		{4, 7, 0, 3}, // Left Face
		{1, 2, 5, 6} // Right Face
    
    };

    public static readonly Vector2[] m_uvs = new Vector2[4] {
        new Vector2 (0.0f, 0.0f),
        new Vector2 (0.0f, 1.0f),
        new Vector2 (1.0f, 0.0f),
        new Vector2 (1.0f, 1.0f)
    };
}

public class ChunkCoordinates 
{
    public Vector2 m_chunkPos;

    public ChunkCoordinates(Vector2 positionForChunk)
    {
        m_chunkPos = positionForChunk;
    }

    public ChunkCoordinates(int x, int y)
    {
        m_chunkPos = new Vector3(x, y);
    }

    public bool Equals(ChunkCoordinates c)
    {
        if (c == null)
            return false;
        else if (c.m_chunkPos.x == m_chunkPos.x && c.m_chunkPos.y == m_chunkPos.y)
            return true;
        else 
            return false;
    }
}
