using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BNC
{
    public class Terrain : MonoBehaviour
    {
        //private List<Chunk> m_terrainChunks = new List<Chunk>();
        Chunk[,] m_chunks = new Chunk[VoxelData.m_numOfChunksInWorld, VoxelData.m_numOfChunksInWorld];
        public Material m_terrainMaterial;
        public BlockType[] m_blockTypes;

        public GameObject m_player;

        private void Start()
        {
            for(int x  = 0; x < VoxelData.m_numOfChunksInWorld; x++)
                for(int y = 0; y < VoxelData.m_numOfChunksInWorld; y++)
                    GenerateTerrain(x, y);
            //Instantiate(m_player, new Vector3(VoxelData.m_chunkWidth / 2, VoxelData.m_chunkHeight / 2, VoxelData.m_chunkWidth / 2), Quaternion.identity);

        }
        private void GenerateTerrain(int posX, int posY)
        {
            m_chunks[posX, posY] = new Chunk(this, new ChunkCoordinates(new Vector2(posX, posY)));
        }

        public bool isChunkInWorld(ChunkCoordinates co)
        {
            if (co.m_chunkPos.x > 0 && co.m_chunkPos.x < VoxelData.m_numOfChunksInWorld - 1 && co.m_chunkPos.y > 0 && co.m_chunkPos.y < VoxelData.m_numOfChunksInWorld - 1)
                return true;
            else
                return false;
        }

        public byte GetVoxel(Vector3 pos)
        {

            int yPos = Mathf.FloorToInt(pos.y);

            if (!isVoxelInWorld(pos))
                return 0;

            if (yPos == 0)
                return 1;

            int terrainHeight = Mathf.FloorToInt(VoxelData.m_chunkHeight * Noise.Get2DPerlin(new Vector2(pos.x, pos.z), 500, 0.25f));

            if (yPos <= terrainHeight)
                return 2;
            else return 0;


        }

        public bool isVoxelInWorld(Vector3 pos)
        {
            if (pos.x >= 0 && pos.x < VoxelData.m_worldSizeInVoxels && pos.y >= 0 && pos.y < VoxelData.m_worldSizeInVoxels && pos.z >= 0 && pos.z < VoxelData.m_worldSizeInVoxels)
                return true;
            else 
                return false;
        }

        public Chunk GetChunkFromVector3(Vector3 pos)
        {

            int x = Mathf.FloorToInt(pos.x / VoxelData.m_chunkWidth);
            int z = Mathf.FloorToInt(pos.z / VoxelData.m_chunkWidth);
            return m_chunks[x, z];
        }

        ChunkCoordinates GetChunkCoordFromVector3(Vector3 pos)
        {
            int x = Mathf.FloorToInt(pos.x / VoxelData.m_chunkWidth);
            int z = Mathf.FloorToInt(pos.z / VoxelData.m_chunkWidth);
            return new ChunkCoordinates(x, z);

        }

        private void LoadDataFromFileName()
        { 
        
        }


    }
}