using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheLeader : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float followDistance = 3f;

    [HideInInspector] public bool lineLeader = false;
    [HideInInspector] public GameObject inFrontOfMe = null;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    public void Follow()
    {
        if (inFrontOfMe != null)
        {

        }
    }
}
