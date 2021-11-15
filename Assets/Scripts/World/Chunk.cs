using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BNC
{
    public class Chunk
    {
        public MeshRenderer m_meshRenderer;
        public MeshFilter m_meshFilter;
        public GameObject m_chunkObject;
        private Terrain m_terrain;
        private MeshCollider m_chunkMeshCollider;
        private Mesh m_mesh;

        private Material m_chunkMaterial;

        private int m_vertexIndex = 0;
        private List<Vector3> m_verticesArray = new List<Vector3>();
        private List<Vector3> m_normalsArray = new List<Vector3>();
        private List<int> m_trianglesArray = new List<int>();
        private List<Vector2> m_uvsArray = new List<Vector2>();
        private List<Color> m_colorsArray = new List<Color>();

        private byte[,,] m_voxelMap = new byte[VoxelData.m_chunkWidth, VoxelData.m_chunkHeight, VoxelData.m_chunkWidth];

        private ChunkCoordinates m_chunkCoord;

        private float m_placementRange = 8.0f;

        public Chunk(Terrain mainTerrain, ChunkCoordinates chunkCoordinates)
        {
            this.m_terrain = mainTerrain;
            m_chunkCoord = chunkCoordinates;

            this.CreateGameObject();

            PopulateVoxelMap();
            UpdateChunk();
            

            
        }

        public bool isActive
        { 
            get { return m_chunkObject.activeSelf; }
            set { m_chunkObject.SetActive(value); }
        }

        public Vector3 position
        {
            get { return m_chunkObject.transform.position; }
        }

        public bool CheckVoxel(Vector3 pos)
        {
            int x = Mathf.FloorToInt(pos.x);
            int y = Mathf.FloorToInt(pos.y);
            int z = Mathf.FloorToInt(pos.z);

            if (!isVoxelInChunk(x, y, z))
                return m_terrain.m_blockTypes[m_terrain.GetVoxel(pos + position)].m_isSolid;

            return m_terrain.m_blockTypes[m_voxelMap[x, y, z]].m_isSolid;
        }

        private void UpdateChunk()
        {
            ClearMeshData(); // This basically updates the mesh data every time this function is called.
            for (int y = 0; y < VoxelData.m_chunkHeight; y++)
                for (int x = 0; x < VoxelData.m_chunkWidth; x++)
                    for (int z = 0; z < VoxelData.m_chunkWidth; z++)
                        if(m_terrain.m_blockTypes[m_voxelMap[x, y, z]].m_isSolid)
                            this.UpdateMeshData(new Vector3(x, y, z));

            // Create the mesh from the MeshData in UpdateChunk.
            // This mesh can be used for like collision detection.
            this.CreateMesh();
        }

        private void ClearMeshData()
        {
            m_vertexIndex = 0;
            m_verticesArray.Clear();
            m_trianglesArray.Clear();
            m_uvsArray.Clear();
        }

        private void PopulateVoxelMap()
        {
            

            for (int y = 0; y < VoxelData.m_chunkHeight; y++)
                for (int x = 0; x < VoxelData.m_chunkWidth; x++)
                    for (int z = 0; z < VoxelData.m_chunkWidth; z++)
                        m_voxelMap[x, y, z] = m_terrain.GetVoxel(new Vector3(x, y, z) + position );
        }


        private void PlaceVoxel(Vector3 pos)
        { 
        
        }

        private void RemoveVoxel(Vector3 pos)
        {
            EditVoxel(pos, 0);
        }

        private void EditVoxel(Vector3 pos, byte newID)
        {
            int xCheck = Mathf.FloorToInt(pos.x);
            int yCheck = Mathf.FloorToInt(pos.y);
            int zCheck = Mathf.FloorToInt(pos.z);
            xCheck -= Mathf.FloorToInt(m_chunkObject.transform.position.x);
            yCheck -= Mathf.FloorToInt(m_chunkObject.transform.position.y);

            //If newID is set to zero, then it will place an air (empty non existing) block.
            m_voxelMap[xCheck, yCheck, zCheck] = newID;

            //Update surronding chunks
            UpdateSurroundingVoxels(xCheck, yCheck, zCheck);

            UpdateChunk();
        }

        void UpdateSurroundingVoxels(int x, int y, int z)
        {
            Vector3 _thisVoxel = new Vector3(x, y, z);
            for (int i = 0; i < 6; i++)
            {
                Vector3 _currentVoxel = _thisVoxel + VoxelData.m_faceChecks[i];

                if (!isVoxelInChunk((int)_currentVoxel.x, (int)_currentVoxel.y, (int)_currentVoxel.z))
                {
                    m_terrain.GetChunkFromVector3(_currentVoxel + position).UpdateChunk();
                }
            }
        }


        public void UpdateMeshData(Vector3 pos)
        {
            for (int i = 0; i < 6; i++)
            {
                if (!CheckVoxel(pos + VoxelData.m_faceChecks[i]))
                {
                    
                    byte textureID = m_voxelMap[(int)pos.x, (int)pos.y, (int)pos.z];

                    m_verticesArray.Add(pos + VoxelData.m_vertices[VoxelData.m_voxelTriangles[i, 0]]);
                    m_verticesArray.Add(pos + VoxelData.m_vertices[VoxelData.m_voxelTriangles[i, 1]]);
                    m_verticesArray.Add(pos + VoxelData.m_vertices[VoxelData.m_voxelTriangles[i, 2]]);
                    m_verticesArray.Add(pos + VoxelData.m_vertices[VoxelData.m_voxelTriangles[i, 3]]);
                    
                    AddTexture(m_terrain.m_blockTypes[textureID].GetTextureID(i));
                    
                    m_trianglesArray.Add(m_vertexIndex);
                    m_trianglesArray.Add(m_vertexIndex + 1);
                    m_trianglesArray.Add(m_vertexIndex + 2);
                    m_trianglesArray.Add(m_vertexIndex + 2);
                    m_trianglesArray.Add(m_vertexIndex + 1);
                    m_trianglesArray.Add(m_vertexIndex + 3);


                    m_vertexIndex += 4;
                }
            }
        }

        private void CreateMesh()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = m_verticesArray.ToArray();
            mesh.triangles = m_trianglesArray.ToArray();
            mesh.uv = m_uvsArray.ToArray();

            mesh.RecalculateNormals();

            m_meshFilter.mesh = mesh;            
        }

        private void CreateGameObject()
        {
            m_chunkObject = new GameObject();
            m_chunkObject.name = "ChunkCoordinate: {X: " + m_chunkCoord.m_chunkPos.x + ", Y: " + m_chunkCoord.m_chunkPos.y + " }";
            MeshFilter m = m_chunkObject.AddComponent<MeshFilter>();
            MeshRenderer mr = m_chunkObject.AddComponent<MeshRenderer>();
            MeshCollider mc = m_chunkObject.AddComponent<MeshCollider>();

            m_meshFilter = m;
            m_meshRenderer = mr;
            m_chunkMeshCollider = mc;

            m_chunkObject.transform.position = new Vector3(m_chunkCoord.m_chunkPos.x * VoxelData.m_chunkWidth, 0f, m_chunkCoord.m_chunkPos.y * VoxelData.m_chunkWidth);
            m_chunkObject.transform.SetParent(m_terrain.transform);
            m_chunkMaterial = m_terrain.m_terrainMaterial;
            m_meshRenderer.material = this.m_chunkMaterial;

            UpdateMeshCollider();
        }

        private void AddTexture(int TextureID)
        {
            float y = TextureID / VoxelData.m_textureSizeInBlocks;
            float x = TextureID - (y * VoxelData.m_textureSizeInBlocks);

            x *= VoxelData.m_normalizedBlockTextureSize;
            y *= VoxelData.m_normalizedBlockTextureSize;

            y = 1.0f - y - VoxelData.m_normalizedBlockTextureSize;

            m_uvsArray.Add(new Vector2(x, y));
            m_uvsArray.Add(new Vector2(x, y + VoxelData.m_normalizedBlockTextureSize));
            m_uvsArray.Add(new Vector2(x + VoxelData.m_normalizedBlockTextureSize, y));
            m_uvsArray.Add(new Vector2(x + VoxelData.m_normalizedBlockTextureSize, y + VoxelData.m_normalizedBlockTextureSize));
            

        }

         bool isVoxelInChunk(int x, int y, int z)
        {
            if (x < 0 || x > VoxelData.m_chunkWidth - 1 || y < 0 || y > VoxelData.m_chunkHeight - 1 || z < 0 || z > VoxelData.m_chunkWidth - 1)
                return false;
            else 
                return true;
        }

        public void UpdateMeshCollider()
        {
            this.m_chunkMeshCollider.sharedMesh = m_meshFilter.mesh;
        }


        public byte GetVoxelFromGlobalVector3(Vector3 pos)
        {
            int xCheck = Mathf.FloorToInt(pos.x);
            int yCheck = Mathf.FloorToInt(pos.y);
            int zCheck = Mathf.FloorToInt(pos.z);
            xCheck -= Mathf.FloorToInt(m_chunkObject.transform.position.x);
            yCheck -= Mathf.FloorToInt(m_chunkObject.transform.position.y);

            return m_voxelMap[xCheck, yCheck, zCheck];


        }
        

    }
}