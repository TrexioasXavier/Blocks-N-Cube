using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Photon.Pun;
using Photon.Realtime;

public class WorldSerialization : MonoBehaviour
{

    public Terrain m_instantiatedTerrain;
    
    public List<DeviceAbstract> m_devices;
    private List<Vector3[]> m_normals;
    private List<Color[]> m_colors;
    private List<Vector2[]> m_texCoords;
    private List<Vector3[]> m_vertices;
    private List<Mesh> m_meshes;

    


    public void LoadInformation()
    {
        if (m_instantiatedTerrain == null)
            return;

        m_meshes = new List<Mesh>(m_instantiatedTerrain.transform.childCount);

        for(int x = 0; x < m_instantiatedTerrain.transform.childCount; x++)
            m_meshes[x] = m_instantiatedTerrain.transform.GetChild(x).GetComponent<Mesh>();


        m_vertices = new List<Vector3[]>(m_meshes.Count);
        m_normals = new List<Vector3[]>(m_meshes.Count);
        m_colors = new List<Color[]>(m_meshes.Count);
        m_texCoords = new List<Vector2[]>(m_meshes.Count);

        for (int x = 0; x < m_meshes.Count; x++)
        {
            m_vertices[x] = m_meshes[x].vertices;
            m_normals[x] = m_meshes[x].normals;
            m_colors[x] = m_meshes[x].colors;
            m_texCoords[x] = m_meshes[x].uv;
        }
    }

    public void SaveWorld(string fileName)
    {
        LoadInformation();

        string _destination = Application.persistentDataPath + fileName + ".bnlw";
        FileStream file;

        if (File.Exists(_destination)) file = File.OpenWrite(_destination);
        else file = File.Create(_destination);

        BinaryFormatter bf = new BinaryFormatter();
        for (int x = 0; x < m_meshes.Count; x++)
        {
            bf.Serialize(file, m_meshes[x]);
        }
        file.Close();

    }

    public void LoadWorld()
    { 
    
    }

    
}
