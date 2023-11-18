using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public int seed = 42;
    public float length = 100f;
    public float width = 100f;
    public float scale = 20.0f;
    public float rockscale = 75.0f;
    public float lakescale = 5f;
    public float xOffset;
    public float zOffset;
    public GameObject outterWall;
    public GameObject theVoid;
    public GameObject grass;
    public GameObject flower;
    public GameObject tree;
    public GameObject boulder;
    public GameObject rock;
    public GameObject lake; // make way...
    public GameObject tent;
    public GameObject pickaxe;
    public GameObject helmet;
    public GameObject campfire;
    public GameObject minecarttrack;

    private int[] map;
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
        
        // layer 1: flowers trees and grass
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
                    map[(int)y * (int)width + (int)x] = 0; // void
                else if (sample == Mathf.Clamp(sample, 0.5f, 0.7f))
                {
                    if (Random.Range(0,100) >= 75)
                      map[(int)y * (int)width + (int)x] = 2; //flower
                    else 
                      map[(int)y * (int)width + (int)x] = 1; // grass
                }
                else if (sample == Mathf.Clamp(sample, 0.7f, 1f))
                    map[(int)y * (int)width + (int)x] = 3; //tree
                else
                    map[(int)y * (int)width + (int)x] = 0;

                x++;
            }
            y++;
        }
        y = 0f;
        // layer 2: boulders and rocks
        while (y < length)
        {
            float x = 0f;
            while (x < width)
            {
                float xCoord = randomorg + x / width * rockscale;
                float yCoord = randomorg + y / length * rockscale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                if (sample == Mathf.Clamp(sample, .8f, .95f))
                    map[(int)y * (int) width + (int)x] = 5; // rock
                else if (sample == Mathf.Clamp(sample, .95f, 1f))
                    map[(int)y * (int) width + (int)x] = 4; //boulder
                x++;
            }
            y++;
        }
        y = 0f;
        // layer 3: lakes ( 12 x 12 )
        Debug.Log("Starting Lake gen");
        while (y < length)
        {
            float x = 0f;
            while (x < width)
            {
                float xCoord = randomorg + x / width * lakescale;
                float yCoord = randomorg + y / length * lakescale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                if (sample == Mathf.Clamp(sample, .8f, 1f))
                {
                    map[(int)y * (int)width + (int)x] = 6;
                    for(int i = 0; i < 12; i++)
                    {
                        for(int j = 0; j < 12; j++)
                        {
                            if(j + y < length && i + x < width && j + y >= 0 && i + x >= 0)
                            {
                                if(map[(int)(j + y - 6) * (int)width + (int)(i + x - 6)] != 6)
                                {
                                    map[(int)(j + y - 6) * (int)width + (int)(i + x -6)] = 0;
                                }
                            }
                        }
                    }
                }
            x++;
            }
        y++;
        }
                  //do nothing;
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
                    Instantiate(outterWall, new Vector3(i + xOffset, 0f, j + zOffset), Quaternion.identity);
                }
                Debug.Log("checking for random tree");
                GameObject toInst = theVoid;
                switch (map[i * (int)width + j])
                {
                    case 0:
                        break;
                    case 1:
                        toInst = grass;
                        break;
                    case 2: 
                        toInst = flower;
                        break;
                    case 3:
                        toInst = tree;
                        break;
                    case 4:
                        toInst = boulder;
                        break;
                    case 5:
                        toInst = rock;
                        break;
                    case 6:
                        toInst = lake;
                        break;
                    case 7:
                        toInst = tent;
                        break;
                    case 8:
                        toInst = pickaxe;
                        break;
                    case 9:
                        toInst = helmet;
                        break;
                    case 10: 
                        toInst = campfire;
                        break;
                    case 11: 
                        toInst = minecarttrack;
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
