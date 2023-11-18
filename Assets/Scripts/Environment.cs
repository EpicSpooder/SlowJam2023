using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public int seed = 42;
    public float length = 100f;
    public float width = 100f;
    public float scale = 1.0f;
    public float xOffset;
    public float zOffset;
    public GameObject obj;
    public GameObject outerWall;
    public GameObject tree;
    public GameObject grass;
    public GameObject boulder;
    public GameObject rock;

    public Texture2D noiseTex;
    public int[] map;
    //
    // Void/Dirt = 0
    // Grass = 1
    // Tree = 2
    // Boulder = 3
    // Rock = 4
    void GenerateMap()
    {
        //Set up the texture and add a Color array to hold pixels during processing
        //noiseTex = new Texture2D(width, length);
        //pix = new Color[noiseTex.width * noiseTex.height)];
        float randomorg = Random.Range(0,100);
        map = new int[(int)width * (int)length];
        // For each pixel in the texture...
        float y = 0.0f;

        while (y < length)
        {
            float x = 0.0f;
            while (x < width)
            {
                
                float xCoord = randomorg + x / width * scale;
                float yCoord = randomorg + y / length * scale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                //Debug.Log("hello");
                if (sample == Mathf.Clamp(sample, 0f, 0.5f))
                    map[(int)y * (int)width + (int)x] = 0;
                else if (sample == Mathf.Clamp(sample, 0.5f, 0.6f))
                    map[(int)y * (int)width + (int)x] = 1;
                else if (sample == Mathf.Clamp(sample, 0.6f, 0.7f))
                    map[(int)y * (int)width + (int)x] = 2;
                else if (sample == Mathf.Clamp(sample, 0.7f, 0.8f))
                    map[(int)y * (int)width + (int)x] = 3;
                else if (sample == Mathf.Clamp(sample, 0.8f, 1f))
                    map[(int)y * (int)width + (int)x] = 4;
                else
                    map[(int)y * (int)width + (int)x] = 0;

                x++;
            }
            y++;
        }

        // Copy the pixel data to the texture and load it into the GPU.
        //noiseTex.SetPixels(pix);
        //noiseTex.Apply();

        //mappreview.texture = noiseTex;
        //worldmap = noiseTex;
    
    }
    // Start is called before the first frame update
    void Start()
    {
    Debug.Log("Starting world gen");
    GenerateMap();
    zOffset = -width / 2f;
    xOffset = -length / 2f;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Debug.Log("checking outer wall");
                //Instantiate(obj, new Vector3(i + xOffset, 5.1f, j+ zOffset), Quaternion.identity);
                if (i == 0 || j == 0 || i >=length - 1 || j >= width - 1) 
                {
                    Instantiate(outerWall, new Vector3(i + xOffset, 0f, j + zOffset), Quaternion.identity);
                }
                Debug.Log("checking for random tree");
                GameObject toInst = grass;
                switch (map[i * (int)width + j])
                {
                    case 0:
                        break;
                    case 1:
                        toInst = grass;
                        break;
                    case 2:
                        toInst = tree;
                        break;
                    case 3:
                        toInst = boulder;
                        break;
                    case 4:
                        toInst = rock;
                        break;
                }

                Instantiate(toInst, new Vector3(i + xOffset, 0f, j + zOffset), Quaternion.identity);

            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
