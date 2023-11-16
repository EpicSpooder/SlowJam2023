using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    public float length = 100f;
    public float width = 100f;
    public float xOffset;
    public float zOffset;
    public GameObject obj;
    
    // Start is called before the first frame update
    void Start()
    {
    zOffset = -width / 2f;
    xOffset = -length / 2f;
        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < width; j++)
            {
                Instantiate(obj, new Vector3(i + xOffset, 0, j+ zOffset), Quaternion.identity);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
