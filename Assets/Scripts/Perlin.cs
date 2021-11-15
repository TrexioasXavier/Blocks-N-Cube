using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perlin : MonoBehaviour
{

    public Texture2D m_texture;
    public float m_horizontalMultiplier;
    public float m_verticalMultiplier;
    public GameObject obj;

    private void Start()
    {
        for (int x = 0; x < 30; x++)
        {
            SampleTexture();
        }
    }

    private void SampleTexture()
    {
        float height = m_texture.height * m_verticalMultiplier;
        float width = m_texture.width * m_horizontalMultiplier;


        float y = Mathf.PerlinNoise( 1.0f * width * Time.time, 0.0f * Time.time);

        Instantiate(obj, new Vector3(0.0f, y, 0.0f), Quaternion.identity);
    }

}
